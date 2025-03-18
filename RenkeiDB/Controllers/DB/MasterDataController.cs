using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RenkeiDB.Common;
using RenkeiDB.Dto;
using RenkeiDB.Dto.EquipmentDto;
using RenkeiDB.Dto.MasterDto;
using RenkeiDB.Services;
using RenkeiDB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RenkeiDB.Controllers.DB
{
    /// <summary>
    /// マスターデータに関する操作を提供するコントローラー
    /// </summary>
    [ApiController]
    [Route("api/master")]
    public class MasterDataController : ControllerBase
    {
        private readonly ILogger<MasterDataController> _logger;
        private readonly IMasterDataService _masterDataService;
        private readonly ILuggageService _luggageService;
        private readonly IDefaultMoneyService _defaultMoneyService;

        public MasterDataController(IMasterDataService masterDataService, ILuggageService luggageService,IDefaultMoneyService defaultMoneyService, ILogger<MasterDataController> logger)
            => (_masterDataService, _luggageService, _defaultMoneyService, _logger) = (masterDataService, luggageService, defaultMoneyService, logger);

        #region M_Code_Data
        /*****************************************************************************
         M_Code_Data
         *****************************************************************************/
        /// <summary>
        /// マスタ―情報の取得
        /// </summary>
        /// <param name="codeId">コードID</param>
        /// <returns>マスタ―情報</returns>
        [HttpGet("code/{codeId}")]
        public async Task<IActionResult> GetMasterCodeData(string codeId)
        {
            try
            {
                // クッキーの値を取得する
                int loginId = User.RequiredUserID();
                // codeIdがnullの場合、BadRequestを返す
                if (codeId is null)
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, string.Format(SystemConstants.Message.RequiredField, "CodeId"));
                }
                // codeIdがint型に変換できない場合、BadRequestを返す
                if (!int.TryParse(codeId, out int intValue))
                {
                    // codeIdが数字でない場合、BadRequestを返す
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, string.Format(SystemConstants.Message.InValidNumber, "CodeId"));
                }
                // codeIdに対応するデータを取得する
                IEnumerable<MasterCodeDataDto> data = await _masterDataService.GetMasterCodeData(intValue);

                return Ok(data);
            }
            // UnauthorizedAccessExceptionが発生した場合、Unauthorizedを返す
            catch (UnauthorizedAccessException)
            {
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.Unauthorized, SystemConstants.Message.Unauthorized);
            }
            // その他の例外が発生した場合、InternalServerErrorを返す
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }
        #endregion M_Code_Data

        #region M_PostCode
        /*****************************************************************************
         M_PostCode
         *****************************************************************************/
        /// <summary>
        /// 都道府県名の取得
        /// </summary>
        /// <returns>都道府県名の配列</returns>
        [HttpGet("post-code")]
        [Authorize]
        public async Task<IActionResult> GetPostCode()
        {
            try
            {
                IEnumerable<string> data = await _masterDataService.GetPostCodeData();

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }
        #endregion M_PostCode

        #region M_Luggage
        /// <summary>
        /// 荷物詳細追加（荷物情報ダイアログ）
        /// </summary>
        /// <returns>
        /// 200: 成功
        /// 401: アクセスは許可されていません。
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [Authorize]
        [HttpPost("luggage")]
        public async Task<IActionResult> CreateLuggage([FromBody] LuggageDataDto luggage)
        {
            try
            {
                // ユーザーの会社IDとユーザーIDを取得する
                var (cid, uid) = User.RequiredCompanyIdUserID();
                // 荷物情報を作成する
                await _luggageService.Create(cid, uid, luggage);
                return Ok();
            }
            // UnauthorizedAccessExceptionが発生した場合、Unauthorizedを返す
            catch (UnauthorizedAccessException)
            {
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.Unauthorized, SystemConstants.Message.Unauthorized);
            }
            // その他の例外が発生した場合、InternalServerErrorを返す
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }
        #endregion

        #region M_DefaultMoney
        /// <summary>
        /// マスター情報取得（M_DefaultMoney）
        /// </summary>
        /// <returns>
        /// 200: 荷物詳細追加
        /// 401: アクセスは許可されていません。
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [Authorize]
        [HttpGet("default-money")]
        public async Task<IActionResult> GetDefaultMoneys()
        {
            try
            {
                IEnumerable<DefaultMoneyDto> data = await _defaultMoneyService.GetDefaultMoneysAsync();
                return Ok(data);
            }
            // UnauthorizedAccessExceptionが発生した場合、Unauthorizedを返す
            catch (UnauthorizedAccessException)
            {
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.Unauthorized, SystemConstants.Message.Unauthorized);
            }
            // その他の例外が発生した場合、InternalServerErrorを返す
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }
        #endregion


        #region M_Equipment
        /// <summary>
        /// 装備品の作成
        /// </summary>
        /// <param name="dto">装備品データ</param>
        /// <returns>
        /// 200: 成功
        /// 400: パラメータが不正となります。
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpPost("equipment")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateEquipment(CreateEquipmentDto dto)
        {
            try
            {
                // ユーザーの会社IDとユーザーIDを取得する
                var (cid, uid) = User.RequiredCompanyIdUserID();

                // データを使用してEquipmentを作成します
                ApiResponse result = await _masterDataService.CreateEquipmentAsync(cid, uid, dto);

                // メッセージがnullでない場合
                if (result.Message != null)
                {
                    return ApiResponseCommon.CreateApiResponse((HttpStatusCode)result.Code, result.Message);
                }

                return Ok(new { });
            }
            // UnauthorizedAccessExceptionが発生した場合、Unauthorizedを返す
            catch (UnauthorizedAccessException)
            {
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.Unauthorized, SystemConstants.Message.Unauthorized);
            }
            // その他の例外が発生した場合、InternalServerErrorを返す
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// 装備品グループの取得
        /// </summary>
        /// <returns>
        /// 200: 成功
        /// 500: サーバーエラー
        /// </returns>
        [HttpGet("equipment-groups")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetEquipmenentGroup()
        {
            try
            {
                // ユーザーの会社IDを取得する
                string companyId = User.FindFirst("CompanyId")?.Value;

                // companyIdがnullの場合、Unauthorizedを返す
                if (companyId == null)
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.Unauthorized, SystemConstants.Message.Unauthorized);
                }

                // Equipmentグループを取得する
                IEnumerable< EquipmentGroupDto> data = await _masterDataService.GetEquipmentGroupAsync(int.Parse(companyId));

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }
        #endregion

        #region M_Syaryo
        /// <summary>
        /// 車両情報の取得
        /// </summary>
        /// <param name="syasu">車種</param>
        /// <param name="kata">型</param>
        /// <returns>車両情報</returns>
        [HttpGet("syaryo")]
        [Authorize]
        public async Task<IActionResult> GetSyaryo([FromQuery]string syasu = "", [FromQuery]string kata = "")
        {
            try
            {
                IList<SyaryoDto> data = await _masterDataService.GetSyaryoAsync(syasu, kata);

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }
        #endregion M_PostCode
    }
}
