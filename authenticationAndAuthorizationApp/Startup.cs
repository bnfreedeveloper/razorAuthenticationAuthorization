using authenticationAndAuthorizationApp.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace authenticationAndAuthorizationApp
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
            services.AddRazorPages();
            services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth",options =>
                {
                    options.Cookie.Name = "CookieAuth";

                    //possible configuration for cookie lifetime 
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("BelongToDepartment", policy =>
                 {
                     policy.RequireClaim("Department", "HR");
                 });
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
                options.AddPolicy("ManagerOnly", policy =>
                {
                    policy.RequireClaim("Department").RequireClaim("Manager")
                    .Requirements.Add(new ManagementRequirement(3));
                });
            });
            services.AddSingleton<IAuthorizationHandler,RequirementHandler>();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //here the authentication middleware will populate the user/security context from the cookie
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
