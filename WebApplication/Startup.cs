using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using WebApplication.Data;
using WebApplication.Data.Kintai;
using WebApplication.Data.NPData;
using WebApplication.Model;
using WebApplication.Repositories;
using WebApplication.Services;

namespace WebApplication
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
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ApplicationDbContextKintai>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionkintai")));
            services.AddDbContext<ApplicationDbContextNPData>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionNPData")));
            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            services.AddScoped<ISyabanRenrakuService, SyabanRenrakuService>();
            services.AddScoped<ISyabanRenrakuRepository, SyabanRenrakuRepository>();
            services.AddScoped<IReceiptService, ReceiptService>();
            services.AddScoped<IReceiptRepository, ReceiptRepository>();
            services.AddScoped<IDailyReportRegistrationDetailService, DailyReportRegistrationDetailService>();
            services.AddScoped<IDailyReportRegistrationDetailRepository, DailyReportRegistrationDetailRepository>();
            services.AddScoped<IOperationInstructionsService, OperationInstructionsService>();
            services.AddScoped<IOperationInstructionsRepository, OperationInstructionsRepository>();
            services.AddScoped<ISalesService, SalesService>();
            services.AddScoped<ISalesRepository, SalesRepository>();
            services.AddScoped<IDailyReportService, DailyReportService>();
            services.AddScoped<IDailyReportRepository, DailyReportRepository>();

            services.AddScoped<IUriageDataListRepository, UriageDataListRepository>();
            services.AddScoped<IUriageDataListService, UriageDataListService>();
            services.AddScoped<IShitabaraiService, ShitabaraiService>();
            services.AddScoped<IShitabaraiRepository, ShitabaraiRepository>();
            services.AddScoped<ReportCommonDataModel>();
            services.AddScoped<IReportCommonService, ReportCommonService>();
            services.AddScoped<IReportCommonRepository, ReportCommonRepository>();

            services.AddScoped<IShitabaraiInquiryModifyListService, ShitabaraiInquiryModifyListService>();
            services.AddScoped<IShitabaraiInquiryModifyListRepository, ShitabaraiInquiryModifyListRepository>();

            services.AddScoped<ICompletedPaymentsRepository, CompletedPaymentsRepository>();
            services.AddScoped<ICompletedPaymentsService, CompletedPaymentsService>();

            services.AddScoped<IPurchasePaymentRegistrationRepository, PurchasePaymentRegistrationRepository>();
            services.AddScoped<IPurchasePaymentRegistrationService, PurchasePaymentRegistrationService>();
            services.AddScoped<IAccidentListRepository, AccidentListRepository>();
            services.AddScoped<IAccidentListService, AccidentListService>();
            services.AddScoped<IAccidentRepository, AccidentRepository>();
            services.AddScoped<IAccidentService, AccidentService>();

            services.AddDbContext<DbContext>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication", Version = "v1" });
            });

            services.Configure<MapApiSettings>(Configuration.GetSection(MapApiSettings.MapApiSetting));

            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            try
            {
                Dto.AddressListDto dto = Dto.AddressListDto.GetInstance();
                dto.SetAddressList(context);
            }
            catch (Exception) { throw; }
        }
    }
}
