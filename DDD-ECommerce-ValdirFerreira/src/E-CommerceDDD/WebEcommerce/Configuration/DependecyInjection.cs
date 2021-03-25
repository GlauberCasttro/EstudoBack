using AplicacaoApp.Interfaces;
using AplicacaoApp.OpenApp;
using Dominio.Interfaces.Generics;
using Dominio.Interfaces.Produtos;
using Dominio.Interfaces.Services;
using Dominio.Services;
using Infra.Repositorio;
using Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebEcommerce.Configuration
{
    public static class DependecyInjection
    {
        public static IServiceCollection InjecaoDeDependencia(this IServiceCollection services, IConfiguration Configuration)
        {
            #region CONTEXTOS DA APLICACAO E CONFIGURACAO IDENTITY
            //string de conexão da aplicação

            services.AddDbContext<ContextBase>(options => options.UseSqlServer(Configuration["StringConexao:Padrao"]));
            
            services.AddScoped<ContextBase>();

            //configuração do identity
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ContextBase>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            #endregion
            #region INJECAO DE DEPENDCIA DA APLICAÇÃO
            // INTERFACE REPOSITORIO
           // services.AddTransient(typeof(IGenerics<>), typeof(RepositoryGenerics<>));
            services.AddTransient<IProdutoRepository, ProdutoRepository>();


            //INTERFACE APLICACAO
            services.AddTransient<IProdutoApp, ProdutoApp>();


            //SERVICO DO DOMINIO//
            services.AddTransient<IProdutoService, ProdutoService>();
            #endregion

            return services;
        }
    }
}
