using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PartnerWeb.Data;
using PartnerWeb.Models;
using PartnerWeb.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerWeb
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
            services.AddDbContext<ApplicationDbContext>();
            services.AddDatabaseDeveloperPageExceptionFilter();

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddIdentity<ApplicationUser, IdentityRole>().AddUserStore<ApplicationUserStore>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            //services.AddIdentity<ApplicationUser, IdentityRole>().AddUserStore<ApplicationUserStore>();

            services.AddControllersWithViews();

            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddSingleton<IPathProvider, PathProvider>();

            services.Configure<MapApiSettings>(Configuration.GetSection(MapApiSettings.MapApiSetting));

            //services.Configure<Microsoft.AspNetCore.Mvc.Razor.RazorViewEngineOptions>(o => { 
            //    o.ViewLocationFormats.Add("/Views/{0}" + Microsoft.AspNetCore.Mvc.Razor.RazorViewEngine.ViewExtension);
            //});

            //// セッションを使う
            services.AddSession(options => { options.Cookie.Name = "session"; });

            services.AddRazorPages();
            services.AddRazorPages().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            // コンテキストマネージャーの初期化
            Context.ContextManager.Initialize();
        }

        internal interface IPathProvider
        {
            string MapPath(string path);
        }

        public class PathProvider : IPathProvider
        {
            private IWebHostEnvironment _hostEnvironment;

            public PathProvider(IWebHostEnvironment environment)
            {
                _hostEnvironment = environment;
            }

            public string MapPath(string path)
            {
                string filePath = Path.Combine(_hostEnvironment.WebRootPath, path);
                return filePath;
            }
        }
    }
}
