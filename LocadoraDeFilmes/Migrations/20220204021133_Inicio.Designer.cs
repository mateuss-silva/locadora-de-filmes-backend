﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using LocadoraDeFilmes.Dados;

#nullable disable

namespace LocadoraDeFilmes.Migrations
{
    [DbContext(typeof(EAuditoriaContexto))]
    [Migration("20220204021133_Inicio")]
    partial class Inicio
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LocadoraDeFilmes.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("VARCHAR(11)")
                        .HasColumnName("CPF");

                    b.Property<DateTime>("DataDeNascimento")
                        .HasColumnType("DATETIME")
                        .HasColumnName("DataNascimento");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("VARCHAR(200)")
                        .HasColumnName("Nome");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Cpf" }, "idx_CPF")
                        .IsUnique();

                    b.HasIndex(new[] { "Nome" }, "idx_NOME");

                    b.ToTable("Cliente", (string)null);
                });

            modelBuilder.Entity("LocadoraDeFilmes.Models.Filme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ClassificacaoIndicativa")
                        .HasColumnType("INT")
                        .HasColumnName("ClassificacaoIndicativa");

                    b.Property<sbyte>("Lancamento")
                        .HasColumnType("TINYINT")
                        .HasColumnName("Lancamento");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("Titulo");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Lancamento" }, "idx_Lancamento");

                    b.HasIndex(new[] { "Titulo" }, "idx_Titulo");

                    b.ToTable("Filme", (string)null);
                });

            modelBuilder.Entity("LocadoraDeFilmes.Models.Locacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataDeDevolucao")
                        .HasColumnType("DATETIME")
                        .HasColumnName("DataDevolucao");

                    b.Property<DateTime>("DataDeLocacao")
                        .HasColumnType("DATETIME")
                        .HasColumnName("DataLocacao");

                    b.Property<int>("FilmeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("FilmeId");

                    b.ToTable("Locacao", (string)null);
                });

            modelBuilder.Entity("LocadoraDeFilmes.Models.Locacao", b =>
                {
                    b.HasOne("LocadoraDeFilmes.Models.Cliente", "Cliente")
                        .WithMany("Locacoes")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired()
                        .HasConstraintName("FK_Cliente_idx");

                    b.HasOne("LocadoraDeFilmes.Models.Filme", "Filme")
                        .WithMany("Locacoes")
                        .HasForeignKey("FilmeId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired()
                        .HasConstraintName("FK_Filme_idx");

                    b.Navigation("Cliente");

                    b.Navigation("Filme");
                });

            modelBuilder.Entity("LocadoraDeFilmes.Models.Cliente", b =>
                {
                    b.Navigation("Locacoes");
                });

            modelBuilder.Entity("LocadoraDeFilmes.Models.Filme", b =>
                {
                    b.Navigation("Locacoes");
                });
#pragma warning restore 612, 618
        }
    }
}
