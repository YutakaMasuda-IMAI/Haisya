using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeikyuWeb.Common;
using SeikyuWeb.Dto;
using SeikyuWeb.Dto.PortalDto;
using SeikyuWeb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SeikyuWeb.Controllers
{
    /// <summary>
    /// ポータルコントローラー
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PortalsController : ControllerBase
    {
        private readonly ILogger<PortalsController> _logger;
        private readonly IPortalInfoService _portalInfoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PortalsController(ILogger<PortalsController> logger, 
            IPortalInfoService portalInfoService, 
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _portalInfoService = portalInfoService;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// ポータル情報を取得
        /// </summary>
        /// <returns>
        /// 200: 成功
        /// 404: ページが見つかりません。
        /// 401: 許可されていません。
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(PortalDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetPortals()
        {
            try
            {
                ISession session = _httpContextAccessor.HttpContext.Session;
                (string id, bool isCompany) = GetSessionIdentifiers(session);

                if (string.IsNullOrEmpty(id) || !int.TryParse(id, out int idInt) || idInt == 0)
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.NotFound, SystemConstants.Message.DataNotFound);
                }

                IEnumerable<PortalDto> data = await _portalInfoService.GetPortals(idInt, isCompany);
                
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
            string customerBranchId = session.GetString("customerBranchId");

            if (!string.IsNullOrEmpty(companyId))
            {
                return (companyId, true);
            }

            if (!string.IsNullOrEmpty(customerBranchId))
            {
                return (customerBranchId, false);
            }

            return (null, false);
        }
    }
}
