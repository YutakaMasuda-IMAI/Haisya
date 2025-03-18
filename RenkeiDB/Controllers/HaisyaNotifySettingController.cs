using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RenkeiDB.Common;
using RenkeiDB.Dto.LuggageDto;
using RenkeiDB.Services;
using RenkeiDB.Services.Interfaces;
using System.Net;
using System.Threading.Tasks;
using System;
using RenkeiDB.Dto;
using RenkeiDB.Dto.ValidateRules;
using static RenkeiDB.Common.SystemEnums;
using System.ComponentModel.DataAnnotations;
using RenkeiDB.Dto.HaisyaNotifySettingDto;

namespace RenkeiDB.Controllers
{
    /// <summary>
    /// 配車通知設定に関する操作を提供するコントローラー
    /// </summary>
    [Route("api/haisya-notify-settings")]
    [ApiController]
    public class HaisyaNotifySettingController : ControllerBase
    {
        private readonly ILogger<HaisyaNotifySettingController> _logger;
        private IHaisyaNotifySettingService _haisyaNotifySettingService;

        public HaisyaNotifySettingController(IHaisyaNotifySettingService haisyaNotifySettingService, ILogger<HaisyaNotifySettingController> logger)
        {
            _haisyaNotifySettingService = haisyaNotifySettingService;
            _logger = logger;
        }

        /// <summary>
        /// 配車通知設定を更新する
        /// </summary>
        /// <param name="dto">配車通知設定の更新情報</param>
        /// <returns>更新結果</returns>
        [HttpPut()]
        [Authorize]
        [ProducesResponseType(typeof(ShareLuggageDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(UpdateHaisyaNotifySettingDto dto)
        {
            try
            {
                string userId = User.FindFirst("UserID")?.Value;

                ApiResponse result = await _haisyaNotifySettingService.UpdateDataAsync(int.Parse(userId), (int)dto.notifyType);

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
