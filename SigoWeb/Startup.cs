using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Refit;
using SigoWeb.Infrastructure.Requests;
using System;
using System.Text;

namespace SigoWeb
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
            services.AddControllersWithViews();

            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddRefitClient<IRefitAuth>()
              .ConfigureHttpClient(c =>
              {
                  c.BaseAddress = new Uri("http://andremagalhaes-001-site2.etempurl.com/api");
              });

            services.AddRefitClient<IRefitNormas>()
              .ConfigureHttpClient(c =>
              {
                  c.BaseAddress = new Uri("http://andremagalhaes-001-site2.etempurl.com/api");
              });

            services.AddRefitClient<IRefitConsultorias>()
              .ConfigureHttpClient(c =>
              {
                  c.BaseAddress = new Uri("http://andremagalhaes-001-site2.etempurl.com/api");
              });

            //Token JWT
            var key = Encoding.ASCII.GetBytes("CdWiH4fu7byjWwzgIzRa9PGHM7WdhpZr0F0_3a-F71LdGu-BSw0ZVOOLw5quQgD080b7eK1sJqg3jaRU9hwtfPyAZG9STaMPg4DdmQlmi6EUbcSjdBeKmTdGCZ8wkEltDWj1p51otfaWrdxICzCFZ6bVvjTzdWI-dQXMFCIXACP-aN1cAL1JewNbFetnAkA9c9Z3hgJLmGYQTC3LUkvVAQXxm4J_RRE7v5kWKbn0BPziJQF-_sedgHrIP0zpsHU7g1Ztch0tLwIu7NiXe3-gCrWVLhe2tUB6IkxLjD3eMEXNdUI2uy6Croa_sXyX6lT4npYJnJ1lChSZwiFqUgK_");

            //Authentication
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie("token")
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Auth}/{action=Login}/{id?}");
            });
        }
    }
}
