using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProvaTecnicaEAuditoria.Models;

namespace ProvaTecnicaEAuditoria.Data.Mapeamentos
{
    public class MapeamentoDoFilme : IEntityTypeConfiguration<Filme>
    {


        public void Configure(EntityTypeBuilder<Filme> builder)
        {
            // Tabela
            builder.ToTable("Filme");

            // Chave Primária
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            // Propriedades
            builder.Property(x => x.Titulo)
                .IsRequired()
                .HasColumnName("Titulo")
                .HasColumnType("VARCHAR")
                .HasMaxLength(100);

            builder.Property(x => x.ClassificacaoIndicativa)
                .IsRequired()
                .HasColumnName("ClassificacaoIndicativa")
                .HasColumnType("INT");

            builder.Property(x => x.Lancamento)
                .IsRequired()
                .HasColumnName("Lancamento")
                .HasColumnType("TINYINT");

            // Índices
            builder
                .HasIndex(x => x.Lancamento, "idx_Lancamento")
                .IsUnique();

            builder
                 .HasIndex(x => x.Titulo, "idx_Titulo")
                 .IsUnique();
        }
    }
}
