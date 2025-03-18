using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RenkeiDB.Common;
using RenkeiDB.Dto.CarInfoDto;
using RenkeiDB.Services.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;

namespace RenkeiDB.Controllers
{
    /// <summary>
    /// 車両情報に関するAPIコントローラー
    /// </summary>
    [Route("api")]
    [ApiController]
    public class CarInfoController : ControllerBase
    {
        private readonly ILogger<CarInfoController> _logger;
        private ICarInfoService _carInfoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CarInfoController(ILogger<CarInfoController> logger, ICarInfoService carInfoService, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _carInfoService = carInfoService;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 車番から最新の車両情報の取得
        /// </summary>
        /// <param name="carNo">車番号</param>
        /// <returns>車両情報</returns>
        /// <returns>
        /// 200: 成功
        /// 400: パラメータが不正となります。
        /// 404: データがみつかりません。
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpGet("car-info/{carNo}")]
        [Authorize]
        public async Task<IActionResult> GetCarInfo(string carNo)
        {
            try
            {
                ISession session = _httpContextAccessor.HttpContext.Session;
                if (string.IsNullOrWhiteSpace(carNo))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, string.Format(SystemConstants.Message.RequiredField, "車番号"));
                }

                string groupIdsSession = session.GetString("GroupIds");
                if (string.IsNullOrEmpty(groupIdsSession))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.NotFound, SystemConstants.Message.DataNotFound);
                }
                int[] groupIds = System.Text.Json.JsonSerializer.Deserialize<int[]>(groupIdsSession);
                CarInfoDto data = await _carInfoService.GetCarInfoAsync(carNo, groupIds);
                if (data is null)
                {
                    return Ok(new { });
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }
    }
}
