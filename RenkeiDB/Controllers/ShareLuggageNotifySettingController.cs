using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RenkeiDB.Common;
using RenkeiDB.Dto;
using RenkeiDB.Services.Interfaces;
using System.Net;
using System.Threading.Tasks;
using System;
using RenkeiDB.Dto.LuggageDto;
using RenkeiDB.Dto.SettingDto;

namespace RenkeiDB.Controllers
{
    /// <summary>
    /// 荷物通知設定に関する操作を提供するコントローラー
    /// </summary>
    [Route("api/share-luggage-notify-settings")]
    [ApiController]
    public class ShareLuggageNotifySettingController : ControllerBase
    {
        private readonly ILogger<ShareLuggageNotifySettingController> _logger;
        private IShareLuggageNotifySettingService _shareLuggageSettingService;

        public ShareLuggageNotifySettingController(IShareLuggageNotifySettingService shareLuggageSettingService, ILogger<ShareLuggageNotifySettingController> logger)
        {
            _shareLuggageSettingService = shareLuggageSettingService;
            _logger = logger;
        }

        /// <summary>
        /// 荷物通知設定の作成
        /// </summary>
        /// <param name="dtos">荷物通知設定データ</param>
        /// <returns>
        /// 200: 成功
        /// 400: パラメータが不正となります。
        /// 404: ページが見つかりません。
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create(CreateShareLuggageNotifySettingDto dtos)
        {
            try
            {
                string loginId = User.FindFirst("LoginId")?.Value;

                ApiResponse result = await _shareLuggageSettingService.CreateShareLuggageNotifySetting(loginId, dtos);

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
