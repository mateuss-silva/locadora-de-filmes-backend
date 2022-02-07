using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProvaTecnicaEAuditoria.Modelos;

namespace ProvaTecnicaEAuditoria.Dados.Mapeamentos
{
    public class MapeamentoDaLocacao : IEntityTypeConfiguration<Locacao>
    {


        public void Configure(EntityTypeBuilder<Locacao> builder)
        {
            // Tabela
            builder.ToTable("Locacao");

            // Chave Primária
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            // Propriedades
            builder.Property(x => x.DataDeLocacao)
                .IsRequired()
                .HasColumnType("DATETIME")
                .HasColumnName("DataLocacao");

            builder.Property(x => x.DataDeDevolucao)
                .HasColumnType("DATETIME")
                .HasColumnName("DataDevolucao");


            // Relacionamentos
            builder
                .HasOne(x => x.Filme)
                .WithMany(x => x.Locacoes)
                .HasForeignKey(x => x.FilmeId)
                .HasConstraintName("FK_Filme_idx")
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.Cliente)
                .WithMany(x => x.Locacoes)
                .HasForeignKey(x => x.ClienteId)
                .HasConstraintName("FK_Cliente_idx")
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
