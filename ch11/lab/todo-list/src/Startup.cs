using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToDoList.Model;
using ToDoList.Services;

namespace ToDoList
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            var dbProvider = Configuration.GetValue<DbProvider>("Database:Provider");
            _ = dbProvider switch
            {
                DbProvider.Sqlite => services.AddDbContext<ToDoContext>(options =>
                     options.UseSqlite(Configuration.GetConnectionString("ToDoDb"))),

                DbProvider.Postgres => services.AddDbContext<ToDoContext>(options =>
                     options.UseNpgsql(Configuration.GetConnectionString("ToDoDb"),
                     postgresOptions => postgresOptions.EnableRetryOnFailure())),

                _ => throw new NotSupportedException("Supported providers: Sqlite and Posgtres")
            };

            services.AddScoped<ToDoService>();
            services.AddSingleton<DiagnosticsService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");                
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
