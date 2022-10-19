using LancheBom.Database;
using LancheBom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LancheBom.Controllers
{
    [ApiController]
    [Route("/api/v1/adicional")]
    public class AdicionalController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdicionalController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> cadastrarAdicional(Adicional adicional)
        {
            await _context.Adicionais.AddAsync(adicional);
            await _context.SaveChangesAsync();
            return Created("", adicional);
        }

        [HttpGet]
        public async Task<ActionResult> obterAdicional()
        {
            List<Adicional> listaDeAdicionais = await _context.Adicionais.ToListAsync();
            return Ok(listaDeAdicionais);
        }

    }
}