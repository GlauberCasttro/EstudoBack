using Entidades;
using Entidades.Entidades;
using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class ContextBase : IdentityDbContext<IdentityUser>
    {
        public ContextBase(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<CompraUsuario> UsuarioCompras { get; set; }
        public DbSet<IdentityUser> IdentityUser { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUser>().ToTable("AspNetUsers").HasKey(t => t.Id);

            base.OnModelCreating(builder);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {

                // b.GetType().IsSubclassOf(typeof(A))
                var testeFilho = entry is EntityBase;

                if (entry.GetType().IsSubclassOf(typeof(EntityBase)))
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                    }

                    if (entry.State == EntityState.Modified)
                    {
                        entry.Property("DataCadastro").IsModified = false;
                        entry.Property("DataAtualizacao").CurrentValue = DateTime.Now;
                    }
                }

            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
