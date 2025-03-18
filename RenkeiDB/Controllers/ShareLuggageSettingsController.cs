using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RenkeiDB.Common;
using RenkeiDB.Dto.SettingDto;
using RenkeiDB.Services;
using RenkeiDB.Services.Interfaces;
using System.Net;
using System.Threading.Tasks;
using System;
using RenkeiDB.Dto;
using RenkeiDB.Dto.LuggageDto;
using System.ComponentModel.DataAnnotations;

namespace RenkeiDB.Controllers
{
    /// <summary>
    /// 荷物通知設定に関する操作を提供するコントローラー
    /// </summary>
    [Route("api/share-luggage-settings")]
    [ApiController]
    public class ShareLuggageSettingsController : ControllerBase
    {
        private readonly ILogger<ShareLuggageSettingsController> _logger;
        private IShareLuggageNotifySettingService _shareLuggageSettingService;

        public ShareLuggageSettingsController(IShareLuggageNotifySettingService shareLuggageSettingService, ILogger<ShareLuggageSettingsController> logger)
        {
            _shareLuggageSettingService = shareLuggageSettingService;
            _logger = logger;
        }

        /// <summary>
        /// idで荷物通知設定詳細取得
        /// </summary>
        /// <param name="id">荷物通知設定ID</param>
        /// <returns>
        /// 200: 成功
        /// 400: パラメータが不正となります。
        /// 404: データがみつかりません。
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(LuggageNotifySettingDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetDetail(string id)
        {
            try
            {
                if (!CommonHelper.TryParseInt(id, out int intValue, "id", out string errorMessage))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, errorMessage);
                }

                LuggageNotifySettingDto result = await _shareLuggageSettingService.GetDetailShareLuggageNotifySetting(intValue);

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
        /// 荷物通知設定の更新
        /// </summary>
        /// <param name="id">荷物通知設定ID</param>
        /// <param name="dtos">荷物通知設定データ</param>
        /// <returns>
        /// 200: 成功
        /// 400: パラメータが不正となります。
        /// 404: ページが見つかりません。
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update([Range(1, int.MaxValue, ErrorMessage = SystemConstants.Message.InValidNumber)] string id, [FromBody] CreateShareLuggageNotifySettingDto dtos)
        {
            try
            {
                string loginId = User.FindFirst("LoginId")?.Value;

                if (!CommonHelper.TryParseInt(id, out int intValue, "id", out string errorMessage))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, errorMessage);
                }

                ApiResponse result = await _shareLuggageSettingService.UpdateShareLuggageNotifySetting(intValue, loginId, dtos);

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
        /// 荷物通知設定の削除
        /// </summary>
        /// <param name="id">荷物通知設定ID</param>
        /// <returns>
        /// 200: 成功
        /// 400: パラメータが不正となります。
        /// 404: データがみつかりません。
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(LuggageNotifySettingDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (!CommonHelper.TryParseInt(id, out int intValue, "id", out string errorMessage))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, errorMessage);
                }

                ApiResponse result = await _shareLuggageSettingService.DeleteShareLuggageNotifySetting(intValue);

                if (result.Message != null)
                {
                    return ApiResponseCommon.CreateApiResponse((HttpStatusCode)result.Code, result.Message);
                }

                return Ok(new {});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }

        }
    }
}
