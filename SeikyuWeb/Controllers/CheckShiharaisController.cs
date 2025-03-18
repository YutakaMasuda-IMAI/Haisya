using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeikyuWeb.Common;
using SeikyuWeb.Dto;
using SeikyuWeb.Dto.CheckShiharaisDto;
using SeikyuWeb.Dto.Shitabarai;
using SeikyuWeb.Dto.ValidateRules;
using SeikyuWeb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace SeikyuWeb.Controllers
{
    /// <summary>
    /// 支払確認コントローラー
    /// </summary>
    [Route("api/check-shiharais")]
    [ApiController]
    [Authorize]
    public class CheckShiharaisController : ControllerBase
    {
        private readonly ILogger<CheckShiharaisController> _logger;
        private readonly ICheckShiharaiService _checkShiharaiService;
        private readonly IPortalInfoService _portalInfoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CheckShiharaisController(ILogger<CheckShiharaisController> logger,
            ICheckShiharaiService checkShiharaiService, IPortalInfoService portalInfoService,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _checkShiharaiService = checkShiharaiService;
            _portalInfoService = portalInfoService;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 支払問合せ入力
        /// </summary>
        /// <param name="check_shitabarai_id">支払確認ID</param>
        /// <param name="dto">更新データ</param>
        /// <returns>
        /// 200: 成功
        /// 404: ページが見つかりません。
        /// 401: 許可されていません。
        /// 401: 確認期限が過ぎました
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpPut("{check_shitabarai_id}")]
        [Authorize]
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateCheckShiarais(int check_shitabarai_id, RequestUpdateCheckShiharaisDto dto)
        {
            try
            {
                ISession session = _httpContextAccessor.HttpContext.Session;
                string tantouId = session.GetString("tantouId");

                if (string.IsNullOrEmpty(tantouId))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.Unauthorized, SystemConstants.Message.Unauthorized);
                }

                ApiResponse result = await _checkShiharaiService.UpdateCheckShiharai(check_shitabarai_id, int.Parse(tantouId), dto);
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
        /// 支払問合せ一覧を取得
        /// </summary>
        /// <param name="kubun">抽出区分（1：確認済、2：確認中/未確認）</param>
        /// <returns>
        /// 200: 成功
        /// 401: 許可されていません。
        /// 401: 確認期限が過ぎました
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpGet()]
        [Authorize]
        [ProducesResponseType(typeof(CheckShiharaisDto[]), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetListCheckShiharais(
            [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
            [AllowedValues("1", "2")]
            [Display(Name = "区分")]
            string kubun
        )
        {
            try
            {
                ISession session = _httpContextAccessor.HttpContext.Session;
                string tantouId = session.GetString("tantouId");
                string companyId = session.GetString("companyId");

                if (string.IsNullOrEmpty(tantouId) && string.IsNullOrEmpty(companyId))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.Unauthorized, SystemConstants.Message.Unauthorized);
                }

                IEnumerable<CheckShiharaisDto> data = await _checkShiharaiService.GetListCheckShiharaisAsync(int.Parse(tantouId ?? "0"), int.Parse(companyId ?? "0"), int.Parse(kubun));

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// 支払問合せ入力＆取得
        /// </summary>
        /// <param name="check_shitabarai_id">支払確認ID</param>
        /// <returns>
        /// 200: 成功
        /// 400: ページが見つかりません。
        /// 401: 許可されていません。
        /// 401: 確認期限が過ぎました
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpGet("{check_shitabarai_id}")]
        [ProducesResponseType(typeof(PaymentInquiryDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetPaymentInquiriesById([Required] int check_shitabarai_id)
        {
            try
            {
                if (check_shitabarai_id == 0)
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.NotFound, SystemConstants.Message.DataNotFound);
                }

                ISession session = _httpContextAccessor.HttpContext.Session;
                (string id, bool isCompany) = GetSessionIdentifiers(session);

                if (string.IsNullOrEmpty(id) || !int.TryParse(id, out int idInt) || idInt == 0)
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.NotFound, SystemConstants.Message.DataNotFound);
                }

                PaymentInquiryDto data = await _checkShiharaiService.GetPaymentInquiriesById(check_shitabarai_id, idInt, isCompany);

                if (data == null)
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.NotFound, SystemConstants.Message.DataNotFound);
                }

                await _portalInfoService.UpdateDisplayFlag(data.portalInfoIds);

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// セッション識別子を取得
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
    }
}
