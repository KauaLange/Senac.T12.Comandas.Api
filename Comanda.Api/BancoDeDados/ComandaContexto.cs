using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SistemaDeComandas.Modelos;

namespace SistemaDeComandas.BancoDeDados
{
    public class ComandaContexto : DbContext
    {
        // criar as variaveis que representam tables
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<CardapioItem> CardapioItems { get; set; }
        public DbSet<Modelos.Comanda> Comandas { get; set; }
        public DbSet<ComandaItem> ComandaItems { get; set; }
        public DbSet<PedidoCozinha> PedidoCozinhas { get; set; }
        public DbSet<PedidoCozinhaItem> PedidoCozinhaItems { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }    

        
    public ComandaContexto(DbContextOptions<ComandaContexto> options) : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            //acesso a entidade CardapioItem para determinar a nomenclatura
            modelBuilder.Entity<CardapioItem>()
                .HasComment("Cadastro de items do cardapio");

            //Uma comanda possui muitos ComandasItems
            //E suas chave estraingeira é ComandaId
            modelBuilder.Entity<Modelos.Comanda>()
                .HasMany<ComandaItem>()
                .WithOne(ci => ci.Comanda)
                .HasForeignKey(f => f.ComandaId);
            // o item de comanda possui um item de cardapio
            // e sua chave estrangeira é CardapioItemId
            modelBuilder.Entity<ComandaItem>()
                .HasOne(ci => ci.CardapioItem)
                .WithMany()
                .HasForeignKey(ci => ci.CardapioItemId);

            modelBuilder.Entity<ComandaItem>()
                .HasOne(ci => ci.Comanda)
                .WithMany(ci => ci.ComandaItems)
                .HasForeignKey(f => f.ComandaId);


            // Pedido cozinha com pedido cozinha item
            modelBuilder.Entity<PedidoCozinhaItem>()
               .HasOne(pci => pci.PedidoCozinha)
               .WithMany(pci => pci.PedidoCozinhaItems)
               .HasForeignKey(pci => pci.PedidoCozinhaId);


            modelBuilder.Entity<PedidoCozinhaItem>()
                .HasOne(pci => pci.ComandaItem)
                .WithMany()
                .HasForeignKey(pci => pci.ComandaItemId);

            base.OnModelCreating(modelBuilder);
        }
    }

}