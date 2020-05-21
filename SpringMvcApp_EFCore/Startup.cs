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
using SpringMvcApp.BusinessLayer.Interfaces;
using SpringMvcApp.BusinessLayer.Services;
using SpringMvcApp.BusinessLayer.Services.Repository;
using SpringMvcApp.DataLeyer;

using SpringMvcApp_EFCore.Controllers;

namespace SpringMvcApp_EFCore
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // services.AddDbContext<SpringMvcAppDbContext>(options => options.UseInMemoryDatabase(databaseName: "SpringMvcApp"));
            services.AddSingleton<IDbConnectionFactory>(x =>
                 new SqLiteConnectionFactory(Configuration.GetValue<string>("Database:DbLocation")));
            // services.AddTransient<IUserServices, UserServices>();
            // services.AddTransient<IDbConnectionFactory, SqLiteConnectionFactory>();
            services.AddSingleton<IAdminRepository, AdminRepository>();
            services.AddTransient<IAdminServices, AdminServices>();
          //  services.AddTransient<IUserRepository, UserRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IUserServices, UserServices>();
           


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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=User}/{action=AllUsers}/{id?}");
            });
        }
    }
}
