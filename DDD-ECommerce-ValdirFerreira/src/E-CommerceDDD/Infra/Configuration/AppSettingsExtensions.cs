using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Celig.Infra.Configuration
{
    public static class AppSettingsExtensions
    {
        /// <summary>
        /// Adiciona as configurações da aplicação ao contêiner de injeção de dependência do ASP.NET
        /// de forma fortemente tipada.
        /// </summary>
        /// <example>services.AddConfiguration(Configuration)</example>
        /// <param name="services">Coleção de serviços da aplicação.</param>
        /// <param name="configuration">Configurações da aplicação</param>
        /// <returns>Coleção de serviços da aplicação.</returns>
        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<StringConexaoOptions>(configuration.GetSection(StringConexaoOptions.StringConexao));
            return services;
        }
    }
}
