using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using SistemaDeComandas.BancoDeDados;
using SistemaDeComandas.Modelos;

namespace Comanda.Api
{
    public class InicializarDados
    {
        public static void Semear(ComandaContexto banco)
        {
            // cardapio
            // SE não tem nenhum cardapio item
            if (!banco.CardapioItems.Any())
            {
                banco.CardapioItems.AddRange(
                    new CardapioItem()
                    {
                        Descricao = "Bife, salada, ovo, queijo, maionese, milho, ervilha",
                        PossuiPreparo = true,
                        Preco = 18.00M,
                        Titulo = "XIS SALADA"
                    },
                    new CardapioItem()
                    {
                        Descricao = "Presunto, maionese, ketchup, queijo",
                        PossuiPreparo = true,
                        Preco = 5.50M,
                        Titulo = "TORRADA"
                    },
                    new CardapioItem()
                    {
                        Descricao = "Carne, queijo, cebolinha, ovo, molho da casa",
                        PossuiPreparo = true,
                        Preco = 18.00M,
                        Titulo = "PASTEL"
                    }

                );

            }
            //if
            if (!banco.Usuarios.Any())
            {
                banco.Usuarios.AddRange(
                    new Usuario()
                    {
                        Email = "admin@admin.com",
                        Nome = "Admin",
                        Senha = "admin"
                    }
                );
            }

            if (!banco.Mesas.Any())
            {
                banco.Mesas.AddRange(
                    new Mesa() { NumeroMesa = 1, SituacaoMesa = 1 },
                    new Mesa() { NumeroMesa = 2, SituacaoMesa = 1 },
                    new Mesa() { NumeroMesa = 3, SituacaoMesa = 1 },
                    new Mesa() { NumeroMesa = 4, SituacaoMesa = 1 }

                );
            }

            if (!banco.Comandas.Any())
            {
                var comanda = new SistemaDeComandas.Modelos.Comanda { NomeCliente = "Kauã Oliveira", NumeroMesa = 1, SituacaoComanda = 1 };

                banco.Comandas.Add(comanda);

                if (!banco.ComandaItems.Any())
                {
                    banco.ComandaItems.AddRange(
                        new ComandaItem()
                        {
                            Comanda = comanda,
                            CardapioItemId = 1
                        },
                         new ComandaItem()
                         {
                             Comanda = comanda,
                             CardapioItemId = 2
                         }

                        );


                }
                //INSERRT INTO cardapioItem (Colums) VALUES(1, "SALSICAO"
                banco.SaveChanges();

            }
        }
    }
}
