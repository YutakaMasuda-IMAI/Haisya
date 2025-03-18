using HaisyaDesktop.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static HaisyaDesktop.Models.MapApiModel;

namespace HaisyaDesktop.API.Map
{
    internal class AddressApi : BaseHttpClient
    {

        public AddressApi() {
            ////this.baseUrl = "";
            //// 通信するメソッドでその都度HttpClientをnewすると毎回ソケットを開いてリソースを消費するため、
            //// メンバ変数で使い回す手法を取っています。
            //this.httpClient = new HttpClient();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="poition"></param>
        /// <returns></returns>
        public async Task<MapAddress_Local> GetAddressDataAsync(string poition)
        {
            String url = PublicObjects.GetWebAPIHosts() + "api/MapAddress?positionl=" + poition;
            //データ取得
            return await GetHttpData<MapAddress_Local>(url);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="addressVal"></param>
        /// <returns></returns>
        public async Task<MapAddress_Local> GelatlonFromAddressAsync(string addressVal)
        {
            String url = PublicObjects.GetWebAPIHosts()+ "api/MapAddress/Getlatlon?address=" + addressVal;
            //データ取得
            return await GetHttpData<MapAddress_Local>(url);
        }


        /// <summary>
        /// 建物・テナント名称検索
        /// </summary>
        /// <param name="addressVal"></param>
        /// <returns></returns>
        public async Task<Map_Building_Name_Local> GetBuildingNameAsync(string address_code, string word)
        {
            String url = PublicObjects.GetWebAPIHosts()+ "api/MapAddress/GetBuildingName?dummy=1";
            if (address_code != null) { url += string.Format("&address_code_list={0}", address_code); }
            if (word != null) { url += string.Format("&word_match_type=3&word={0}", word); }
            //データ取得
            return await GetHttpData<Map_Building_Name_Local>(url);
        }

    }
}
