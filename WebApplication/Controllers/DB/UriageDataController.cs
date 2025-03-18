using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Services;
using System;
using WebApplication.Common;

namespace WebApplication.Controllers.DB
{
    /// <summary>
    /// 売上データのコントローラークラス
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UriageDataController : MyBaseController
    {
        private readonly ILogger<UriageDataController> _logger;
        private readonly IUriageDataListService _uriageDataListService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger">ロガー</param>
        /// <param name="uriageDataListService">売上データリストサービス</param>
        public UriageDataController(ILogger<UriageDataController> logger, IUriageDataListService uriageDataListService)
        {
            _logger = logger;
            _uriageDataListService = uriageDataListService;
        }

        /// <summary>
        /// 売上データリストを取得します。
        /// </summary>
        /// <param name="companyId">会社ID。</param>
        /// <param name="uriageKubun">売上区分。</param>
        /// <param name="tourokuKubun">登録区分。</param>
        /// <param name="fromDate">開始日（YYYY-MM-DD形式）。</param>
        /// <param name="toDate">終了日（YYYY-MM-DD形式）。</param>
        /// <param name="fromTokuisaki">（文字列）。</param>
        /// <param name="toTokuisaki">（文字列）。</param>
        /// <param name="fromYosya">（文字列）。</param>
        /// <param name="toYosya">（文字列）。</param>
        /// <param name="shimeDay">締日。</param>
        /// <param name="seikyudateTo">請求日終了（文字列）。</param>
        /// <param name="seikyuTantou">請求担当者ID。</param>
        /// <param name="haisyaTantou">配車担当者ID。</param>
        /// <param name="driverId">運転手ID。</param>
        /// <returns>非同期操作を表すタスク。結果として<see cref="IEnumerable{V_UriageDataList}"/>を返します。</returns>
        [HttpGet("GetUriageDataList")]
        public async Task<IActionResult> GetUriageDataList(int companyId, int uriageKubun, int tourokuKubun, string fromDate, string toDate, string fromTokuisaki, string toTokuisaki, string fromYosya, string toYosya, int shimeDay, string seikyudateTo, int seikyuTantou, int haisyaTantou, int driverId)
        {
            try
            {
                string fromT = "";
                string toT = "";
                if (fromTokuisaki != null)
                {
                    fromT = fromTokuisaki;
                }
                if (toTokuisaki != null)
                {
                    toT = toTokuisaki;
                }
                IEnumerable<V_UriageDataList> uriageDataList = await _uriageDataListService.GetUriageDataList(companyId, uriageKubun, tourokuKubun, fromDate, toDate, fromT, toT, fromYosya, toYosya, shimeDay, seikyudateTo, seikyuTantou, haisyaTantou, driverId);
                return new OkObjectResult(uriageDataList);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
        }

        /// <summary>
        /// 売上リストを取得する
        /// </summary>
        /// <param name="ids">売上のidリスト</param>
        /// <returns>売上リスト（T_Uriage）のIEnumerableを表すTask。</returns>
        [HttpPost("GetUriageByIds")]
        public async Task<IActionResult> GetUriageByIds([FromBody] List<int> ids)
        {
            try
            {
                IEnumerable<T_Uriage> uriageDataList = await _uriageDataListService.GetUriageByIds(ids);
                return new OkObjectResult(uriageDataList);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
        }
    }
}

