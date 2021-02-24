﻿using Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infra.Configuration
{
    public class ContextBase : DbContext
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {

        }

      
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(GetStringConnectionConfig());
                base.OnConfiguring(builder);
            }

            base.OnConfiguring(builder);
        }

        private string GetStringConnectionConfig()
        {
            return "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProjetoDDD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        public DbSet<Produto> Produtos { get; set; }
    }
}