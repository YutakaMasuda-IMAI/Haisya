using HaisyaWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HaisyaWeb.API.WebApp
{
    class ShitabaraiInquiryModifyListApi : BaseHttpClient
    {

        public ShitabaraiInquiryModifyListApi(MapApiSettings mapApiSetting)
        {
            _baseUrl = mapApiSetting.Api.WebAPIHosts;
        }

        /// <summary>
        /// 指定されたパラメータに基づいて仕払データのリストを取得します。
        /// </summary>
        /// <param name="CompanyID">会社ID。</param>
        /// <param name="shiharaiNengetsu">支払い年月（YYYYMM形式）。</param>
        /// <param name="shimeDay">締め日。</param>
        /// <param name="shiharaiTantou">支払い担当者名。</param>
        /// <param name="zeiKubun">税区分。</param>
        /// <param name="inquiryStatus">問い合わせステータス。</param>
        /// <param name="shiharaiChanged">支払いの変更状態。</param>
        /// <param name="yosyasakiFrom">傭車先の開始コード。</param>
        /// <param name="yosyasakiTo">傭車先の終了コード。</param>
        /// <param name="checkShitabaraiId">（オプション）チェック支払ID。デフォルト値は0。</param>
        /// <returns>仕払データリストの非同期タスクを返します。</returns>
        public async Task<List<Dto.V_ShitabaraiCheckDataList_Local>> GetShitabaraiCheckDataList(int CompanyID, string shiharaiNengetsu, int? shimeDay, string shiharaiTantou, int? zeiKubun, int? inquiryStatus, int? shiharaiChanged, string yosyasakiFrom, string yosyasakiTo, int? checkShitabaraiId)
        {
            string url = _baseUrl + string.Format("ShitabaraiInquiryModifyList/GetShitabaraiInquiryModifyList?CompanyID={0}&shiharaiNengetsu={1}&shimeDay={2}&shiharaiTantou={3}&zeiKubun={4}&inquiryStatus={5}&shiharaiChanged={6}&yosyasakiFrom={7}&yosyasakiTo={8}&checkShitabaraiId={9}", CompanyID.ToString(), shiharaiNengetsu, shimeDay ?? 0, shiharaiTantou ?? "", zeiKubun ?? 0 , inquiryStatus ?? 0 , shiharaiChanged ?? 0, yosyasakiFrom ?? "", yosyasakiTo ?? "", checkShitabaraiId ?? 0);
            //データ取得
            return await GetHttpData<List<Dto.V_ShitabaraiCheckDataList_Local>>(url);
        }
    }
}