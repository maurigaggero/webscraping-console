using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library_Tabla_Valores
{
    public class ValoresContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = valoresDb; Integrated Security = True;");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Valores> Valores { get; set; }
    }
}
