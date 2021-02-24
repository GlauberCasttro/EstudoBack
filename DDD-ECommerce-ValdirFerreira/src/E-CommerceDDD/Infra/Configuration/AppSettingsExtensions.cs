using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Celig.Infra.Configuration
{
    public static class AppSettingsExtensions
    {
        /// <summary>
        /// Adiciona as configura��es da aplica��o ao cont�iner de inje��o de depend�ncia do ASP.NET
        /// de forma fortemente tipada.
        /// </summary>
        /// <example>services.AddConfiguration(Configuration)</example>
        /// <param name="services">Cole��o de servi�os da aplica��o.</param>
        /// <param name="configuration">Configura��es da aplica��o</param>
        /// <returns>Cole��o de servi�os da aplica��o.</returns>
        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<StringConexaoOptions>(configuration.GetSection(StringConexaoOptions.StringConexao));
            return services;
        }
    }
}
