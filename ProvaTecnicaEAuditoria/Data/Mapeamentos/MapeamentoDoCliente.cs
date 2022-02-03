using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProvaTecnicaEAuditoria.Models;

namespace ProvaTecnicaEAuditoria.Data.Mapeamentos
{
    public class MapeamentoDoCliente : IEntityTypeConfiguration<Cliente>
    {


        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            // Tabela
            builder.ToTable("Cliente");

            // Chave Primária
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            // Propriedades
            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnName("Nome")
                .HasColumnType("VARCHAR")
                .HasMaxLength(200);

            builder.Property(x => x.Cpf)
                .IsRequired()
                .HasColumnName("CPF")
                .HasColumnType("VARCHAR")
                .HasMaxLength(11);

            builder.Property(x => x.DataDeNascimento)
                 .IsRequired()
                 .HasColumnName("DataNascimento")
                .HasColumnType("DATETIME");

            // Índices
            builder
                .HasIndex(x => x.Cpf, "idx_CPF")
                .IsUnique();

            builder
                 .HasIndex(x => x.Nome, "idx_NOME")
                 .IsUnique();
        }
    }
}