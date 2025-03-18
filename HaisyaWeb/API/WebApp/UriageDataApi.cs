using HaisyaWeb.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HaisyaWeb.API.WebApp
{
    class UriageDataApi: BaseHttpClient
    {

        public UriageDataApi(MapApiSettings mapApiSetting)
        {
            _baseUrl = mapApiSetting.Api.WebAPIHosts;
        }

        /// <summary>
        /// 売上データリストを取得します。
        /// </summary>
        /// <param name="companyId">会社ID。</param>
        /// <param name="uriageKubun">売上区分。</param>
        /// <param name="tourokuKubun">登録区分。</param>
        /// <param name="fromDate">開始日（YYYY-MM-DD形式）。</param>
        /// <param name="toDate">終了日（YYYY-MM-DD形式）。</param>
        /// <param name="fromTokuisaki">取引先開始（文字列）。</param>
        /// <param name="toTokuisaki">（文字列）。</param>
        /// <param name="fromYosya">（文字列）。</param>
        /// <param name="toYosya">（文字列）。</param>
        /// <param name="shimeDay">締日。</param>
        /// <param name="seikyudateTo">請求日終了（文字列）。</param>
        /// <param name="seikyuTantou">請求担当者ID。</param>
        /// <param name="haisyaTantou">配車担当者ID。</param>
        /// <param name="driverId">運転手ID。</param>
        /// <returns>非同期操作を表すタスク。結果として<see cref="List{Dto.V_UriageDataList}"/>を返します。</returns>
        public async Task<List<Dto.V_UriageDataList>> GetUriageDataList(int companyId, int uriageKubun, int tourokuKubun, string fromDate, string toDate, string fromTokuisaki, string toTokuisaki, string fromYosya, string toYosya, int shimeDay, string seikyudateTo, int seikyuTantou, int haisyaTantou, int driverId)
        {
            string url = _baseUrl + string.Format("UriageData/GetUriageDataList?CompanyId={0}&uriageKubun={1}&tourokuKubun={2}&fromDate={3}&toDate={4}&fromTokuisaki={5}&toTokuisaki={6}&fromYosya={7}&toYosya={8}&shimeDay={9}&seikyudateTo={10}&seikyuTantou={11}&haisyaTantou={12}&driverId={13}", companyId, uriageKubun, tourokuKubun, fromDate, toDate, fromTokuisaki, toTokuisaki, fromYosya, toYosya, shimeDay, seikyudateTo, seikyuTantou, haisyaTantou, driverId);
            //データ取得
            return await GetHttpData<List<Dto.V_UriageDataList>>(url);
        }

        /// <summary>
        /// 売上リストを取得する
        /// </summary>
        /// <param name="ids">売上のidリスト</param>
        /// <returns>売上リスト</returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<List<Dto.T_Uriage_Local>> GetUriageByIds(List<int> ids)
        {
            var response = await MakePostGetRequestAsync<List<int>, List<Dto.T_Uriage_Local>>(ids, _baseUrl, "UriageData/GetUriageByIds");
            if (response.Success == false)
            {
                throw new HttpRequestException(response.Message);
            }
            return response.Data;
        }
    }
}