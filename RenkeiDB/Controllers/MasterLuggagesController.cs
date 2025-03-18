using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RenkeiDB.Common;
using RenkeiDB.Dto.MasterLuggageDto;
using RenkeiDB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RenkeiDB.Controllers
{
    /// <summary>
    /// マスター荷物に関する操作を提供するコントローラー
    /// </summary>
    [Route("api")]
    [ApiController]
    public class MasterLuggagesController : ControllerBase
    {
        private readonly ILogger<MasterLuggagesController> _logger;
        private IMasterLuggagesService _masterLuggagesService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MasterLuggagesController(ILogger<MasterLuggagesController> logger, IMasterLuggagesService masterLuggagesService, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _masterLuggagesService = masterLuggagesService;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 荷物グループのリストを取得する
        /// </summary>
        /// <returns>
        /// 200: 成功
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpGet("master/luggage-groups")]
        [Authorize]
        public async Task<IActionResult> GetMasterLuggageGroups()
        {
            try
            {
                // CompanyIdのCookie値を取得
                string companyId = User.FindFirst("CompanyId")?.Value;
                // CompanyIdがnullの場合、Unauthorizedを返す
                if (companyId == null)
                {
                    return Unauthorized();
                }
                // 荷物グループを取得
                IEnumerable<MasterLuggageGroupsDto> data = await _masterLuggagesService.GetMasterLuggageGroups(int.Parse(companyId));
                return Ok(data);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// 荷物グループIDによるマスター荷物のリストを取得する
        /// </summary>
        /// <param name="mLuggageGroupId">荷物グループID</param>
        /// <returns>
        /// 200: List Master Luggage By LuggageGroupId
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpGet("master/luggages/{mLuggageGroupId}")]
        [Authorize]
        public async Task<IActionResult> GetMasterLuggageByGroupIds(string mLuggageGroupId) {
            try
            {
                // mLuggageGroupIdがnullまたは空の場合、RequiredFieldメッセージとともにBadRequestを返す
                if (string.IsNullOrWhiteSpace(mLuggageGroupId))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, string.Format(SystemConstants.Message.RequiredField, "mLuggageGroupId"));
                }

                // mLuggageGroupIdが整数値でない場合、InValidNumberメッセージとともにBadRequestを返す
                if (!int.TryParse(mLuggageGroupId, out int intLuggageGroupId))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, string.Format(SystemConstants.Message.InValidNumber, "mLuggageGroupId"));
                }
                // 荷物グループIDからマスター荷物のリストを取得
                IEnumerable<MasterLuggageDto> data = await _masterLuggagesService.GetMasterLuggagesByLugGroupId(intLuggageGroupId);
                return Ok(data);
            }
            // 例外が発生した場合
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }
    }
}
