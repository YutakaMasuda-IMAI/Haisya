using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeikyuWeb.Common;
using SeikyuWeb.Dto;
using SeikyuWeb.Dto.InfoDto;
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
    /// インフォメーションコントローラー
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class InfosController : ControllerBase
    {
        private readonly ILogger<InfosController> _logger;
        private readonly IPortalInfoService _portalInfoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InfosController(ILogger<InfosController> logger,
            IPortalInfoService portalInfoService,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _portalInfoService = portalInfoService;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// お知らせ情報を取得
        /// </summary>
        /// <returns>
        /// 200: 成功
        /// 404: ページが見つかりません。
        /// 401: 許可されていません。
        /// 401: 確認期限が過ぎました
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(PortalInfoDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetNotification()
        {
            try
            {
                ISession session = _httpContextAccessor.HttpContext.Session;
                (string id, bool isCompany) = GetSessionIdentifiers(session);

                if (string.IsNullOrEmpty(id) || !int.TryParse(id, out int idInt) || idInt == 0)
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.NotFound, SystemConstants.Message.DataNotFound);
                }

                IEnumerable<PortalInfoDto> data = await _portalInfoService.GetNotification(idInt, isCompany);

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// 指定したポータル情報の表示フラグを更新
        /// </summary>
        /// <param name="id">ポータル情報ID</param>
        /// <param name="dto">更新データ</param>
        /// <returns>
        /// 200: 成功
        /// 404: ページが見つかりません。
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpPut("{id}/display")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateDisplayFlag([Required] int id, [FromBody] PortalInfoUpdateDto dto)
        {
            try
            {
                // 表示フラグを更新
                ApiResponse result = await _portalInfoService.UpdateDisplayFlagById(id, dto?.displayFlg);

                if (result.message != null)
                {
                    return ApiResponseCommon.CreateApiResponse((HttpStatusCode)result.code, result.message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
            return Ok(new { });
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
