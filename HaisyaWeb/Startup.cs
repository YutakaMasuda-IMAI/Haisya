using DinkToPdf;
using DinkToPdf.Contracts;
using HaisyaWeb.Context;
using HaisyaWeb.Models;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace HaisyaWeb
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
            services.AddDbContext<Data.ApplicationDbContext>();
            services.AddDatabaseDeveloperPageExceptionFilter();

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddIdentity<ApplicationUser, IdentityRole>().AddUserStore<ApplicationUserStore>().AddEntityFrameworkStores<Data.ApplicationDbContext>().AddDefaultTokenProviders();

            services.AddControllersWithViews();

            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddSingleton<IPathProvider, PathProvider>();



            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
            });


            services.Configure<MapApiSettings>(Configuration.GetSection(MapApiSettings.MapApiSetting));

            //// セッションを使う
            services.AddSession(options => { options.Cookie.Name = "HaisyaSession"; options.IdleTimeout = TimeSpan.FromMinutes(120); });

            services.AddRazorPages();
            services.AddRazorPages().AddRazorRuntimeCompilation();

            string[] allowedOrigins = Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
            if (allowedOrigins == null)
            {
                allowedOrigins = new string[] { "*" };
            }

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins(allowedOrigins)
                                    .AllowAnyHeader()
                                    .AllowAnyMethod());
            });

            //CookieAuthenticationDefaults.AuthenticationSchemeのスキーム名は別の文字に変更することも可能
            //例えば：MyLoginScheme
            //だが変更され場合は利用する際はスキーム名を変更されたスキーム名と一致する必要があります。
            //.AddCookieでは認証用のCookieに関する設定です
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, option =>
                {
                    //AuthenticationSchemeという認証スキームの共通設定

                    //HttpContext.ChallengeAsync(認証チャレンジ)で認証失敗した場合のリダイレクト先
                    //簡単に言うとログインされていない時にログインが必要の機能へアクセスする時のリダイレクト先
                    option.LoginPath = "/Account/Login";
                    //HttpContext.ForbidAsync（認証されているが、必要な権限がない時の禁止）でのリダイレクト先
                    //ログインされている、だがアクセスする機能に必要な権限が足りない時のリダイレクト先
                    option.AccessDeniedPath = "/Account/Forbid";

                    //該当スキームのCookie名、デフォルトは.AspNetCore.Cookies
                    option.Cookie.Name = "HaisyaWeb";
                    //Cookieの中に保存されている認証データの有効期限、ここでは5分以内にサーバーへアクセスがないと認証タイムアウトが発生する
                    option.ExpireTimeSpan = TimeSpan.FromMinutes(100);
                    //その他の設定に関しては省略する
                });

            string wkhtmltopdfPath;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                wkhtmltopdfPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "libwkhtmltox.dll");
                CustomAssemblyLoadContext context = new CustomAssemblyLoadContext();
                context.LoadUnmanagedLibrary(wkhtmltopdfPath);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                wkhtmltopdfPath = "";
                Console.WriteLine("Skip load wkhtmltopdf library on macos");
            }
            else
            {
                throw new PlatformNotSupportedException("This platform is not supported");
            }
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(120);

                options.LoginPath = "/Account/Login";
                //options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
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
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("AllowSpecificOrigin");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=SignIn}/{id?}");
                endpoints.MapRazorPages();
            });

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
