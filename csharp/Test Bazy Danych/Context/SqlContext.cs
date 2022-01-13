using Microsoft.EntityFrameworkCore;
using Test_Bazy_Danych.Model;

namespace Test_Bazy_Danych.Context
{
        public class SqlContext : DbContext
        {
                protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                {
                        optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Database=Obywatele_DB_Jaca_3Dg;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                        base.OnConfiguring(optionsBuilder);
                }

                public DbSet<Osoba> Osoby { get; set; }
                public DbSet<Plec> Plcie { get; set; }
        }
}
