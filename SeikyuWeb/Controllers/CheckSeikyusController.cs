using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeikyuWeb.Common;
using SeikyuWeb.Dto;
using SeikyuWeb.Dto.CheckShiharaisDto;
using SeikyuWeb.Dto.ReportDto;
using SeikyuWeb.Dto.Seikyu;
using SeikyuWeb.Models;
using SeikyuWeb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SeikyuWeb.Controllers
{
    /// <summary>
    /// CheckSeikyusController クラス
    /// </summary>
    [Route("api/check-seikyus")]
    [ApiController]
    public class CheckSeikyusController : ControllerBase
    {
        private readonly ILogger<CheckSeikyusController> _logger;
        private readonly ICheckSeikyuService _checkSeikyuService;
        private readonly ISeikyuService _seikyuService;
        private readonly IPortalInfoService _portalInfoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CheckSeikyusController(ILogger<CheckSeikyusController> logger,
            ICheckSeikyuService checkSeikyuService,
            ISeikyuService seikyuService,
            IPortalInfoService portalInfoService,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _checkSeikyuService = checkSeikyuService;
            _seikyuService = seikyuService;
            _portalInfoService = portalInfoService;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 請求問合せ入力 確認中または確認済み登録ボタン
        /// </summary>
        /// <param name="check_seikyu_id">チェック請求ID</param>
        /// <param name="dto">更新リクエストDTO</param>
        /// <returns>
        /// 200: 成功
        /// 404: ページが見つかりません。
        /// 401: 許可されていません。
        /// 401: 確認期限が過ぎました
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpPut("{check_seikyu_id}")]
        [Authorize]
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateCheckSeikyus([Required] int check_seikyu_id, [FromBody] RequestUpdateCheckSeikyusDto dto)
        {
            try
            {
                ISession session = _httpContextAccessor.HttpContext.Session;
                string tantouId = session.GetString("tantouId");

                // 担当IDのNULLチェックおよび値チェック
                if (string.IsNullOrEmpty(tantouId) || !int.TryParse(tantouId, out int idInt))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.Unauthorized, SystemConstants.Message.Unauthorized);
                }

                // キーの重複チェック
                if (CheckDuplicateKeySeikyuChanges(dto))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, string.Format(SystemConstants.Message.AllowValue, "請求変更"));
                }

                // 請求問合せ入力
                ApiResponse result = await _checkSeikyuService.UpdateCheckSeikyu(check_seikyu_id, idInt, dto);

                if (result.message != null)
                {
                    return ApiResponseCommon.CreateApiResponse((HttpStatusCode)result.code, result.message);
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
        /// 案件一覧 CSV出力
        /// </summary>
        /// <param name="check_seikyu_id">チェック請求ID</param>
        /// <returns>
        /// 200: 成功
        /// 401: 権限がありません。
        /// 404: データがみつかりません。
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpGet("csv")]
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ExportCsv([FromQuery] CheckSeikyuCsvReqDto dto)
        {
            try
            {
                // 出力するデータを取得します
                ReportCommonDto data = await _checkSeikyuService.GetDataExport(dto.kubun, dto.checkId);

                if (data == null || data.ReportSearch == null || data.ReportCommonList.Count == 0 || data.ReportSearch.Report_Serch_Kubun_List.Count == 0)
                {
                    return Ok(new { });
                }

                MReportOutputItem[] searchItems = data.ReportSearch.Report_Serch_Kubun_List.SelectMany(item => item.Report_Output_Item_List.OrderBy(r => r.SortOrder)).ToArray();

                var dataheader = searchItems.Select(item => !string.IsNullOrEmpty(item.CsvTitle) ? item.CsvTitle : item.ReportOutputItemMaster.DisplayTitle);

                string headers = string.Join(",", dataheader);
                List<MReportOutputItemMaster> fieldOrder = searchItems.Select(item => item.ReportOutputItemMaster).ToList();
                string fileName = $"{DateTime.Now.ToString(SystemConstants.DateFormat.DATE_NO_SLASH)}_{data.ReportSearch.CsvFileName}.csv";

                byte[] memoryStream = CsvExportHelper.ExportToCsv(data.ReportCommonList, headers, fieldOrder, fileName);

                return File(memoryStream, "application/octet-stream", fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }
        #region Billing_Inquiry_Detail
        /// <summary>
        /// 請求問合せ入力＆取得
        /// </summary>
        /// <param name="check_seikyu_id">チェック請求ID</param>
        /// <returns>
        /// 200: 成功
        /// 400: ページが見つかりません。
        /// 401: 許可されていません。
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpGet("{check_seikyu_id}")]
        [Authorize]
        [ProducesResponseType(typeof(BillingInquiryDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetBillingInquiriesById([Required] int check_seikyu_id)
        {
            try
            {
                ISession session = _httpContextAccessor.HttpContext.Session;
                (string id, bool isCompany) = GetSessionIdentifiers(session);

                if (check_seikyu_id == 0)
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.NotFound, SystemConstants.Message.DataNotFound);
                }

                BillingInquiryDto data = await _checkSeikyuService.GetBillingInquiriesById(check_seikyu_id, int.Parse(id), isCompany);

                if (data == null)
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.NotFound, SystemConstants.Message.DataNotFound);
                }

                // 表示フラグの更新
                await _portalInfoService.UpdateDisplayFlag(data.portalInfoIds);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }
        #endregion Billing_Inquiry_Detail

        #region Billing_Inquiry_List
        /// <summary>
        /// 請求問合せ一覧
        /// </summary>
        /// <param name="dto">請求問合せリクエストDTO</param>
        /// <returns>
        /// 200: 成功
        /// 400: ページが見つかりません。
        /// 401: 許可されていません。
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpGet()]
        [Authorize]
        [ProducesResponseType(typeof(List<CheckSeikyuDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetBillings([FromQuery] CheckSeikyuReqDto dto)
        {
            try
            {
                ISession session = _httpContextAccessor.HttpContext.Session;
                (string id, bool isCompany) = GetSessionIdentifiers(session);

                IEnumerable<CheckSeikyuDto> data = await _seikyuService.GetBillingInqueryList(dto.kubun, int.Parse(id), isCompany);

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }
        #endregion Billing_Inquiry_List

        #region Private Methods
        /// <summary>
        /// キーの重複チェック
        /// </summary>
        /// <param name="dto">更新リクエストDTO</param>
        /// <returns>重複がある場合はtrue、ない場合はfalse</returns>
        private static bool CheckDuplicateKeySeikyuChanges(RequestUpdateCheckSeikyusDto dto)
        {
            if (dto.seikyuChanges.Any())
            {
                List<SeikyuChangeDto> seikyuChanges = dto.seikyuChanges;
                HashSet<(int? checkSeikyuId, int? uriageUnchinId)> uniqueKeys = new HashSet<(int? checkSeikyuId, int? uriageUnchinId)>();

                foreach (var change in seikyuChanges)
                {
                    (int? checkSeikyuId, int? uriageUnchinId) key = (change.checkSeikyuId, change.uriageUnchinId);
                    if (!uniqueKeys.Add(key))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// セッション識別子の取得
        /// </summary>
        /// <param name="session">セッション</param>
        /// <returns>識別子と会社フラグのタプル</returns>
        private static (string id, bool isCompany) GetSessionIdentifiers(ISession session)
        {
            string companyId = session.GetString("companyId");
            string tantouId = session.GetString("tantouId");

            return
                !string.IsNullOrEmpty(companyId) ? (companyId, true) :
                !string.IsNullOrEmpty(tantouId) ? (tantouId, false) : (null, false);
        }
        #endregion Private Methods
    }
}
