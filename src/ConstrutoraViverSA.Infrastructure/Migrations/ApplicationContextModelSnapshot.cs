﻿// <auto-generated />
using System;
using ConstrutoraViverSA.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ConstrutoraViverSA.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ConstrutoraViverSA.Domain.Estoque", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("MaterialId")
                        .HasColumnType("bigint");

                    b.Property<int>("Operacao")
                        .HasColumnType("integer");

                    b.Property<int>("Quantidade")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.ToTable("Estoque");
                });

            modelBuilder.Entity("ConstrutoraViverSA.Domain.Funcionario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Cargo")
                        .HasColumnType("integer");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.Property<DateTime?>("DataNascimento")
                        .IsRequired()
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Genero")
                        .HasColumnType("integer");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<string>("NumCtps")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Funcionario");
                });

            modelBuilder.Entity("ConstrutoraViverSA.Domain.Material", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<int>("Quantidade")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<int>("Tipo")
                        .HasColumnType("integer");

                    b.Property<double>("Valor")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Material");
                });

            modelBuilder.Entity("ConstrutoraViverSA.Domain.Obra", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<long>("OrcamentoId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("PrazoConclusao")
                        .IsRequired()
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("TipoObra")
                        .HasColumnType("integer");

                    b.Property<double?>("Valor")
                        .IsRequired()
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("OrcamentoId")
                        .IsUnique();

                    b.ToTable("Obra");
                });

            modelBuilder.Entity("ConstrutoraViverSA.Domain.ObraMaterial", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DataHora")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("MaterialId")
                        .HasColumnType("bigint");

                    b.Property<long>("ObraId")
                        .HasColumnType("bigint");

                    b.Property<int>("Operacao")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.HasIndex("ObraId");

                    b.ToTable("ObraMaterial");
                });

            modelBuilder.Entity("ConstrutoraViverSA.Domain.Orcamento", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DataEmissao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DataValidade")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TipoObra")
                        .HasColumnType("integer");

                    b.Property<double?>("Valor")
                        .IsRequired()
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Orcamento");
                });

            modelBuilder.Entity("FuncionarioObra", b =>
                {
                    b.Property<long>("FuncionariosId")
                        .HasColumnType("bigint");

                    b.Property<long>("ObrasId")
                        .HasColumnType("bigint");

                    b.HasKey("FuncionariosId", "ObrasId");

                    b.HasIndex("ObrasId");

                    b.ToTable("FuncionarioObra");
                });

            modelBuilder.Entity("ConstrutoraViverSA.Domain.Estoque", b =>
                {
                    b.HasOne("ConstrutoraViverSA.Domain.Material", "Material")
                        .WithMany("Estoque")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Material");
                });

            modelBuilder.Entity("ConstrutoraViverSA.Domain.Obra", b =>
                {
                    b.HasOne("ConstrutoraViverSA.Domain.Orcamento", "Orcamento")
                        .WithOne("Obra")
                        .HasForeignKey("ConstrutoraViverSA.Domain.Obra", "OrcamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Orcamento");
                });

            modelBuilder.Entity("ConstrutoraViverSA.Domain.ObraMaterial", b =>
                {
                    b.HasOne("ConstrutoraViverSA.Domain.Material", "Material")
                        .WithMany("ObraMateriais")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConstrutoraViverSA.Domain.Obra", "Obra")
                        .WithMany("ObraMateriais")
                        .HasForeignKey("ObraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Material");

                    b.Navigation("Obra");
                });

            modelBuilder.Entity("FuncionarioObra", b =>
                {
                    b.HasOne("ConstrutoraViverSA.Domain.Funcionario", null)
                        .WithMany()
                        .HasForeignKey("FuncionariosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConstrutoraViverSA.Domain.Obra", null)
                        .WithMany()
                        .HasForeignKey("ObrasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConstrutoraViverSA.Domain.Material", b =>
                {
                    b.Navigation("Estoque");

                    b.Navigation("ObraMateriais");
                });

            modelBuilder.Entity("ConstrutoraViverSA.Domain.Obra", b =>
                {
                    b.Navigation("ObraMateriais");
                });

            modelBuilder.Entity("ConstrutoraViverSA.Domain.Orcamento", b =>
                {
                    b.Navigation("Obra");
                });
#pragma warning restore 612, 618
        }
    }
}
