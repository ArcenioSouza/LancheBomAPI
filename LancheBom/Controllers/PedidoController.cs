using LancheBom.Database;
using LancheBom.Models;
using Microsoft.AspNetCore.Mvc;

namespace LancheBom.Controllers
{
    [ApiController]
    [Route("/api/v1/pedido")]
    public class PedidoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PedidoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> cadastrarPedido(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();
            return Created("", pedido);
        }

    }
}