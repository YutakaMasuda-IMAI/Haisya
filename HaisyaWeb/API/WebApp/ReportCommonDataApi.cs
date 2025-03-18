using HaisyaWeb.Dto;
using HaisyaWeb.Models;
using System.Threading.Tasks;

namespace HaisyaWeb.API.WebApp
{
    /// <summary>
    /// Class ReportCommonDataApi は共通帳票のデータを取得するために使用
    /// </summary>
    class ReportCommonDataApi : BaseHttpClient
    {
        public ReportCommonDataApi(MapApiSettings mapApiSetting)
        {
            _baseUrl = mapApiSetting.Api.WebAPIHosts;
        }

        /// <summary>
        /// レポートデータ取得API
        /// </summary>
        /// <param name="reportSearchKubunId"> 区分検索レポートのid</param>
        /// <param name="jsonData">プロシージャのパラメータの json データ</param>
        /// <param name="userId">ユーザーのid</param>
        /// <param name="companyId">会社のid</param>
        /// <returns> ReportCommonDtoLocalのデータ</returns>
        public async Task<ReportCommonDtoLocal> GetDataReport(int reportSearchKubunId, string jsonData, int userId, int companyId)
        {
            string url = _baseUrl + string.Format("ReportCommon/{0}?jsonData={1}&userId={2}&companyId={3}", reportSearchKubunId, jsonData, userId, companyId);
            return await GetHttpData<ReportCommonDtoLocal>(url);
        }
    }
}
