using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RenkeiDB.Common;
using RenkeiDB.Dto;
using RenkeiDB.Dto.InfoDto;
using RenkeiDB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RenkeiDB.Controllers
{
    /// <summary>
    /// お知らせに関する操作を提供するコントローラー
    /// </summary>
    [Route("api/infos")]
    [ApiController]
    public class InfosController : ControllerBase
    {
        private readonly ILogger<InfosController> _logger;
        private readonly IInfoService _infoService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="infoService">お知らせサービス</param>
        /// <param name="logger">ロガー</param>
        public InfosController(IInfoService infoService, ILogger<InfosController> logger)
        {
            _infoService = infoService;
            _logger = logger;
        }

        /// <summary>
        /// ログイン情報に紐づくお知らせを返却
        /// </summary>
        /// <returns>
        /// 200: ログイン情報に紐づくお知らせを返却
        /// 400: パラメータが不正となります。
        /// 404: データがみつかりません。
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpGet()]
        [Authorize]
        [ProducesResponseType(typeof(InfoDto[]), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetListInfo()
        {
            try
            {
                // クッキーの値を取得
                (int cid, int uid) = User.RequiredCompanyIdUserID();
                // ログイン情報に紐づくお知らせを取得
                List<InfoDto> result = await _infoService.GetListInfo(uid, cid);

                return Ok(result);
            }
            // アクセスは許可されていません。
            catch (UnauthorizedAccessException)
            {
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.Unauthorized, SystemConstants.Message.Unauthorized);
            }
            // データが存在しません。
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// お知らせ閲覧フラグ更新
        /// </summary>
        /// <returns>
        /// 200: お知らせ閲覧フラグ更新
        /// 401: アクセスは許可されていません。
        /// 404: データが存在しません。
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [Authorize]
        [HttpPut("{id}/display")]
        public async Task<IActionResult> Display([FromRoute] int id, [FromBody] DisplayFlgDto display)
        {
            try
            {
                // クッキーの値を取得
                (int cid, int uid) = User.RequiredCompanyIdUserID();
                // お知らせ閲覧フラグを更新
                await _infoService.Set_display_flg(uid, cid, id, display.displayFlg);

                return Ok();
            }
            // アクセスは許可されていません。
            catch (UnauthorizedAccessException)
            {
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.Unauthorized, SystemConstants.Message.Unauthorized);
            }
            // データが存在しません。
            catch (DataNotFoundException)
            {
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.NotFound, SystemConstants.Message.DataNotFound);
            }
            // リクエストの処理中にエラーが発生しました。
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }
    }
}
