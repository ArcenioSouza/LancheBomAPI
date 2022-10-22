using LancheBom.Models;
using Microsoft.EntityFrameworkCore;

namespace LancheBom.Database
{
    public class ApplicationDbContext : DbContext
    {        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Lanche> Lanches { get; set; }
        public DbSet<Adicional> Adicionais { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoAdicional> PedidosAdicionais { get; set; }
    }
}
