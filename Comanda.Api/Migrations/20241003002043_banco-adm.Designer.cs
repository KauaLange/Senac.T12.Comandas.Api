﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemaDeComandas.BancoDeDados;

#nullable disable

namespace Comanda.Api.Migrations
{
    [DbContext(typeof(ComandaContexto))]
    [Migration("20241003002043_banco-adm")]
    partial class bancoadm
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("SistemaDeComandas.Modelos.CardapioItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("PossuiPreparo")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("CardapioItems", t =>
                        {
                            t.HasComment("Cadastro de items do cardapio");
                        });
                });

            modelBuilder.Entity("SistemaDeComandas.Modelos.Comanda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NomeCliente")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("NumeroMesa")
                        .HasColumnType("int");

                    b.Property<int>("SituacaoComanda")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Comandas");
                });

            modelBuilder.Entity("SistemaDeComandas.Modelos.ComandaItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CardapioItemId")
                        .HasColumnType("int");

                    b.Property<int>("ComandaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CardapioItemId");

                    b.HasIndex("ComandaId");

                    b.ToTable("ComandaItems");
                });

            modelBuilder.Entity("SistemaDeComandas.Modelos.Mesa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("NumeroMesa")
                        .HasColumnType("int");

                    b.Property<int>("SituacaoMesa")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Mesas");
                });

            modelBuilder.Entity("SistemaDeComandas.Modelos.PedidoCozinha", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ComandaId")
                        .HasColumnType("int");

                    b.Property<int>("SituacaoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ComandaId");

                    b.ToTable("PedidoCozinhas");
                });

            modelBuilder.Entity("SistemaDeComandas.Modelos.PedidoCozinhaItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ComandaItemId")
                        .HasColumnType("int");

                    b.Property<int>("PedidoCozinhaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ComandaItemId");

                    b.HasIndex("PedidoCozinhaId");

                    b.ToTable("PedidoCozinhaItems");
                });

            modelBuilder.Entity("SistemaDeComandas.Modelos.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("SistemaDeComandas.Modelos.ComandaItem", b =>
                {
                    b.HasOne("SistemaDeComandas.Modelos.CardapioItem", "CardapioItem")
                        .WithMany()
                        .HasForeignKey("CardapioItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaDeComandas.Modelos.Comanda", "Comanda")
                        .WithMany("ComandaItems")
                        .HasForeignKey("ComandaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CardapioItem");

                    b.Navigation("Comanda");
                });

            modelBuilder.Entity("SistemaDeComandas.Modelos.PedidoCozinha", b =>
                {
                    b.HasOne("SistemaDeComandas.Modelos.Comanda", "Comanda")
                        .WithMany()
                        .HasForeignKey("ComandaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comanda");
                });

            modelBuilder.Entity("SistemaDeComandas.Modelos.PedidoCozinhaItem", b =>
                {
                    b.HasOne("SistemaDeComandas.Modelos.ComandaItem", "ComandaItem")
                        .WithMany()
                        .HasForeignKey("ComandaItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaDeComandas.Modelos.PedidoCozinha", "PedidoCozinha")
                        .WithMany("PedidoCozinhaItems")
                        .HasForeignKey("PedidoCozinhaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ComandaItem");

                    b.Navigation("PedidoCozinha");
                });

            modelBuilder.Entity("SistemaDeComandas.Modelos.Comanda", b =>
                {
                    b.Navigation("ComandaItems");
                });

            modelBuilder.Entity("SistemaDeComandas.Modelos.PedidoCozinha", b =>
                {
                    b.Navigation("PedidoCozinhaItems");
                });
#pragma warning restore 612, 618
        }
    }
}
