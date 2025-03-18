using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RenkeiDB.Common;
using RenkeiDB.Dto;
using RenkeiDB.Dto.SettingDto;
using RenkeiDB.Services.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;

namespace RenkeiDB.Controllers
{
    /// <summary>
    /// 設定に関する操作を提供するコントローラー
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SettingsController : ControllerBase
    {
        private readonly ILogger<SettingsController> _logger;
        private ISettingService _settingService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SettingsController(ISettingService settingService, ILogger<SettingsController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _settingService = settingService;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 設定一覧
        /// </summary>
        /// <returns>設定一覧</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(SettingEntryDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllSetting()
        {
            try
            {
                SettingEntryDto setting = await GetSettingAsync();
                if (setting is null)
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.NotFound, SystemConstants.Message.DataNotFound);
                }
                return Ok(setting);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        private async Task<SettingEntryDto> GetSettingAsync()
        {
            ISession session = _httpContextAccessor.HttpContext.Session;
            string groupIdsSession = session.GetString("GroupIds");
            string companyUserIdSession = session.GetString("companyUserId");
            if (string.IsNullOrEmpty(groupIdsSession) && string.IsNullOrEmpty(companyUserIdSession))
            {
                return null;
            }
            int[] groupIds = System.Text.Json.JsonSerializer.Deserialize<int[]>(groupIdsSession);
            int companyUserId = int.Parse(companyUserIdSession);
            SettingEntryDto setting = await _settingService.GetSettingAsync(groupIds, companyUserId);
            return setting;
        }
    }
}
