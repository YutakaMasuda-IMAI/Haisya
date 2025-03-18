using HaisyaWeb.Common;
using HaisyaWeb.Models;
using System;
using System.Threading.Tasks;

namespace HaisyaWeb.API.Map
{
    internal class AddressApi : BaseHttpClient
    {

        public AddressApi(MapApiSettings mapApiSetting)
        {
            _baseUrl = mapApiSetting.Api.WebAPIHosts;
            httpClient.BaseAddress = new Uri(_baseUrl);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="range"></param>
        /// <param name="datum"></param>
        /// <returns></returns>
        public async Task<MapApiModel.MapAddress_Local> GetAddressDataAsync(string position, int range, 
                                                    string datum = SystemConstants.Zenrin_datum.日本測地系_ゼンリンナビ地図)
        {
            String url = "api/MapAddress?dummy=1";
            if (position != null) { url += string.Format("&position={0}", position); }
            url += string.Format("&datum={0}", datum);
            url += string.Format("&range={0}", range);
            //データ取得
            return await GetHttpData<MapApiModel.MapAddress_Local>(url);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="addressVal"></param>
        /// <returns></returns>
        public async Task<MapApiModel.MapAddress_Local> GetlatlonFromAddressAsync(string addressVal)
        {
            String url = "api/MapAddress/Getlatlon?address=" + addressVal;
            //データ取得
            return await GetHttpData<MapApiModel.MapAddress_Local>(url);
        }


        /// <summary>
        /// 建物・テナント名称検索
        /// </summary>
        /// <param name="addressVal"></param>
        /// <returns></returns>
        public async Task<MapApiModel.Map_Building_Name_Local> GetBuildingNameAsync(string address_code, string word)
        {
            String url = "api/MapAddress/GetBuildingName?dummy=1";
            if (address_code != null) { url += string.Format("&address_code_list={0}", address_code); }
            if (word != null) { url += string.Format("&word_match_type=3&word={0}", word); }
            //データ取得
            return await GetHttpData<MapApiModel.Map_Building_Name_Local>(url);
        }

    }
}
