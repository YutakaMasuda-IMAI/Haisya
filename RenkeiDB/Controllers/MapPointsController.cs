using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RenkeiDB.Common;
using RenkeiDB.Data;
using RenkeiDB.Dto;
using RenkeiDB.Dto.MapPointDto;
using RenkeiDB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace RenkeiDB.Controllers
{
    /// <summary>
    /// 地図ポイントに関する操作を提供するコントローラー
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api")]
    public class MapPointsController : ControllerBase
    {
        private readonly ILogger<MapPointsController> _logger;
        private readonly IMapPointService _mapPointService;

        public MapPointsController(IMapPointService mapPointService, ILogger<MapPointsController> logger)
        {
            _mapPointService = mapPointService;
            _logger = logger;
        }

        /// <summary>
        /// 地図ポイント一覧取得
        /// </summary>
        /// <param name="address2">住所</param>
        /// <param name="userId">ログインしているユーザーのID</param>
        /// <param name="groupId">グループID</param>
        /// <returns>地図ポイント一覧</returns>
        [HttpGet("points")]
        [ProducesResponseType(typeof(IEnumerable<MapPointDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetMapPoints(string address2, int userId, int? groupId)
        {
            try
            {
                if (userId == 0 && groupId == null)
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.NotFound, SystemConstants.Message.DataNotFound);
                }

                IEnumerable<MapPointDto> data = await _mapPointService.GetMapPoints(userId, groupId, address2);

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// 地図ポイント保存
        /// </summary>
        /// <returns>保存結果</returns>
        [HttpPost("points")]
        public async Task<IActionResult> CreateMapPoints(UpdateMapPointDto dto)
        {
            try
            {
                ApiResponse data = await _mapPointService.CreateMapPoint(dto);

                return Ok(JsonConvert.SerializeObject(data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// 地図ポイント更新
        /// </summary>
        /// <param name="id">地図ポイントID</param>
        /// <param name="dto">更新データ</param>
        /// <returns>更新結果</returns>
        [HttpPut("points/{id}")]
        public async Task<IActionResult> UpdateMapPoints([Range(int.MinValue, int.MaxValue, ErrorMessage = SystemConstants.Message.InValidNumber)] string id, UpdateMapPointDto dto)
        {
            try
            {
                if (!int.TryParse(id, out int intValue))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, SystemConstants.Message.InValidNumber);
                }

                ApiResponse data = await _mapPointService.UpdateMapPoint(intValue, dto);

                return Ok(JsonConvert.SerializeObject(data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// 地図ポイント登録チェック
        /// </summary>
        /// <param name="dto">チェックデータ</param>
        /// <returns>チェック結果</returns>
        [HttpGet("check-point/{userId}/{groupId}/{addressCode}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CheckMapPoints([FromRoute] CheckPointDto dto)
        {
            try
            {
                if (!int.TryParse(dto.userId, out int userIdInt) || !int.TryParse(dto.groupId, out int groupIdInt))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, SystemConstants.Message.InValidNumber);
                }

                T_Point data = await _mapPointService.CheckMapPoint(userIdInt, groupIdInt, dto.addressCode);

                if (data != null)
                    return Ok(new { id = data?.Point_ID });
                else
                    return Ok(new {id = (int?) null});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }
    }
}
