using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RenkeiDB.Common;
using RenkeiDB.Controllers.DB;
using RenkeiDB.Dto;
using RenkeiDB.Services;
using RenkeiDB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RenkeiDB.Controllers
{
    /// <summary>
    /// 住所に関するAPIコントローラー
    /// </summary>
    [ApiController]
    [Route("api/address")]
    public class AddressController : Controller
    {
        private readonly ILogger<MasterDataController> _logger;
        private IMasterDataService _masterDataService;
        private IAddressService _addressService;

        public AddressController(IMasterDataService masterDataService, IAddressService addressService, ILogger<MasterDataController> logger)
        {
            _masterDataService = masterDataService;
            _addressService = addressService;
            _logger = logger;
        }

        #region M_PostCode
        /// <summary>
        /// 住所情報の取得
        /// </summary>
        /// <param name="ken">都道府県名</param>
        /// <param name="shikucho">市区町村名</param>
        /// <returns>住所情報</returns>
        /// <returns>
        /// 200: 成功
        /// 400: パラメータが不正となります。
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpGet("{ken}")]
        [Authorize]
        public async Task<IActionResult> GetAddressByKen(string ken, [FromQuery] string shikucho)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ken))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, string.Format(SystemConstants.Message.RequiredField, "ken"));
                }

                IEnumerable<string> data = await _masterDataService.GetAddressByKenAsync(ken, shikucho);

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }
        #endregion M_PostCode
        
        #region T_Renkei_Anken_Point
        /// <summary>
        /// 住所履歴取得
        /// </summary>
        /// <returns>住所履歴情報</returns>
        /// <returns>
        /// 200: OK
        /// 401: Unauthorized
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpGet("history")]
        [Authorize]
        public async Task<IActionResult> GetHistoryAddress()
        {
            try
            {
                AddressHistoryDto result = await _addressService.GetRenkeiAnkenPointListAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }
        #endregion T_Renkei_Anken_Point
    }
}
