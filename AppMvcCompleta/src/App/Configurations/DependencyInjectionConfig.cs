using App.Extensions;
using DevIo.Business.Interfaces;
using DevIo.Business.Interfaces.Repositories;
using DevIo.Business.Notifications;
using DevIo.Business.Services;
using DevIo.Data;
using DevIo.Data.Repository;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;

namespace App.Configurations
{
    /// <summary>
    /// Injecao de dependecias de todo projeto
    /// </summary>
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependecies(this IServiceCollection services)
        {
            //Dependencias
            services.AddScoped<AplicacaoContext>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>(); 
            services.AddScoped<IProdutoService, ProdutosSevice>(); 
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<INotificador, Notificador>();
            services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributoAdapterProvider>();
            return services;
        }
    }
}
