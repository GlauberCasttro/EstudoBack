using Areas_MVC.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Areas_MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //Customizando o nome de Areas, mudando de areas para Modulos
            services.Configure<RazorViewEngineOptions>(op =>
            {
                op.AreaViewLocationFormats.Clear();
                op.AreaViewLocationFormats.Add("/Modulos/{2}/Views/{1}/{0}.cshtml");
                op.AreaViewLocationFormats.Add("/Modulos/{2}/Views/Shared/{0}.cshtml");
                op.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
                
            });
            services.AddTransient<IPedidoRepository, PedidoRepository>();

            services.AddMvc();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {  
                
                //Mapeando areas da aplicação. A Rota da aréa precisa estar primeiro que a rota padraao
                //endpoints.MapControllerRoute(
                //  name: "areas",
                //  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");


                //Mapeamento de rotas especificas para cada area
                //endpoints.MapControllerRoute("AreaProdutos", "Produtos", "Produtos/{controller=Cadastro}/{action=Index}/{id?}");
                //endpoints.MapControllerRoute("AreaVendas", "Vendas", "Vendas/{controller=Pedido}/{action=Index}/{id?}");
            });
        }
    }
}
