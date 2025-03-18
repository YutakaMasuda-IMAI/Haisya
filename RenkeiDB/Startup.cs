using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RenkeiDB.Common;
using RenkeiDB.Data;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Middlewares;
using RenkeiDB.Repositories;
using RenkeiDB.Repositories.Interfaces;
using RenkeiDB.Services;
using RenkeiDB.Services.Interfaces;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using RenkeiDB.Context;
using RenkeiDB.Service;

namespace RenkeiDB
{
    /// <summary>
    /// アプリケーションのスタートアップクラス
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="configuration">アプリケーション構成</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// アプリケーション構成
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// サービスをコンテナに追加するメソッド
        /// </summary>
        /// <param name="services">サービスコレクション</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                // Configure Json serializer to handle DateTime
                options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
            });

            services.AddScoped<IViewRenderService, ViewRenderService>();

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
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
                    options.Cookie.Name = "RenkeiSession";
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
                RenkeiDB.Context.CustomAssemblyLoadContext context = new();
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

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Add services here
            services.AddScoped(typeof(IRepositoryBaseAsync<,>), typeof(RepositoryBaseAsync<,>));
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMasterDataService, MasterDataService>();
            services.AddScoped<IShareSyaryoService, ShareSyaryoService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<ILuggageService, LuggageService>();
            services.AddScoped<IShareLuggageNotifySettingService, ShareLuggageNotifySettingService>();
            services.AddScoped<IInfoService, InfoService>();
            services.AddScoped<IHaisyaNotifySettingService, HaisyaNotifySettingService>();
            services.AddScoped<ICustomerPortalService, CustomerPortalService>();
            services.AddScoped<ICarInfoService, CarInfoService>();
            services.AddScoped<ICompanyPortalService, CompanyPortalService>();
            services.AddScoped<IMasterEquipmentsService, MasterEquipmentsService>();
            services.AddScoped<IAnkensService, AnkensService>();
            services.AddScoped<IMapPointService, MapPointService>();
            services.AddScoped<IShareSyaryoNotifySettingService, ShareSyaryoNotifySettingSerrvice>();
            services.AddScoped<IMasterLuggagesService, MasterLuggagesService>();
            services.AddScoped<IPdfService, PdfService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IDefaultMoneyService, DefaultMoneyService>();


            // Add repositories here
            services.AddScoped<ILoginUserRepository, LoginUserRepository>();
            services.AddScoped<ILuggageNotifySettingRepository, LuggageNotifySettingRepository>();
            services.AddScoped<ISyaryoNotifySettingRepository, SyaryoNotifySettingRepository>();
            services.AddScoped<IMasterCodeDataRepository, MasterCodeDataRepository>();
            services.AddScoped<IShareLuggageRepository, ShareLuggageRepository>();
            services.AddScoped<IShareLuggageDetailRepository, ShareLuggageDetailRepository>();
            services.AddScoped<IShareSyaryoRepository, ShareSyaryoRepository>();
            services.AddScoped<IShareSyaryoDetailRepository, ShareSyaryoDetailRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IPostCodeRepository, PostCodeReponsitory>();
            services.AddScoped<ICompanyUserRepository, CompanyUserRepository>();
            services.AddScoped<IPortalInfoRepository, PortalInfoRepository>();
            services.AddScoped<IShareNoRepository, ShareNoRepository>();
            services.AddScoped<ICustomerPortalRepository, CustomerPortalRepository>();
            services.AddScoped<ICarInfoRepository, CarInfoRepository>();
            services.AddScoped<ICompanyUserGroupRepository, CompanyUserGroupRepository>();
            services.AddScoped<ISyaryoRepository, SyaryoRepository>();
            services.AddScoped<ICompanyPortalRepository, CompanyPortalRepository>();
            services.AddScoped<ICompanyPortalService, CompanyPortalService>();
            services.AddScoped<ILuggageRepository, LuggageRepository>();
            services.AddScoped<IEquipmentRepository, EquipmentRepository>();
            services.AddScoped<IEquipmentGroupRepository, EquipmentGroupRepository>();
            services.AddScoped<IMasterEquipmentRepository, MasterEquipmentRepository>();
            services.AddScoped<IMasterLuggageRepository, MasterLuggageRepository>();
            services.AddScoped<IMasterSyaryoRepository, MasterSyaryoRepository>();
            services.AddScoped<IRenkeiAnkenRepository, RenkeiAnkenRepository>();
            services.AddScoped<IRenkeiAnkenDetailRepository, RenkeiAnkenDetailRepository>();
            services.AddScoped<IShareSyaryoSecureRepository, ShareSyaryoSecureRepository>();
            services.AddScoped<IRenkeiAnkenDetailRepository, RenkeiAnkenDetailRepository>();
            services.AddScoped<IRenkeiAnkenLuggageRepository, RenkeiAnkenLuggageRepository>();
            services.AddScoped<IRenkeiAnkenEquipmentRepository, RenkeiAnkenEquipmentRepository>();
            services.AddScoped<IRenkeiAnkenPointRepository, RenkeiAnkenPointRepository>();
            services.AddScoped<IRenkeiAnkenCheckRepository, RenkeiAnkenCheckRepository>();
            services.AddScoped<IRenkeiAnkenSecureCheckRepository, RenkeiAnkenSecureCheckRepository>();
            services.AddScoped<IShareLuggageSecureRepository, ShareLuggageSecureRepository>();
            services.AddScoped<IMapPointRepository, MapPointRepository>();
            services.AddScoped<IDefaultMoneyRepository, DefaultMoneyRepository>();


            services.AddScoped<IOperationInstructionService, OperationInstructionService>();
            services.AddScoped<IOperationInstructionRepository, OperationInstructionRepository>();

            // Add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RenkeiDB", Version = "v1" });
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
                RenkeiDB.Context.CustomAssemblyLoadContext context = new RenkeiDB.Context.CustomAssemblyLoadContext();
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
        /// HTTPリクエストパイプラインを構成するメソッド
        /// </summary>
        /// <param name="app">アプリケーションビルダー</param>
        /// <param name="env">ホスティング環境</param>
        /// <param name="context">アプリケーションDBコンテキスト</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RenkeiDB v1"));
            }

            app.UseHttpsRedirection();

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
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            try
            {
                Dto.AddressListDto dto = Dto.AddressListDto.GetInstance();
                dto.SetAddressList(context);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw; 
            }
        }

        /// <summary>
        /// DinkToPdfサービスを登録し、OSに応じてwkhtmltopdfのパスを取得する
        /// </summary>
        /// <returns>wkhtmltopdfのパス</returns>
        /// <exception cref="PlatformNotSupportedException">サポートされていないプラットフォームの場合にスローされる例外</exception>
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
}
