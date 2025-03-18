using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeikyuWeb.Common;
using SeikyuWeb.Dto;
using SeikyuWeb.Dto.ReportDto;
using SeikyuWeb.Dto.Seikyu;
using SeikyuWeb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using static SeikyuWeb.Common.SystemConstants;
using static SeikyuWeb.Common.SystemEnums;
using static SeikyuWeb.Dto.ReportDto.ReportDto;
using System.Linq;
using SeikyuWeb.Models;
using System.Text;

namespace SeikyuWeb.Controllers
{
    /// <summary>
    /// 請求コントローラークラス
    /// </summary>
    [ApiController]
    [Route("api")]
    [Authorize]
    public class SeikyuController : ControllerBase
    {
        private readonly ILogger<SeikyuController> _logger;
        private readonly IConverter _converter;
        private readonly ISeikyuService _seikyuService;
        private readonly IPortalInfoService _portalInfoService;
        private readonly IReportCommonService _reportCommonService;
        private readonly IRazorViewToStringRendererService _razorViewToStringRendererService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SeikyuController(ILogger<SeikyuController> logger,
            IConverter converter,
            ISeikyuService seikyuService,
            ICheckSeikyuService checkSeikyuService,
            IReportCommonService reportCommonService,
            IPortalInfoService portalInfoService,
            IRazorViewToStringRendererService razorViewToStringRendererService,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _converter = converter;
            _seikyuService = seikyuService;
            _portalInfoService = portalInfoService;
            _reportCommonService = reportCommonService;
            _razorViewToStringRendererService = razorViewToStringRendererService;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 請求書一覧の取得
        /// </summary>
        /// <param name="dto">請求書リクエストDTO</param>
        /// <returns>
        /// 200: 成功
        /// 400: パラメータが不正となります。
        /// 401: 許可されていません。
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpGet("seikyus")]
        [ProducesResponseType(typeof(List<InvoiceDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetInvoices([FromQuery] SeikyuReqDto dto)
        {
            try
            {
                ISession session = _httpContextAccessor.HttpContext.Session;
                (string id, bool isCompany) = GetSessionIdentifiers(session);

                DateTime? fromYmd = dto.fromYm != null ? DateTime.ParseExact(dto.fromYm, "yyyy/MM", CultureInfo.InvariantCulture) : null;
                DateTime? toYmd = dto.toYm != null ? DateTime.ParseExact(dto.toYm, "yyyy/MM", CultureInfo.InvariantCulture) : null;

                IEnumerable<InvoiceDto> data = await _seikyuService.GetInvoiceList(int.Parse(id), isCompany, fromYmd, toYmd, dto.shimeDay, dto.zeiKubun ?? -1, dto.status ?? -1);

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// 請求書発行の取得
        /// </summary>
        /// <param name="print_seikyu_id">print_seikyu_id</param>
        /// <returns>
        /// 200: 成功
        /// </returns>
        [HttpGet("seikyus/{print_seikyu_id}")]
        [ProducesResponseType(typeof(InvoiceAndReportLayoutDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetInvoiceDetail(int print_seikyu_id)
        {
            try
            {
                ISession session = _httpContextAccessor.HttpContext.Session;
                (string id, bool isCompany) = GetSessionIdentifiers(session);

                if (string.IsNullOrEmpty(id) || !int.TryParse(id, out int idInt) || idInt == 0)
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.NotFound, SystemConstants.Message.DataNotFound);
                }

                InvoiceAndReportLayoutDto data = await _seikyuService.GetInvoiceDetailAsync(print_seikyu_id, idInt, isCompany);
                if (data?.portalInfoIds.Count != 0)
                {
                    await _portalInfoService.UpdateDisplayFlag(data.portalInfoIds);
                }

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// 請求書 レポート出力
        /// </summary>
        /// <param name="searchParam">検索パラメータのJSON</param>
        /// <returns>
        /// 200: PDFファイルのストリーム
        /// 404: ページが見つかりません。
        /// 401: 許可されていません。
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpPost("seikyus/report")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Report([FromBody] SearchExportDto searchParam)
        {
            try
            {
                string userId = User.FindFirst("UserId")?.Value;
                string companyId = User.FindFirst("UserId")?.Value;

                if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int userIdParse) || userIdParse == 0)
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.NotFound, Message.DataNotFound);
                }

                if (string.IsNullOrEmpty(companyId) || !int.TryParse(companyId, out int companyIdParse) || companyIdParse == 0)
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.NotFound, Message.DataNotFound);
                }

                Dictionary<string, object> jsonData = new Dictionary<string, object>();
                jsonData["seikyuID"] = searchParam.seikyuID;

                TReportLayout reportLayout = await _reportCommonService.GetReportLayout(searchParam.reportType ?? 0, searchParam.searchKubunID ?? 0);
                if (reportLayout == null || reportLayout.ReportSerchId != (int)ReportType.SEIKYUSHO)
                {
                    return Ok(new { });
                }

                return reportLayout.CsvOutputFlg == 0 
                    ? await PrintPDF(searchParam, jsonData, userIdParse, companyIdParse, reportLayout.ReportSerchId) 
                    : await PrintCSV(searchParam, jsonData, userIdParse, companyIdParse, reportLayout.ReportSerchId);
            }
            catch (BadHttpRequestException ex)
            {
                _logger.LogError(ex, Message.Error);
                if (ex.Message == Message.DataNotFound || ex.Message == Message.NoDataOutput)
                {
                    return Ok(new { });
                }

                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, Message.InternalServerError);
            }
        }

        #region Private Methods
        /// <summary>
        /// セッション識別子の取得
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        private static (string id, bool isCompany) GetSessionIdentifiers(ISession session)
        {
            string companyId = session.GetString("companyId");
            string tantouId = session.GetString("tantouId");
            return
                !string.IsNullOrEmpty(companyId) ? (companyId, true) :
                !string.IsNullOrEmpty(tantouId) ? (tantouId, false) : (null, false);
        }

        /// <summary>
        /// ReportHtmlでテンプレートcshtmlを変換
        /// </summary>
        /// <param name="ReportHtml">帳票Htmlフィールド</param>
        /// <returns>pdfテンプレート名</returns>
        private string ConvertReportHtml(string ReportHtml)
        {
            string result = "";
            for (int i = 0; i < ReportHtml.Length; i++)
            {
                if (i == 0)
                {
                    result += ReportHtml[i].ToString().ToUpper();
                }
                else if (ReportHtml[i] == '-')
                {
                    i++;
                    result += ReportHtml[i].ToString().ToUpper();
                }
                else
                {
                    result += ReportHtml[i].ToString();
                }
            }
            return "PDFReport" + result;
        }

        private async Task<IActionResult> PrintPDF(SearchExportDto searchParam, Dictionary<string, object> jsonData, int userId, int companyId, int reportType)
        {
            // Initialize data model
            PDFReportModel model = new PDFReportModel();

            // Get data report common by Report_Serch_Kubun_Id
            ReportCommonDto dataReport = await _reportCommonService.GetReportCommonAsync(searchParam.searchKubunID ?? 0, jsonData, userId, companyId, reportType, true);

            // Get Model PDF Report
            model = await _reportCommonService.GetModelPDFReport(dataReport, companyId, searchParam.seikyuID ?? 0);

            if (model.ReportSearchKubun == null)
            {
                return Ok(new { });
            }

            if (!ReportCommon.ReportHtmlList.Any(s => s.Equals(model.ReportSearchKubun.ReportHtml, StringComparison.OrdinalIgnoreCase)))
            {
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, Message.ReportHtmlInvalid);
            }

            if (model.ReportSearchKubun.Report_Serch.PdfFlg == 0)
            {
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, Message.PDFInvalid);
            }

            // Convert report html
            string ReportHtml = ConvertReportHtml(model.ReportSearchKubun.ReportHtml);

            // get html view from razor
            string htmlView = await _razorViewToStringRendererService.RenderViewAfterExecuseJSToStringAsync("/Templates/" + ReportHtml + ".cshtml", model);

            // convert html to PDF
            HtmlToPdfDocument doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4,
                    DPI = 96,
                },
                Objects = {
                    new ObjectSettings() {
                        HtmlContent = htmlView,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        FooterSettings = new FooterSettings
                        {
                            FontSize = 9,
                            Center = "頁 [page]",
                        }
                    }
                }
            };

            byte[] pdf = _converter.Convert(doc);

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(pdf, 0, pdf.Length);
                return File(stream.ToArray(), "application/pdf", model.ReportSearchKubun.ReportHtml + ".pdf");
            }
        }

        private async Task<IActionResult> PrintCSV(SearchExportDto searchParam, Dictionary<string, object> jsonData, int userId, int companyId, int reportType)
        {
            // Initialize data model
            PDFReportModel model = new PDFReportModel();

            // Get data report common by Report_Serch_Kubun_Id
            ReportCommonDto dataReport = await _reportCommonService.GetReportCommonAsync(searchParam.searchKubunID ?? 0, jsonData, userId, companyId, reportType, false);

            // Get Model PDF Report
            model = await _reportCommonService.GetModelPDFReport(dataReport, companyId, searchParam.seikyuID ?? 0);

            if (model.ReportSearchKubun == null)
            {
                return Ok(new { });
            }

            if (!ReportCommon.ReportHtmlList.Any(s => s.Equals(model.ReportSearchKubun.ReportHtml, StringComparison.OrdinalIgnoreCase)))
            {
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, Message.ReportHtmlInvalid);
            }

            if (model.ReportOutputItemList.Count() == 0)
            {
                throw new BadHttpRequestException(Message.NoDataOutput);
            }

            using MemoryStream memoryStream = new MemoryStream();
            using StreamWriter writer = new StreamWriter(memoryStream, Encoding.GetEncoding("shift_jis"));
            IOrderedEnumerable<MReportOutputItem> listHeader = model.ReportOutputItemList
                .OrderBy(x => x.SortOrder);
            string header = listHeader.Select(x => "\"" + (x.CsvTitle != null && x.CsvTitle != "" ? x.CsvTitle : x.ReportOutputItemMaster.DisplayTitle) + "\"")
                .Aggregate((x, y) => x + "," + y);
            writer.WriteLine(header);

            foreach (var item in model.ReportCommonList)
            {
                string dataRow = "";
                foreach (var headerItem in listHeader)
                {
                    bool isFormat = headerItem.ReportOutputItemMaster.DisplayFormat != null && headerItem.ReportOutputItemMaster.DisplayFormat != "";
                    string value = item.getValueDynamic(headerItem.ReportOutputItemMaster, false, isFormat).ToString();
                    value = "\"" + value + "\"";
                    dataRow += value + ",";
                }
                dataRow = dataRow.TrimEnd(',');
                writer.WriteLine(dataRow);
            }

            writer.Flush();
            memoryStream.Position = 0;
            string fileName = model.ReportSearchKubun.Report_Serch.CsvFileName == null || model.ReportSearchKubun.Report_Serch.CsvFileName == ""
                ? "report" : model.ReportSearchKubun.Report_Serch.CsvFileName;
            return File(memoryStream.ToArray(), "application/octet-stream", fileName + ".csv");
        }
        #endregion Private Methods
    }
}
