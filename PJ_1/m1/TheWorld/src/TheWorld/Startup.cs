using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using TheWorld.ViewModels;
using TheWorld.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Middleware
/// </summary>
namespace TheWorld
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private IConfigurationRoot _config;

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            _config = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
           services.AddSingleton(_config);

            if (_env.IsEnvironment("Development"))
                services.AddScoped<Services.IMailServices, Services.DebugMailService>();
            else
            {
                services.AddMvc(c =>
                    {
                        c.Filters.Add(new RequireHttpsAttribute());
                    }
                );
            }

            services.AddIdentity<WorldUser, IdentityRole>(c =>
            {
                c.User.RequireUniqueEmail = true;
                c.Password.RequiredLength = 8;
                c.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";
            }
            ).AddEntityFrameworkStores<Models.WordContext>();

            services.AddDbContext<Models.WordContext>();
            services.AddTransient<Models.WorldContextSeddData>();
            services.AddTransient<Services.GeoCoordService>();
            services.AddScoped<Models.IWorldRepository, Models.WorldRepository>(); //using IWorldRepository interface cause the real instance is expensive and also it is possible to use test classes instead (eg TestWorldRepository implementing IWorldRepository)
            services.AddLogging();
            services.AddMvc()
                            .AddJsonOptions(c =>
                                c.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver()
                                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Models.WorldContextSeddData seeder, ILoggerFactory factory)
        {
            // order matters!!
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                factory.AddDebug(LogLevel.Information);
            }
            else
            {
                factory.AddDebug(LogLevel.Error);
            }
            
            app.UseStaticFiles();
            app.UseIdentity(); //turn on 
            app.UseMvc(config =>
            {
                config.MapRoute(name: "Default",
                template: "{controller}/{action}/{id?}",
                defaults: new { controller = "App", action = "Index" }
                );
            });

            Mapper.Initialize(c => {
                c.CreateMap<TripViewModel, Trip>().ReverseMap();
                c.CreateMap<StopViewModel, Stop>().ReverseMap();
                }
              ); //Tip: Can map diff prop names

            seeder.EnsureData().Wait();
        }
    }
}
