using HaisyaWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HaisyaWeb.API.WebApp
{
    class IMAIDataApi : BaseHttpClient
    {
        public IMAIDataApi(MapApiSettings mapApiSetting)
        {
            _baseUrl = mapApiSetting.Api.WebAPIHosts;
        }

        #region V_Tokuisaki
        public async Task<List<Dto.V_Tokuisaki_Local>> V_TokuisakiList(int CompanyID, string code = null, string key = null, string phone = null)
        {
            string url = _baseUrl + string.Format("IMAIData/V_TokuisakiList?CompanyID={0}", CompanyID);
            if (code != null) { url += string.Format("&Code={0}", code); }
            if (key != null) { url += string.Format("&Key={0}", key); }
            if (phone != null) { url += string.Format("&Phone={0}", phone); }

            //データ取得
            return await GetHttpData<List<Dto.V_Tokuisaki_Local>>(url);
        }
        #endregion V_Tokuisaki

        #region V_TokuisakiForNotConnect
        public async Task<List<Dto.V_TokuisakiForNotConnect_Local>> GetTokuisakiForNotConnectList(int CompanyID, string code = null, string key = null, string phone = null)
        {
            string url = _baseUrl + string.Format("IMAIData/GetTokuisakiForNotConnectList?CompanyID={0}", CompanyID);
            if (code != null) { url += string.Format("&Code={0}", code); }
            if (key != null) { url += string.Format("&Key={0}", key); }
            if (phone != null) { url += string.Format("&Phone={0}", phone); }

            //データ取得
            return await GetHttpData<List<Dto.V_TokuisakiForNotConnect_Local>>(url);
        }
        #endregion V_TokuisakiForNotConnect

        #region V_YosyasakiForNotConnect
        public async Task<List<Dto.V_YosyasakiForNotConnect_Local>> GetYosyasakiForNotConnectList(int CompanyID, string code = null, string key = null, string phone = null)
        {
            string url = _baseUrl + string.Format("IMAIData/GetYosyasakiForNotConnectList?CompanyID={0}", CompanyID);
            if (code != null) { url += string.Format("&Code={0}", code); }
            if (key != null) { url += string.Format("&Key={0}", key); }
            if (phone != null) { url += string.Format("&Phone={0}", phone); }

            //データ取得
            return await GetHttpData<List<Dto.V_YosyasakiForNotConnect_Local>>(url);
        }
        #endregion V_YosyasakiForNotConnect
    }
}
