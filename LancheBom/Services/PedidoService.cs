using LancheBom.Database;
using LancheBom.Models;
using Microsoft.EntityFrameworkCore;

namespace LancheBom.Services
{
    public class PedidoService
    {
        private readonly ApplicationDbContext _context;
        public PedidoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Pedido> gerarPedido(int idLanche, int[]? adicionais)
        {
            Pedido pedido = new Pedido();
            pedido.Lanche = await _context.Lanches.FirstOrDefaultAsync(lanche => lanche.Id == idLanche) ?? throw new ArgumentNullException("Está faltando um lanche no seu pedido ou o lanche escolhido é inválido");

            if (adicionais[0] != 0 || adicionais[1] != 0)
            {
                var removeDuplicados = new HashSet<int>(adicionais);
                var verificaValoresRepetidos = adicionais.Length - removeDuplicados.Count;
                if (verificaValoresRepetidos != 0) throw new ArgumentException("Não é possível fazer um pedido com dois adicionais iguais");
            }

            foreach (int id in adicionais)
            {
                Adicional? adicional = await _context.Adicionais.FirstOrDefaultAsync(adicionais => adicionais.Id == id);
                if(adicional != null)
                {
                    PedidoAdicional pedidoAdicional = new PedidoAdicional
                    {
                        Adicional = adicional,
                        Pedido = pedido,
                    };

                    await _context.PedidosAdicionais.AddAsync(pedidoAdicional);
                }
            };

            double valorPedido = calcularValorDoPedido(pedido);
            pedido.ValorPedido = valorPedido;

            await _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();

            return pedido;
        }

        public async Task<Pedido> atualizarPedido(int idPedido, int? idLanche, int[]? adicionais)
        {
            Pedido? pedidoAtualizado = await _context.Pedidos.Include(lanche => lanche.Lanche).Include(adicionais => adicionais.PedidoAdicional).Where(pedido => pedido.Status == true).FirstOrDefaultAsync(pedido => pedido.Id == idPedido);

            if (pedidoAtualizado == null) throw new ArgumentNullException("Pedido não encontrado");          

            if(idLanche != null) pedidoAtualizado.Lanche = await _context.Lanches.FirstOrDefaultAsync(lanche => lanche.Id == idLanche) ?? throw new ArgumentNullException("O lanche escolhido é inválido");

            List<PedidoAdicional> AdicionaisDoPedido = await _context.PedidosAdicionais.Where(pedido => pedido.PedidoId == idPedido).ToListAsync();

            AdicionaisDoPedido.ForEach(adicional =>
            {
                pedidoAtualizado.PedidoAdicional.Remove(adicional);
                _context.PedidosAdicionais.Remove(adicional);
            });

            await _context.SaveChangesAsync();

            if (adicionais[0] != 0 || adicionais[1] != 0)
            {
                var removeDuplicados = new HashSet<int>(adicionais);
                var verificaValoresRepetidos = adicionais.Length - removeDuplicados.Count;
                if (verificaValoresRepetidos != 0) throw new ArgumentException("Não é possível atualizar um pedido com dois adicionais iguais");
            }

            foreach (int id in adicionais)
            {
                Adicional? adicional = await _context.Adicionais.FirstOrDefaultAsync(adicionais => adicionais.Id == id);
                if (adicional != null)
                {
                    PedidoAdicional pedidoAdicional = new PedidoAdicional
                    {
                        Adicional = adicional,
                        Pedido = pedidoAtualizado,
                    };

                    await _context.PedidosAdicionais.AddAsync(pedidoAdicional);
                }
            };

            double valorPedido = calcularValorDoPedido(pedidoAtualizado);
            pedidoAtualizado.ValorPedido = valorPedido;

            await _context.Pedidos.AddAsync(pedidoAtualizado);

            List<PedidoAdicional> AdicionaisDoPedido2 = await _context.PedidosAdicionais.Where(pedido => pedido.PedidoId == idPedido).ToListAsync();

            _context.Pedidos.Update(pedidoAtualizado);
            await _context.SaveChangesAsync();

            return pedidoAtualizado;
        }        

        private double calcularValorDoPedido(Pedido pedido)
        {
            double valorTotal = pedido.Lanche.Preco;

            foreach (PedidoAdicional adicional in pedido.PedidoAdicional)
            {
                valorTotal += adicional.Adicional.Preco;
            }

            if(pedido.PedidoAdicional.Count == 2)
            {
                var desconto = valorTotal * 0.20;
                valorTotal -= desconto;
            }
            else
            {
                foreach (PedidoAdicional adicional in pedido.PedidoAdicional)
                {
                    if(adicional.Adicional.Nome == "Refrigerante")
                    {
                        var desconto = valorTotal * 0.15;
                        valorTotal -= desconto;
                    }
                    else
                    {
                        var desconto = valorTotal * 0.10;
                        valorTotal -= desconto;
                    }
                }
            }

            return valorTotal;
        }

        public async Task<List<Pedido>> buscarPedidos()
        {
            List<Pedido> pedidos = await _context.Pedidos.Include(lanche => lanche.Lanche).Include(pedidoAdicional => pedidoAdicional.PedidoAdicional).Where(pedido => pedido.Status == true).AsNoTracking().ToListAsync();

            if (pedidos.Count == 0) throw new ArgumentNullException("Nenhum pedido encontrado");

            return pedidos;
        }

        public async Task<string> apagarPedido(int id)
        {
            Pedido? pedido = await _context.Pedidos.Where(pedido => pedido.Status == true).FirstOrDefaultAsync(pedido => pedido.Id == id);
            if (pedido == null) throw new ArgumentNullException("Pedido não encontrado");
            pedido.Status = false;
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
            return "Pedido excluido com sucesso";
        }
    }
}
