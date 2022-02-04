using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProvaTecnicaEAuditoria.Modelos;

namespace ProvaTecnicaEAuditoria.Dados.Mapeamentos
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
                .HasColumnType("INT")
                .HasColumnName("ClassificacaoIndicativa");

            builder.Property(x => x.Lancamento)
                .IsRequired()
                .HasColumnType("TINYINT")
                .HasColumnName("Lancamento");

            // Índices
            builder
                .HasIndex(x => x.Lancamento, "idx_Lancamento");

            builder
                 .HasIndex(x => x.Titulo, "idx_Titulo");
        }
    }
}
