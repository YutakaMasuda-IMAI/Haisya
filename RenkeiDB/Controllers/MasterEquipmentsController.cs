using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RenkeiDB.Common;
using RenkeiDB.Dto.EquipmentDto;
using RenkeiDB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RenkeiDB.Controllers
{
    /// <summary>
    /// マスター設備に関する操作を提供するコントローラー
    /// </summary>
    [Route("api")]
    [ApiController]
    public class MasterEquipmentsController : ControllerBase
    {
        private readonly ILogger<MasterEquipmentsController> _logger;
        private IMasterEquipmentsService _masterEquipmentsService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MasterEquipmentsController(ILogger<MasterEquipmentsController> logger, IMasterEquipmentsService masterEquipmentsService, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _masterEquipmentsService = masterEquipmentsService;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// EquipmentグループIDによるマスターEquipmentリストを取得する
        /// </summary>
        /// <param name="mEquipmentGroupId">設備グループID</param>
        /// <returns>
        /// 200: 成功
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpGet("master/equipments/{mEquipmentGroupId}")]
        [Authorize]
        public async Task<IActionResult> GetMasterLuggageByGroupIds(string mEquipmentGroupId)
        {
            try
            {
                // mEquipmentGroupIdがnullまたは空の場合、RequiredFieldメッセージを返す
                if (string.IsNullOrWhiteSpace(mEquipmentGroupId))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, string.Format(SystemConstants.Message.RequiredField, "mEquipmentGroupId"));
                }

                // mEquipmentGroupIdが整数値でない場合、InValidNumberメッセージを返す
                if (!int.TryParse(mEquipmentGroupId, out int intEquipmentGroupId))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, string.Format(SystemConstants.Message.InValidNumber, "mEquipmentGroupId"));
                }

                // mEquipmentGroupIdによるマスターEquipmentリストを取得する
                IEnumerable<MasterEquipmentDto> data = await _masterEquipmentsService.GetMasterEquipmentsByEqGroupId(intEquipmentGroupId);
                return Ok(data);
            }
            // 例外処理
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }
    }
}
