using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RenkeiDB.Common;
using RenkeiDB.Dto;
using RenkeiDB.Dto.LuggageDto;
using RenkeiDB.Services;
using RenkeiDB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RenkeiDB.Controllers
{
    /// <summary>
    /// 荷物に関する操作を提供するコントローラー
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LuggagesController : Controller
    {
        private readonly ILogger<LuggagesController> _logger;
        private readonly IConverter _converter;
        private readonly ILuggageService _luggageService;
        private readonly IViewRenderService _viewRenderService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="luggageService">荷物サービス</param>
        /// <param name="converter">PDFコンバーター</param>
        /// <param name="viewRenderService">ビューレンダーサービス</param>
        /// <param name="logger">ロガー</param>
        public LuggagesController(ILuggageService luggageService, IConverter converter, IViewRenderService viewRenderService, ILogger<LuggagesController> logger)
        {
            _viewRenderService = viewRenderService;
            _luggageService = luggageService;
            _converter = converter;
            _logger = logger;
        }

        /// <summary>
        /// 荷物共有一覧印刷
        /// </summary>
        /// <param name="q">クエリパラメータ</param>
        /// <returns>PDFファイル</returns>
        [HttpGet("print")]
        public async Task<IActionResult> Print([FromQuery] LuggageQuery q)
        {
            try
            {
                string msg;

                #region Parse & validate string-date-type

                // 積み日時（From）をパース・検証
                DateTime? d1 = null;
                if (!string.IsNullOrWhiteSpace(q.tumiFromDate) && !q.tumiFromDate.TryParseDate(out d1))
                {
                    msg = string.Format(SystemConstants.Message.InValidDate, nameof(q.tumiFromDate));
                    throw new BadHttpRequestException(msg);
                }
                // 積み日時（To）をパース・検証
                DateTime? d2 = null;
                if (!string.IsNullOrWhiteSpace(q.tumiToDate) && !q.tumiToDate.TryParseDate(out d2))
                {
                    msg = string.Format(SystemConstants.Message.InValidDate, nameof(q.tumiToDate));
                    throw new BadHttpRequestException(msg);
                }
                // 積み日時（From）が積み日時（To）より大きい場合
                if (d1 != null && d2 != null && d1 > d2)
                {
                    msg = string.Format(SystemConstants.Message.FromDayGreaterThanToDay, nameof(q.tumiFromDate), nameof(q.tumiToDate));
                    throw new BadHttpRequestException(msg); ;
                }
                #endregion

                // syasyuをパース・検証
                int syasyu = 0;
                if (!string.IsNullOrWhiteSpace(q.syasyu) && !int.TryParse(q.syasyu, out syasyu))
                {
                    msg = string.Format(SystemConstants.Message.InValidNumber, nameof(q.syasyu));
                    throw new BadHttpRequestException(msg);
                }

                // queryビルド
                LuggageParamsDto query = new()
                {
                    tumi = q.tumi,
                    syasyu = syasyu,
                    tumiToDate = d2,
                    oroshi = q.oroshi,
                    tumiFromDate = d1,
                };
                var (cid, bid) = User.RequiredCompanyIdBranchId();
                // データ取得
                IEnumerable<LuggagePrintDataDto> r = await _luggageService.Get_luggages(query, cid, bid);
                if (r == null || !r.Any())
                {
                    return Ok(new { });
                }

                string html = await _viewRenderService.Render_to_string(this, "_Print", r, true);

                byte[] pdf = _converter.ExportToPdf(html);

                using MemoryStream ms = new();
                ms.Write(pdf, 0, pdf.Length);
                return File(ms.ToArray(), "application/pdf", $"luggage-print-data-{DateTime.Now:yyyyMMdd}.pdf");
            }
            // データが存在しません。
            catch (BadHttpRequestException x)
            {
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, x.Message);
            }
            catch (Exception x)
            {
                _logger.LogError(x, SystemConstants.Message.Error);
                // 500エラー
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// 荷物共有一覧の取得
        /// </summary>
        /// <param name="LuggageParamsDto">クエリパラメータ</param>
        /// <returns>荷物共有一覧</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ShareLuggageDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllLuggages([FromQuery] LuggageQuery dtos)
        {
            try
            {
                if (!CommonHelper.TryParseDate(dtos.tumiFromDate, out DateTime? tempTumiFromDate, "積み開始日", out string errorMessage))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, errorMessage);
                }

                if (!CommonHelper.TryParseDate(dtos.tumiToDate, out DateTime? tempTumiToDate, "積み終了日", out errorMessage))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, errorMessage);
                }

                if (!CommonHelper.TryParseInt(dtos.syasyu, out int tempIntValue, "車種", out errorMessage))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, errorMessage);
                }

                LuggageParamsDto query = new()
                {
                    tumiFromDate = tempTumiFromDate,
                    tumiToDate = tempTumiToDate,
                    tumi = dtos.tumi,
                    oroshi = dtos.oroshi,
                    syasyu = tempIntValue,
                };

                var (cid, bid) = User.RequiredCompanyIdBranchId();
                IEnumerable<ShareLuggageDto> result = await _luggageService.GetLuggageAsync(query, cid, bid);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }

        }

        /// <summary>
        /// 荷物共有の詳細取得
        /// </summary>
        /// <param name="id">荷物ID</param>
        /// <returns>荷物共有の詳細</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ShareLuggageDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetLuggage(string id)
        {
            try
            {
                if (!CommonHelper.TryParseInt(id, out int intValue, "id", out string errorMessage))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, errorMessage);
                }

                ShareLuggageDto result = await _luggageService.GetLuggageDetailAsync(intValue);
                if (result is null)
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.NotFound, SystemConstants.Message.DataNotFound);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }

        }

        /// <summary>
        /// 荷物共有　新規作成
        /// </summary>
        /// <param name="dtos">荷物共有作成DTO</param>
        /// <returns>作成結果</returns>
        [HttpPost()]
        public async Task<IActionResult> Create(CreateShareLuggageDto dtos)
        {
            try
            {
                // Retrieve the cookie value
                var (uid, cid, bid) = User.RequiredUserIdCompanyIdBranchId();
                string groupIdsJson = HttpContext.Session.GetString("GroupIds");
                int minGroupId = 0;
                if (groupIdsJson != null)
                {
                    List<int> groupIds = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(groupIdsJson);
                    minGroupId = groupIds.Min();
                }

                ApiResponse result = await _luggageService.CreateShareLuggageAsync(uid, cid, bid, minGroupId, dtos);

                if (result.Message != null)
                {
                    return ApiResponseCommon.CreateApiResponse((HttpStatusCode)result.Code, result.Message);
                }

                return Ok(new { });
            }
            catch (UnauthorizedAccessException)
            {
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.Unauthorized, SystemConstants.Message.Unauthorized);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// 荷物共有情報　更新
        /// </summary>
        /// <param name="id">Share_Luggage_ID</param>
        /// <param name="dtos">荷物共有更新DTO</param>
        /// <returns>更新結果</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([Range(int.MinValue, int.MaxValue, ErrorMessage = SystemConstants.Message.InValidNumber)] string id, UpdateShareLuggageDto dtos)
        {
            try
            {
                if (!CommonHelper.TryParseInt(id, out int id_val, "id", out string errorMessage))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, errorMessage);
                }
                // Retrieve the cookie value
                int uid = User.RequiredUserID();

                ApiResponse result = await _luggageService.UpdateShareLuggageAsync(id_val, uid, dtos);
                if (result.Message != null)
                {
                    return ApiResponseCommon.CreateApiResponse((HttpStatusCode)result.Code, result.Message);
                }

                return Ok(new { });
            }
            catch (UnauthorizedAccessException)
            {
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.Unauthorized, SystemConstants.Message.Unauthorized);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// 荷物共有　ステータス更新
        /// </summary>
        /// <param name="id">Share_Luggage_ID</param>
        /// <param name="dtos">ステータス更新DTO</param>
        /// <returns>更新結果</returns>
        [HttpPut("{id}/update-status")]
        public async Task<IActionResult> UpdateStatus([Range(int.MinValue, int.MaxValue, ErrorMessage = SystemConstants.Message.InValidNumber)] string id, UpdateShareLuggageStatusDto dtos)
        {
            try
            {
                string userId = User.FindFirst("UserID")?.Value;
                string companyId = User.FindFirst("CompanyID")?.Value;
                string branchId = User.FindFirst("BranchID")?.Value;

                if (!CommonHelper.TryParseInt(id, out int intValue, "id", out string errorMessage))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, errorMessage);
                }

                ApiResponse result = await _luggageService.UpdateShareLuggageStatusAsync(intValue, int.Parse(userId), int.Parse(companyId), int.Parse(branchId), dtos);

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
    }
}
