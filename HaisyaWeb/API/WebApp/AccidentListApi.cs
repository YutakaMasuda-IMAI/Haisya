using HaisyaWeb.Dto;
using HaisyaWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HaisyaWeb.API.WebApp
{
    class AccidentListApi : BaseHttpClient
    {
        public AccidentListApi(MapApiSettings mapApiSetting) => _baseUrl = mapApiSetting.Api.WebAPIHosts;

        /// <summary>
        /// 指定された条件に基づいて事故リストを取得します。
        /// </summary>
        /// <param name="companyId">会社のID。</param>
        /// <param name="userId">ユーザーのID。</param>
        /// <param name="jikoKubun">事故区分。</param>
        /// <param name="loginUser">ログインユーザーかどうかを示すブール値。</param>
        /// <param name="jikoDisplay">事故の表示名（オプション）。</param>
        /// <param name="fromDate">検索開始日（オプション）。</param>
        /// <param name="toDate">検索終了日（オプション）。</param>
        /// <param name="displayName">表示名（オプション）。</param>
        /// <param name="syabanNumber">車輛番号（オプション）。</param>
        /// <returns>指定された条件に基づいて取得した <see cref="List{AccidentListItem_Local}"/> のリスト。</returns>
        /// <remarks>
        /// このメソッドは、指定された条件に基づいて事故リストを非同期に取得します.
        /// 指定されたパラメータを使用して事故データをフィルタリングし、条件に一致する事故情報をリストとして返します.
        /// </remarks>
        public async Task<List<AccidentListItem_Local>> GetAccidentList(
            int companyId,
            int userId,
            int jikoKubun,
            bool loginUser,
            string jikoDisplay = "",
            DateTime fromDate = default,
            DateTime toDate = default,
            string displayName = "",
            string syabanNumber = "")
        {
            string url = _baseUrl + string.Format(
                "AccidentList/GetAccidentList?companyId={0}&userId={1}&jikoKubun={2}&loginUser={3}&jikoDisplay={4}&fromDate={5}&toDate={6}&displayName={7}&syabanNumber={8}",
                companyId, userId, jikoKubun, loginUser, jikoDisplay, fromDate, toDate, displayName, syabanNumber);
            return await GetHttpData<List<AccidentListItem_Local>>(url);
        }

        /// <summary>
        /// 指定されたコードIDに基づいてM_Codeデータリストを取得します。
        /// </summary>
        /// <param name="codeId">取得するコードのID。</param>
        /// <returns>指定されたコードIDに基づいて取得した <see cref="List{M_Code_Data_Local}"/> のリスト。</returns>
        /// <remarks>
        /// このメソッドは、指定されたコードIDに基づいて M_Code データを非同期に取得します.
        /// 指定されたIDに関連する M_Code のデータリストを返します.
        /// </remarks> 
        public async Task<List<M_Code_Data_Local>> GetMCodeList(int codeId)
        {
            string url = _baseUrl + string.Format("AccidentList/GetMCodeList?codeId={0}", codeId);
            return await GetHttpData<List<M_Code_Data_Local>>(url);
        }
    }
}
