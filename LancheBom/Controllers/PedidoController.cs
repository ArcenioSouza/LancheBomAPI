using LancheBom.Database;
using LancheBom.Models;
using LancheBom.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LancheBom.DTO;

namespace LancheBom.Controllers
{
    [ApiController]
    [Route("/api/v1/pedido")]
    public class PedidoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly PedidoService _service;

        public PedidoController(ApplicationDbContext context, PedidoService service)
        {
            _context = context;
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> cadastrarPedido(PedidoDTO pedidoDTO)
        {
            try
            {
                Pedido pedido = await _service.gerarPedido(pedidoDTO.idLanche, pedidoDTO.idAdicional1, pedidoDTO.idAdicional2);
                await _context.Pedidos.AddAsync(pedido);
                await _context.SaveChangesAsync();
                return Created("", pedido);
            }
            catch(ArgumentNullException ex)
            {
                return NotFound(ex.ParamName);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        public async Task<ActionResult> obterPedidos()
        {
            List<Pedido> pedidos = await _context.Pedidos.Include(lanche => lanche.Lanche).Include(adicionais => adicionais.Adicionais).ToListAsync();
            return Ok(pedidos);
        }

    }
}