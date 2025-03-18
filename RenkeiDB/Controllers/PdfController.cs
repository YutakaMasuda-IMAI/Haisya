using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RenkeiDB.Common;
using RenkeiDB.Dto;
using RenkeiDB.Dto.PdfDto;
using RenkeiDB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RenkeiDB.Controllers
{
    /// <summary>
    /// PDFに関する操作を提供するコントローラー
    /// </summary>
    [Route("api")]
    [ApiController]
    [Authorize]
    public class PdfController : ControllerBase
    {
        private readonly ILogger<PdfController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConverter _converter;
        private readonly IRazorViewToStringRendererService _razorViewToStringRendererService;
        private readonly IPdfService _pdfService;

        public PdfController(ILogger<PdfController> logger,
            IHttpContextAccessor httpContextAccessor,
            IConverter converter,
            IRazorViewToStringRendererService razorViewToStringRendererService,
            IPdfService pdfService)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _converter = converter;
            _razorViewToStringRendererService = razorViewToStringRendererService;
            _pdfService = pdfService;
        }

        /// <summary>
        /// 車番連絡票印刷（PDF形式）
        /// </summary>
        /// <param name=""></param>
        /// <returns>
        /// 200: 成功
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpGet("syaban-notify/print")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> PrintSyabanNotify()
        {
            try
            {
                string companyId = User.FindFirst("CompanyId")?.Value;
                string branchId = User.FindFirst("BranchId")?.Value;
                if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(branchId)
                    || !int.TryParse(companyId, out int idCompanyInt)
                    || !int.TryParse(branchId, out int idBranchInt)
                    || idCompanyInt == 0 || idBranchInt == 0)
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.NotFound, SystemConstants.Message.DataNotFound);
                }

                IEnumerable<ContactCarNumberGroupDto> data = await _pdfService.PrintSyabanNotifyAsync(idCompanyInt, idBranchInt);

                if (!data.Any())
                {
                    return Ok(new { });
                }

                string htmlContent = await _razorViewToStringRendererService.RenderViewToStringAsync("/Templates/PDFReportCarNumberContactSheet.cshtml", data);

                GlobalSettings globalSettings = new()
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10, Bottom = 10, Left = 10, Right = 10 },
                    DPI = 96,
                };

                ObjectSettings objectSettings = new()
                {
                    PagesCount = true,
                    HtmlContent = htmlContent,
                    WebSettings = { DefaultEncoding = "utf-8" },
                    HeaderSettings = { },
                    FooterSettings = new FooterSettings
                    {
                        FontSize = 9,
                        Center = "頁 [page]",
                    }
                };

                HtmlToPdfDocument pdf = new()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };

                byte[] file = _converter.Convert(pdf);

                using (MemoryStream stream = new())
                {
                    stream.Write(file, 0, file.Length);
                    return File(stream.ToArray(), "application/pdf", "ContactNumberCar.pdf");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }
    }
}
