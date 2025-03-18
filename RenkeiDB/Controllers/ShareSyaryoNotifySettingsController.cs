using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RenkeiDB.Common;
using System.Net;
using System.Threading.Tasks;
using System;
using RenkeiDB.Services.Interfaces;
using RenkeiDB.Dto;
using RenkeiDB.Dto.SettingDto;
using RenkeiDB.Dto.SyaryoDto;
using System.ComponentModel.DataAnnotations;

namespace RenkeiDB.Controllers
{
    /// <summary>
    /// 車両通知設定に関する操作を提供するコントローラー
    /// </summary>
    [Route("api/share-syaryo-notify-settings")]
    [ApiController]
    public class ShareSyaryoNotifySettingsController : ControllerBase
    {
        private readonly ILogger<ShareSyaryoNotifySettingsController> _logger;
        private IShareSyaryoNotifySettingService _shareSyaryoNotifySettingService;

        public ShareSyaryoNotifySettingsController(IShareSyaryoNotifySettingService shareSyaryoNotifySettingService, ILogger<ShareSyaryoNotifySettingsController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _shareSyaryoNotifySettingService = shareSyaryoNotifySettingService;
            _logger = logger;
        }

        /// <summary>
        /// 車両通知設定の作成
        /// </summary>
        /// <param name="dtos">車両通知設定データ</param>
        /// <returns>
        /// 200: 成功
        /// 400: パラメータが不正となります。
        /// 404: ページが見つかりません。
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateShareSyaryoNotifySettingDto dtos)
        {
            try
            {
                if (!string.IsNullOrEmpty(dtos.fromDate) && !string.IsNullOrEmpty(dtos.toDate) && DateTime.Parse(dtos.fromDate) > DateTime.Parse(dtos.toDate))
                {
                    string errorMessage = string.Format(SystemConstants.Message.FromDayGreaterThanToDay, "開始日", "終了日");
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, errorMessage);
                }

                if (!string.IsNullOrEmpty(dtos.emptyFromDate) && !string.IsNullOrEmpty(dtos.emptyToDate) && DateTime.Parse(dtos.emptyFromDate) > DateTime.Parse(dtos.emptyToDate))
                {
                    string errorMessage = string.Format(SystemConstants.Message.FromDayGreaterThanToDay, "空車開始日", "空車終了日");
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, errorMessage);
                }

                string loginId = User.FindFirst("LoginId")?.Value;

                ApiResponse result = await _shareSyaryoNotifySettingService.CreateShareSyaryoNotifySetting(loginId, dtos);

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
        /// 車両通知設定の更新
        /// </summary>
        /// <param name="id">車両通知設定ID</param>
        /// <param name="dtos">車両通知設定データ</param>
        /// <returns>
        /// 200: 成功
        /// 400: パラメータが不正となります。
        /// 404: ページが見つかりません。
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([Range(int.MinValue, int.MaxValue, ErrorMessage = SystemConstants.Message.InValidNumber)] string id, UpdateShareSyaryoNotifySettingDto dtos)
        {
            try
            {
                string loginId = User.FindFirst("LoginId")?.Value;

                if (!CommonHelper.TryParseInt(id, out int intValue, "id", out string errorMessage))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, errorMessage);
                }

                ApiResponse result = await _shareSyaryoNotifySettingService.UpdateShareSyaryoNotifySetting(intValue, loginId, dtos);

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
        /// 車両通知設定の詳細取得
        /// </summary>
        /// <param name="id">車両通知設定ID</param>
        /// <returns>
        /// 200: 成功
        /// 400: パラメータが不正となります。
        /// 404: 該当アドレスのページがない、またはそのサーバーが落ちている状態です。
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(SyaryoNotifySettignDto), (int)HttpStatusCode.OK)]
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

                SyaryoNotifySettignDto result = await _shareSyaryoNotifySettingService.GetDetailShareSyaryoNotifySetting(intValue);

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
        /// 車両通知設定の削除
        /// </summary>
        /// <param name="id">車両通知設定ID</param>
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

                ApiResponse result = await _shareSyaryoNotifySettingService.DeleteShareSyaryoNotifySetting(intValue);

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
