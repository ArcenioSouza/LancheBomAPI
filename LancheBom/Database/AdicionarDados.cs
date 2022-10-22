using LancheBom.Models;
using System;

namespace LancheBom.Database
{
    public class AdicionarDados
    {
        public void AdicionarDadosNoBanco(ApplicationDbContext context)
        {
            Lanche lanche1 = new Lanche()
            {
                Id = 1,
                Nome = "X-Burguer",
                Preco = 5.00
            };
            Lanche lanche2 = new Lanche()
            {
                Id = 2,
                Nome = "X-Salada",
                Preco = 4.50
            };
            Lanche lanche3 = new Lanche()
            {
                Id = 3,
                Nome = "X-Tudo",
                Preco = 7.00
            };

            context.Lanches.Add(lanche1);
            context.Lanches.Add(lanche2);
            context.Lanches.Add(lanche3);

            Adicional adicional1 = new Adicional()
            {
                Id = 1,
                Nome = "Batata Frita",
                Preco = 2.00
            };

            Adicional adicional2 = new Adicional()
            {
                Id = 2,
                Nome = "Batata Frita",
                Preco = 2.00
            };

            context.Adicionais.Add(adicional1);
            context.Adicionais.Add(adicional2);

            context.SaveChanges();
        }
    }
}
