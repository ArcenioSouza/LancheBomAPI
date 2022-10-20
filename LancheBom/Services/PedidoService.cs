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

        public async Task<Pedido> gerarPedido(int idLanche, int? idAdicional1, int? idAdicional2)
        {
            Pedido pedido = new Pedido();
            var lanche = await _context.Lanches.FirstOrDefaultAsync(lanche => lanche.Id == idLanche);
            if(lanche == null) throw new ArgumentNullException("Está faltando um lanche no seu pedido ou o lanche escolhido é inválido");
            pedido.Lanche = lanche;
            
            var adicional1 = await _context.Adicionais.FirstOrDefaultAsync(adicional => adicional.Id == idAdicional1);
            var adicional2 = await _context.Adicionais.FirstOrDefaultAsync(adicional => adicional.Id == idAdicional2);

            if (adicional1 != null) pedido.Adicionais.Add(adicional1);

            if (adicional2 != null)
            {
                if(adicional2.Id != adicional1.Id)
                {
                    pedido.Adicionais.Add(adicional2);  
                }
                else
                {
                    throw new ArgumentException("Não é possível fazer um pedido com dois adicionais iguais");
                }
            }

            double valorTotalPedido = calcularValorDoPedido(pedido);
            pedido.ValorPedido = valorTotalPedido;

            return pedido;
        }

        private double calcularValorDoPedido(Pedido pedido)
        {
            double valorTotal = pedido.Lanche.Preco;

            if(pedido.Adicionais.Count > 0)
            {
                foreach(Adicional adicional in pedido.Adicionais)
                {
                    valorTotal += adicional.Preco;
                }
            }

            if(pedido.Adicionais.Count >= 2)
            {
                double valorDesconto = valorTotal * 0.2;
                valorTotal -= valorDesconto;
            }
            else
            {
                if (pedido.Adicionais.Count > 0 && pedido.Adicionais[0].Nome == "Refrigerante")
                {
                    double valorDesconto = valorTotal * 0.15;
                    valorTotal -= valorDesconto;
                }
                else if(pedido.Adicionais.Count > 0 && pedido.Adicionais[0].Nome == "Batata-Frita")
                {
                    double valorDesconto = valorTotal * 0.1;
                    valorTotal -= valorDesconto;
                }
            }

            return valorTotal;
        }
    }
}
