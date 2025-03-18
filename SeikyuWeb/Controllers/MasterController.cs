using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeikyuWeb.Common;
using SeikyuWeb.Dto;
using SeikyuWeb.Dto.MasterDto;
using SeikyuWeb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace SeikyuWeb.Controllers
{
    /// <summary>
    /// マスターコントローラー
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MasterController : ControllerBase
    {
        private readonly ILogger<MasterController> _logger;
        private readonly IMasterService _masterService;

        public MasterController(ILogger<MasterController> logger,
            IMasterService masterService)
        {
            _logger = logger;
            _masterService = masterService;
        }

        /// <summary>
        /// マスターコードデータを取得
        /// </summary>
        /// <param name="codeid">コードID</param>
        /// <returns>
        /// 200: 成功
        /// 404: ページが見つかりません。
        /// 401: 許可されていません。
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpGet("code/{codeid}")]
        [Authorize]
        [ProducesResponseType(typeof(List<CodeDataDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetMasterCodeData([Required] int codeid)
        {
            try
            {
                if (codeid == 0)
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.NotFound, SystemConstants.Message.DataNotFound);
                }

                IEnumerable<CodeDataDto> data = await _masterService.GetMasterCodeDataAsync(codeid);

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
