using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaisyaDesktop.API.WebApp
{
    class HaisyaDataApi : BaseHttpClient
    {

        public HaisyaDataApi()
        {
        }

        //#region HeisyaSyaryoDto
        ///// <summary>
        ///// PROC_Heisya_Syaryoを実行してデータを返却
        ///// </summary>
        ///// <param name="CompanyID"></param>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public async Task<List<Dto.HeisyaSyaryoDto_Local>> GetProcHeisyaSyaryoData(int CompanyID, string targetDate)
        //{
        //    string url = PublicObjects.GetWebAPIHosts() + string.Format("Haisya/GetProcHeisyaSyaryo?CompanyID={0}", CompanyID);
        //    url += string.Format("&targetDate={0}", targetDate.Replace("/", "-"));
        //    //データ取得
        //    return await GetHttpData<List<Dto.HeisyaSyaryoDto_Local>>(url);
        //}
        //#endregion HeisyaSyaryoDto


    }
}
