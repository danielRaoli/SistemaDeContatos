using Microsoft.EntityFrameworkCore;
using SistemaDeContatos.Data.Map;
using SistemaDeContatos.Models.Entities;

namespace SistemaDeContatos.Data
{
    public class SistemaDeContatosContext : DbContext
    {
        public SistemaDeContatosContext(DbContextOptions<SistemaDeContatosContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Contato> Contatos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContatoMap());

            base.OnModelCreating(modelBuilder);
        }
       

    }
}
