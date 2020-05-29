using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SistemaFotografa.DomainModel.Interfaces.Repositories;
using SistemaFotografa.DomainModel.Interfaces.Services;
using SistemaFotografa.DomainService.Services;
using SistemaFotografa.InfraStructure.DataAccess;
using SistemaFotografa.InfraStructure.DataAccess.Repositories;

namespace SistemaFotografa
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                
                
                
            });
            
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddTransient<IClienteRepository, ClienteADORepository>();
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<IFotoRepository, FotoADORepository>();
            services.AddTransient<IFotoService, FotoService>();
            services.AddTransient<IAlbumRepository, AlbumADORepository>();
            services.AddTransient<IAlbumService, AlbumService>();
            services.AddTransient<IProdutoRepository, ProdutoADORepository>();
            services.AddTransient<IProdutoService, ProdutoService>();
            services.AddTransient<IPedidoRepository, PedidoADORepository>();
            services.AddTransient<IPedidoService, PedidoService>();
            services.AddTransient<IFotoProdutoRepository, FotoProdutoADORepository>();
            services.AddTransient<IFotoProdutoService, FotoProdutoService>();
            services.AddTransient<IPedidoFotoProdutoRepository, PedidoFotoProdutoADORepository>();
            services.AddTransient<IPedidoFotoProdutoService, PedidoFotoProdutoService>();
            services.AddTransient<IFileServerService, FileServerService>();
             


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Default}/{action=Responsivo}");
            });
        }
    }
}
