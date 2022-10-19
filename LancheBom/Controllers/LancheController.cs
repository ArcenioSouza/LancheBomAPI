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

        [HttpPost]
        public async Task<ActionResult> cadastrarLanche(Lanche lanche)
        {
            await _context.Lanches.AddAsync(lanche);
            await _context.SaveChangesAsync();
            return Created("", lanche);
        }

        [HttpGet]
        public async Task<ActionResult> obterLanches()
        {
            List<Lanche> listaDeLanches = await _context.Lanches.ToListAsync();
            return Ok(listaDeLanches);
        }
    }
}