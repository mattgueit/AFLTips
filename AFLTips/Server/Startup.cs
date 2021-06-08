using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using AFLTips.Server.Repositories;
using AFLTips.Server.Repositories.Interfaces;
using AFLTips.Server.Services;
using AFLTips.Server.Services.Interfaces;
using AFLTips.Shared.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AFLTips.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // default app configuration
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddMemoryCache();


            // DI registrations
            services.AddTransient<IFixtureService, FixtureService>();
            services.AddTransient<ILadderService, LadderService>();
            services.AddTransient<IPlayerService, PlayerService>();
            services.AddTransient<ITipService, TipService>();

            services.AddTransient<IFixtureRepository, FixtureRepository>();
            services.AddTransient<IPlayerRepository, PlayerRepository>();
            services.AddTransient<ITipRepository, TipRepository>();

            // Sql Server config
            var sqlConfig = new SqlDbConfiguration(Configuration.GetConnectionString("default"));
            services.AddSingleton(sqlConfig);


            // HttpClient configuration
            var squiggleEndPoint = new Uri(Configuration.GetValue<string>("SquiggleAPI"));
            var httpClient = new HttpClient()
            {
                BaseAddress = squiggleEndPoint
            };

            var userAgentProductValue = new ProductInfoHeaderValue("AFLTipsWebApp", "1.0");
            var userAgentCommentValue = new ProductInfoHeaderValue("(+https://github.com/mattgueit/AFLTips)");
            httpClient.DefaultRequestHeaders.UserAgent.Add(userAgentProductValue);
            httpClient.DefaultRequestHeaders.UserAgent.Add(userAgentCommentValue);

            ServicePointManager.FindServicePoint(squiggleEndPoint);

            services.AddSingleton<HttpClient>(httpClient);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
