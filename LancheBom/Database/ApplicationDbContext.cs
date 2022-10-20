using LancheBom.Models;
using Microsoft.EntityFrameworkCore;

namespace LancheBom.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Lanche> Lanches { get; set; }
        public DbSet<Adicional> Adicionais { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public ApplicationDbContext()
        {
        }
    }
}
