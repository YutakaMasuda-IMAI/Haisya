using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RenkeiDB.Common;
using RenkeiDB.Data;
using RenkeiDB.Dto;
using RenkeiDB.Dto.EmptyCarDto;
using RenkeiDB.Dto.PortalDto;
using RenkeiDB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static RenkeiDB.Common.SystemConstants;

namespace RenkeiDB.Controllers
{
    /// <summary>
    /// 空車車両に関する操作を提供するコントローラー
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/empty-cars")]
    public class EmptyCarController : ControllerBase
    {
        private readonly ILogger<SettingsController> _logger;
        private readonly IConverter _converter;
        private readonly IRazorViewToStringRendererService _razorViewToStringRendererService;
        private IShareSyaryoService _shareSyaryoService;
        private IAnkensService _ankensService;

        public EmptyCarController(
            ILogger<SettingsController> logger, IConverter converter, IRazorViewToStringRendererService razorViewToStringRendererService,
            IShareSyaryoService shareSyaryoService, IAnkensService ankensService)
        {
            _logger = logger;
            _converter = converter;
            _razorViewToStringRendererService = razorViewToStringRendererService;
            _shareSyaryoService = shareSyaryoService;
            _ankensService = ankensService;
        }

        /// <summary>
        /// 詳細情報の取得
        /// </summary>
        /// <param name="id">詳細車種</param>
        /// <returns>詳細情報</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ShareSyaryoDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetDetail(string id)
        {
            try
            {
                if (id is null)
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, string.Format(SystemConstants.Message.RequiredField, "Id"));
                }

                if (!int.TryParse(id, out int intValue))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, string.Format(SystemConstants.Message.InValidNumber, "Id"));
                }

                return await _shareSyaryoService.GetDetailEmptyCarAsync(intValue);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// 空車車両一覧の取得
        /// </summary>
        /// <param name="paramRequests">リクエストパラメータ</param>
        /// <returns>空車車両一覧</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ShareSyaryoDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetList([FromQuery] ShareSyaryoListRequestDto paramRequests)
        {
            try
            {
                var (cid, bid) = User.RequiredCompanyIdBranchId();
                return await _shareSyaryoService.GetListEmptyCarAsync(paramRequests, cid, bid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// 空車車両の更新
        /// </summary>
        /// <param name="id">車両ID</param>
        /// <param name="dtos">更新情報</param>
        /// <returns>更新結果</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([Range(int.MinValue, int.MaxValue, ErrorMessage = SystemConstants.Message.InValidNumber)] string id, UpdateEmptyCarDto dtos)
        {
            try
            {
                string userId = User.FindFirst("UserID")?.Value;

                if (!CommonHelper.TryParseInt(id, out int intValue, "id", out string errorMessage))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, errorMessage);
                }

                ApiResponse result = await _shareSyaryoService.UpdateEmptyCarAsync(intValue, int.Parse(userId), dtos);

                if (result.Message != null)
                {
                    return ApiResponseCommon.CreateApiResponse((HttpStatusCode)result.Code, result.Message);
                }

                return Ok(new { });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }

        }

        /// <summary>
        /// 空車車両の作成
        /// </summary>
        /// <param name="dtos">作成情報</param>
        /// <returns>作成結果</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create(CreateEmptyCarDto dtos)
        {
            try
            {
                string userId = User.FindFirst("UserID")?.Value;
                string companyId = User.FindFirst("CompanyId")?.Value;
                string branchId = User.FindFirst("BranchId")?.Value;
                string groupIdsJson = HttpContext.Session.GetString("GroupIds");
                int minGroupId = 0;
                if (groupIdsJson != null)
                {
                    List<int> groupIds = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(groupIdsJson);
                    minGroupId = groupIds.Min();
                }

                ApiResponse result = await _shareSyaryoService.CreateEmptyCarAsync(int.Parse(userId), dtos, int.Parse(companyId), int.Parse(branchId), minGroupId);

                if (result.Message != null)
                {
                    return ApiResponseCommon.CreateApiResponse((HttpStatusCode)result.Code, result.Message);
                }

                return Ok(new { });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// 空車車両一覧印刷
        /// </summary>
        /// <returns>印刷結果</returns>
        [HttpGet("csv")]
        public async Task<IActionResult> ExportCsv()
        {
            try
            {
                var (cid, bid) = User.RequiredCompanyIdBranchId();

                CompanyPortalsDto data = await _ankensService.GetInfoListKeepEmptyCarAsync(cid, bid);

                if (!data.keepEmptyCars.Any())
                {
                    return Ok(new { });
                }
                string headers = HeaderCsv.EmptyCars;

                List<string> fieldOrder = FieldOrderCsv.KeepEmptyCarFields;

                byte[] csvBytes = CsvExportHelper.ExportToCsv(data.keepEmptyCars, headers, fieldOrder);
                return File(new MemoryStream(csvBytes), "application/octet-stream", "emptycars.csv");
            }
            catch (UnauthorizedAccessException)
            {
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.Unauthorized, Message.Unauthorized);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, Message.InternalServerError);
            }
        }

        /// <summary>
        /// 空車車両ステータスの更新
        /// </summary>
        /// <param name="id">車両ID</param>
        /// <param name="dtos">更新情報</param>
        /// <returns>更新結果</returns>
        [HttpPut("{id}/status-update")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateStatus(string id, UpdateStatusEmptyCarDto dtos)
        {
            if (!CommonHelper.TryParseInt(id, out int intValue, "id", out string errorMessage))
            {
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, errorMessage);
            }
            try
            {
                var (uid, cid, bid) = User.RequiredUserIdCompanyIdBranchId();

                ApiResponse result = await _shareSyaryoService.UpdateStatusEmptyCarAsync(intValue, uid, cid, bid, dtos);

                if (result.Message != null)
                {
                    return ApiResponseCommon.CreateApiResponse((HttpStatusCode)result.Code, result.Message);
                }

                return Ok(new { });
            }
            catch (UnauthorizedAccessException)
            {
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.Unauthorized, Message.Unauthorized);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, Message.InternalServerError);
            }
        }

        /// <summary>
        /// 空車車両一覧（PDF）の取得
        /// </summary>
        /// <param name="paramRequests">リクエストパラメータ</param>
        /// <returns>PDFファイル</returns>
        [HttpGet("print")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> PrintShareEmptyCar([FromQuery] ShareSyaryoListRequestDto paramRequests)
        {
            try
            {
                (int cid, int bid) = User.RequiredCompanyIdBranchId();
                IEnumerable<T_Share_Syaryo> syaryos = await _shareSyaryoService.GetShareEmptyCarForPDFAsync(paramRequests, cid, bid);

                if (syaryos.Count() == 0)
                {
                    return Ok(new { });
                }

                string htmlContent = await _razorViewToStringRendererService.RenderViewToStringAsync("/Templates/PDFShareEmptyCar.cshtml", syaryos);

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
                    return File(stream.ToArray(), "application/pdf", "ShareEmptyCar.pdf");
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
