using DevIo.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevIo.Data.Contexto
{
    public class AplicacaoContext : DbContext
    {
        public AplicacaoContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //so seta o valor de varchar de 200 quando esquecer de maperar um propriedade string
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(200)");
            }

            //busca todas as entidades que estao mapeadas dentro do dbContext e buscam todas as classe que herdam de IEntityTypeConfiguration (que estão no mapping) para aquelas
            //Entidades que estao relacionadas no dbContext e ai registram todas de um vez só.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AplicacaoContext).Assembly);

            //Desativando o delete cascade. Impedindo que por exemplo exclua o pai, exclua todos os filhos, opcional.
            //fica por conta da aplicação excluir os filhos
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            base.OnModelCreating(modelBuilder);
        }

        //Sobreescrito o metodo saveChanges apenas para setar a data cadastro default para todas entidades que possui esse atriubuto
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.GetType().GetProperty("DataCadastro") == null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                    entry.Property("DataAtualizacao").CurrentValue = DateTime.Now;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                    entry.Property("DataAtualizacao").CurrentValue = DateTime.Now;
                }
            }
            return await base.SaveChangesAsync();
        }
    }
}