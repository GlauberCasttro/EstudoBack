﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using App.ViewModels;

namespace App.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //public DbSet<App.ViewModels.ProdutoViewModel> ProdutoViewModel { get; set; }
        //public DbSet<App.ViewModels.EnderecoViewModel> EnderecoViewModel { get; set; }
    }
}
