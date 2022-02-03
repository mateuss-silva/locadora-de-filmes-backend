using Microsoft.EntityFrameworkCore;
using ProvaTecnicaEAuditoria.Data.Mapeamentos;
using ProvaTecnicaEAuditoria.Models;

namespace ProvaTecnicaEAuditoria.Data
{
    public class EAuditoriaDataContext:DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }
        public DbSet<Filme> Filmes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseMySQL("Server=localhost,port;Database=EAuditoria;User ID=;Password=1q2w3e4r@#$");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MapeamentoDoCliente());
            modelBuilder.ApplyConfiguration(new MapeamentoDoFilme());
            modelBuilder.ApplyConfiguration(new MapeamentoDaLocacao());
        }
    }
}
