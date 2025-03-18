using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SeikyuWeb.Common;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Middlewares;
using SeikyuWeb.Repositories;
using SeikyuWeb.Repositories.Interfaces;
using SeikyuWeb.Services;
using SeikyuWeb.Services.Interfaces;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor;
using System.IO;
using System.Runtime.InteropServices;

namespace SeikyuWeb
{
    /// <summary>
    /// Startup クラス
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// サービスをコンテナに追加します。
        /// </summary>
        /// <param name="services">サービスコレクション</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                // Configure Json serializer to handle DateTime
                options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
            });
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = "SeikyuSessionData";
                options.Cookie.MaxAge = TimeSpan.FromDays(1);
                options.Cookie.HttpOnly = false;
                options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
                options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;
                options.Cookie.IsEssential = true;
            });

            // Register IHttpContextAccessor
            services.AddHttpContextAccessor();

            // Configure cookie authentication
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "SeikyuAuthenData";
                    options.Cookie.MaxAge = TimeSpan.FromDays(1);
                    options.LoginPath = "/api/login";
                    options.LogoutPath = "/api/logout";
                    options.Cookie.IsEssential = true;
                    options.Cookie.HttpOnly = false;
                    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
                    options.Events.OnRedirectToLogin = context =>
                    {
                        context.Response.Headers["Location"] = context.RedirectUri;
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    };
                });

            // Add authorization policies if needed
            services.AddAuthorization();

            string wkhtmltopdfPath;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                wkhtmltopdfPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "libwkhtmltox.dll");
                CustomAssemblyLoadContext context = new();
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

            services.AddDbContext<HaisyaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<DbContext>();

            // Add Repository here
            services.AddScoped(typeof(IRepositoryBaseAsync<,>), typeof(RepositoryBaseAsync<,>));
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped<ILoginUserRepository, LoginUserRepository>();
            services.AddScoped<ILoginUserCustomerRepository, LoginUserCustomerRepository>();
            services.AddScoped<IPortalInfoRepository, PortalInfoRepository>();
            services.AddScoped<IPrintParameterRepository, PrintParameterRepository>();
            services.AddScoped<IPrintSeikyuRepository, PrintSeikyuRepository>();
            services.AddScoped<IPrintSeikyuDetailRepository, PrintSeikyuDetailRepository>();
            services.AddScoped<ICheckShitabaraiRepository, CheckShitabaraiRepository>();
            services.AddScoped<ICheckSeikyuRepository, CheckSeikyuRepository>();
            services.AddScoped<ICheckSeikyuDetailRepository, CheckSeikyuDetailRepository>();
            services.AddScoped<ICheckSeikyuChangeRepository, CheckSeikyuChangeRepository>();
            services.AddScoped<ICheckSeikyuDoneRepository, CheckSeikyuDoneRepository>();
            services.AddScoped<ISeikyuRepository, SeikyuRepository>();
            services.AddScoped<ICheckShitabaraiDetailRepository, CheckShitabaraiDetailRepository>();
            services.AddScoped<ICheckShitabaraiChangeRepository, CheckShitabaraiChangeRepository>();
            services.AddScoped<ICheckShitabaraiDonerepository, CheckShitabaraiDoneRepository>();
            services.AddScoped<IPrintShitabaraiRepository, PrintShitabaraiRepository>();
            services.AddScoped<IPrintShitabaraiDetailRepository, PrintShitabaraiDetailRepository>();
            services.AddScoped<IUriageShitabaraiRepository, UriageShitabaraiRepository>();
            services.AddScoped<ICompanyUserGroupRepository, CompanyUserGroupRepository>();
            services.AddScoped<ICompanyUserGroupUserRepository, CompanyUserGroupUserRepository>();
            services.AddScoped<ICompanyUserRepository, CompanyUserRepository>();
            services.AddScoped<ICustomerBranchRepository, CustomerBranchRepository>();
            services.AddScoped<ICustomerUriageCalcRepository, CustomerUriageCalcRepository>();
            services.AddScoped<IUnitRepository, UnitRepository>();
            services.AddScoped<IUriageUnchinRepository, UriageUnchinRepository>();
            services.AddScoped<ICodeDataRepository, CodeDataRepository>();
            services.AddScoped<ICustomerTantouRepository, CustomerTantouRepository>();
            services.AddScoped<IReportLayoutRepository, ReportLayoutRepository>();
            services.AddScoped<IReportCommonRepository, ReportCommonRepository>();
            services.AddScoped<ICheckSeikyuRepository, CheckSeikyuRepository>();
            services.AddScoped<INyukinRepository, NyukinRepository>();
            services.AddScoped<IAnkenDetailRepository, AnkenDetailRepository>();
            services.AddScoped<IHaisyaRepository, HaisyaRepository>();
            services.AddScoped<INippouRepository, NippouRepository>();
            services.AddScoped<ISyaryoManagementRepository, SyaryoManagementRepository>();
            services.AddScoped<ISyaryoRepository, SyaryoRepository>();
            services.AddScoped<IUriageRepository, UriageRepository>();

            // Add services here
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPortalInfoService, PortalInfoService>();
            services.AddScoped<ISeikyuService, SeikyuService>();
            services.AddScoped<ICheckShiharaiService, CheckShiharaiService>();
            services.AddScoped<ICheckSeikyuService, CheckSeikyuService>();
            services.AddScoped<IMasterService, MasterService>();
            services.AddScoped<IReportCommonService, ReportCommonService>();

            // Add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SeikyuWeb", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.DocumentFilter<LowerCaseDocumentFilter>();
            });

            services.AddControllersWithViews(options =>
            {
                options.AllowEmptyInputInBodyModelBinding = true;
            });

            // Add Razor Pages services if needed
            services.AddRazorPages();
            // Register DinkToPdf services
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            wkhtmltopdfPath = GetWkhtmltopdfPath();
            if (!string.IsNullOrEmpty(wkhtmltopdfPath))
            {
                CustomAssemblyLoadContext context = new CustomAssemblyLoadContext();
                context.LoadUnmanagedLibrary(wkhtmltopdfPath);
            }
            // Register a service to render Razor views to string
            services.AddScoped<IRazorViewToStringRendererService, RazorViewToStringRendererService>();
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationFormats.Add("/Templates/{0}.cshtml");
            });
        }

        /// <summary>
        /// HTTPリクエストパイプラインを構成します。
        /// </summary>
        /// <param name="app">アプリケーションビルダー</param>
        /// <param name="env">ホスティング環境</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SeikyuWeb v1"));
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Add middleware here
            app.UseMiddleware<ResponseOverwritingMiddleware>();

            app.UseRouting();
            app.UseCors(builder => builder
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .SetIsOriginAllowed(origin => true)
                            .AllowCredentials());
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseMiddleware<SessionMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        /// <summary>
        /// DinkToPdfサービスを登録し、OSに応じてwkhtmltopdfのパスを取得します。
        /// </summary>
        /// <returns>wkhtmltopdfのパス</returns>
        /// <exception cref="PlatformNotSupportedException">サポートされていないプラットフォームの場合にスローされます。</exception>
        private string GetWkhtmltopdfPath()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Console.WriteLine("Skip load wkhtmltopdf library on macOS");
                return string.Empty;
            }
            else
            {
                throw new PlatformNotSupportedException("This platform is not supported");
            }
        }
    }

    /// <summary>
    /// LowerCase DocumentFilter クラス
    /// </summary>
    public class LowerCaseDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// Swaggerドキュメントのパスを小文字に変換します。
        /// </summary>
        /// <param name="swaggerDoc">Swaggerドキュメント</param>
        /// <param name="context">ドキュメントフィルターコンテキスト</param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var path in swaggerDoc.Paths.ToList())
            {
                string newPath = path.Key.ToLower();
                swaggerDoc.Paths.Remove(path.Key);
                swaggerDoc.Paths.Add(newPath, path.Value);
            }
        }
    }
}
