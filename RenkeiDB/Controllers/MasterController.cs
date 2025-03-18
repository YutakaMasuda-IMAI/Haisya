using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RenkeiDB.Common;
using RenkeiDB.Dto;
using RenkeiDB.Dto.MasterDto;
using RenkeiDB.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace RenkeiDB.Controllers.DB
{
    /// <summary>
    /// マスターに関する操作を提供するコントローラー
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/")]
    public class MasterController : ControllerBase
    {
        private readonly ILogger<MasterController> _logger;
        private readonly IMasterDataService _masterDataService;

        public MasterController(IMasterDataService masterDataService, ILogger<MasterController> logger)
        {
            _masterDataService = masterDataService;
            _logger = logger;
        }

        /// <summary>
        /// 住所選択　API（郵便番号取得）
        /// </summary>
        /// <returns>郵便番号を取得</returns>
        [HttpGet("post-code/{ken}/{shikucho}/{choiki}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetPostCode([Required] string ken, [Required] string shikucho,
            [Required] string choiki)
        {
            try
            {
                string data = await _masterDataService.GetPostCodeAsync(ken, shikucho, choiki);

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// 住所一覧取得
        /// </summary>
        /// <param name="word">検索ワード</param>
        /// <returns>住所一覧</returns>
        [HttpGet("address/search/{word}")]
        [ProducesResponseType(typeof(IList<AddressDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAddressList([Required] string word)
        {
            try
            {
                IList<AddressDto> data = await _masterDataService.GetAddressList(word);
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
