using LancheBom.Database;
using LancheBom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LancheBom.Controllers
{
    [ApiController]
    [Route("/api/v1/lanche")]
    public class LancheController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LancheController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> obterLanches()
        {
            List<Lanche> listaDeLanches = await _context.Lanches.ToListAsync();
            return Ok(listaDeLanches);
        }

        [HttpGet("adicional")]
        public async Task<ActionResult> obterLanchesEAdicionais()
        {
            List<Lanche> listaDeLanches = await _context.Lanches.ToListAsync();
            List<Adicional> listaDeAdicionais = await _context.Adicionais.ToListAsync();
         
            return Ok(new { lanches = listaDeLanches, adicionais = listaDeAdicionais });
        }
    }
}