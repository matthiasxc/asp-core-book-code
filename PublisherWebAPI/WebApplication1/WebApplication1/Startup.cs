using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Routing;
using WebApplication1.Services;
using WebApplication1.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            var conn = Configuration["connectionStrings:sqlConnection"];
            services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(conn));
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Entities.Book, Models.BookDTO>();
                config.CreateMap<Models.BookDTO, Entities.Book>();
                config.CreateMap<Entities.Publisher, Models.PublisherDTO>();
                config.CreateMap<Models.PublisherDTO, Entities.Publisher>();
            });

            services.AddScoped(typeof(IBookstoreRepository), typeof(MockBookstoreRepository));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                                IHostingEnvironment env, 
                                ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();
            app.UseMvc(ConfigureRoutes);
            

            //app.Run(async (context) =>
            //{
            //    var message = Configuration["Message"];
            //    var secretMessage = Configuration["SecretMessage"];
            //    await context.Response.WriteAsync(message + "\n");
            //    await context.Response.WriteAsync(secretMessage);
            //});
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{Id?}");
        }


    }
}
