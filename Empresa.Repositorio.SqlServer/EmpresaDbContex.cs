using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Empresa.Dominio;

namespace Empresa.Repositorio.SqlServer
{
    public class EmpresaDbContex : DbContext
    {
        public EmpresaDbContex(DbContextOptions options) : base(options)
        {

            Database.EnsureCreated();
        }
        public DbSet<Contato> Contatos  { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
