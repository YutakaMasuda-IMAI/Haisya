using HaisyaWeb.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HaisyaWeb.API.WebApp
{
    class ExpenseDataApi : BaseHttpClient
    {
        public ExpenseDataApi(MapApiSettings mapApiSetting)
            => _baseUrl = mapApiSetting.Api.WebAPIHosts;

        /// <summary>
        /// 指定された条件に基づいて経費データのリストを取得します。
        /// </summary>
        /// <param name="company_id"></param>
        /// <param name="expense_category"></param>
        /// <param name="vehicle_number"></param>
        /// <param name="driver_code"></param>
        /// <param name="driver_name"></param>
        /// <param name="accident_year"></param>
        /// <returns></returns>
        public async Task<List<Dto.V_ExpenseDataList_Local>> GetExpenseDataList(
            int company_id,
            int expense_category,
            int? vehicle_number,
            string driver_code,
            string driver_name,
            int accident_year)
        {
            StringBuilder b = new StringBuilder(_baseUrl).Append("ExpenseData/GetExpenseDataList?");

            b.AppendFormat("company_id={0}", company_id);
            b.AppendFormat("&expense_category={0}", expense_category);
            if (vehicle_number != null)
                b.AppendFormat("&vehicle_number={0}", vehicle_number.Value);
            if (!string.IsNullOrWhiteSpace(driver_code))
                b.AppendFormat("&driver_code={0}", HttpUtility.UrlEncode(driver_code));
            if (!string.IsNullOrWhiteSpace(driver_name))
                b.AppendFormat("&driver_name={0}", HttpUtility.UrlEncode(driver_name));
            b.AppendFormat("&accident_year={0}", accident_year);

            return await GetHttpData<List<Dto.V_ExpenseDataList_Local>>(b.ToString());
        }
    }
}
