using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using TheStorageApp.Website.Services;
using TheStorageApp.Website.Utils;

namespace TheStorageApp.Website
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
            services.AddRazorPages();//.AddJsonOptions(o => { o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve; });
            services.AddServerSideBlazor();
            services.AddControllersWithViews();//.AddJsonOptions(o => { o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve; }); ;

            string uri = string.Empty;
            if (Debugger.IsAttached)
                uri = Configuration.GetValue<string>("WebapiEndpointDebug");
            else
                uri = Configuration.GetValue<string>("WebapiEndpointRelease");

            services.AddHttpClient();
            services.AddHttpClient("TGSClient", endpoint => endpoint.BaseAddress = new Uri(uri));

            services.AddHttpContextAccessor();

            services.AddScoped<AuthorizationController>();
            services.AddScoped<UsersService>();
            services.AddScoped<RolesService>();
            services.AddScoped<CookieController>();            
            services.AddScoped<ReceiptsService>();
            services.AddScoped<CategoriesService>();
            services.AddScoped<ShopsService>();
            services.AddScoped<TagsService>();
            services.AddScoped<ServerControllerService>();
            services.AddScoped<ModelService>();
            services.AddScoped<FieldService>();

            services.AddMemoryCache();
            services.AddSession();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
                endpoints.MapDefaultControllerRoute();
            });

            app.UseSession();
        }
    }
}
