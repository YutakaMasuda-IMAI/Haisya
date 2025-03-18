using PartnerWeb.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PartnerWeb.API.Map
{
    internal class AddressApi : BaseHttpClient
    {

        public AddressApi(MapApiSettings mapApiSetting)
        {
            _baseUrl = mapApiSetting.Api.WebAPIHosts;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="poition"></param>
        /// <returns></returns>
        public async Task<MapApiModel.MapAddress_Local> GetAddressDataAsync(string poition)
        {
            String requestEndPoint = this._baseUrl + "api/MapAddress?positionl=" + poition;
            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, requestEndPoint);

            string resBodyStr;
            MapApiModel.MapAddress_Local address;

            HttpStatusCode resStatusCoode = HttpStatusCode.NotFound;

            HttpResponseMessage response;

            // 通信実行。メンバ変数でhttpClientを持っているので、using(～)で囲いません。囲うと通信後にオブジェクトが破棄されます。
            // 引数にrequestを取る場合はGetAsyncやPostAsyncでなくSendAsyncメソッドになります。
            // 戻り値はTask<HttpResponseMessage>で、変数名.ResultとするとSystem.Net.Http.HttpResponseMessageクラスが取れます。
            try
            {
                response = await httpClient.SendAsync(request);
                resBodyStr = response.Content.ReadAsStringAsync().Result;
                resStatusCoode = response.StatusCode;

                address = JsonConvert.DeserializeObject<MapApiModel.MapAddress_Local>(resBodyStr);

            }
            catch (HttpRequestException e)
            {
                // UNDONE: 通信失敗のエラー処理
                return null;
            }

            if (!resStatusCoode.Equals(HttpStatusCode.OK))
            {
                // UNDONE: レスポンスが200 OK以外の場合のエラー処理
                return null;
            }
            if (String.IsNullOrEmpty(resBodyStr))
            {
                // UNDONE: レスポンスのボディが空の場合のエラー処理
                return null;
            }
            // 中身のチェックなどを経て終了。
            return address;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="addressVal"></param>
        /// <returns></returns>
        public async Task<MapApiModel.MapAddress_Local> GetlatlonFromAddressAsync(string addressVal)
        {
            String requestEndPoint = _baseUrl + "api/MapAddress/Getlatlon?address=" + addressVal;
            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, requestEndPoint);

            string resBodyStr;
            MapApiModel.MapAddress_Local address;

            HttpStatusCode resStatusCoode = HttpStatusCode.NotFound;

            HttpResponseMessage response;

            // 通信実行。メンバ変数でhttpClientを持っているので、using(～)で囲いません。囲うと通信後にオブジェクトが破棄されます。
            // 引数にrequestを取る場合はGetAsyncやPostAsyncでなくSendAsyncメソッドになります。
            // 戻り値はTask<HttpResponseMessage>で、変数名.ResultとするとSystem.Net.Http.HttpResponseMessageクラスが取れます。
            try
            {
                response = await httpClient.SendAsync(request);
                resBodyStr = response.Content.ReadAsStringAsync().Result;
                resStatusCoode = response.StatusCode;

                address = JsonConvert.DeserializeObject<MapApiModel.MapAddress_Local>(resBodyStr);

            }
            catch (HttpRequestException e)
            {
                // UNDONE: 通信失敗のエラー処理
                return null;
            }

            if (!resStatusCoode.Equals(HttpStatusCode.OK))
            {
                // UNDONE: レスポンスが200 OK以外の場合のエラー処理
                return null;
            }
            if (String.IsNullOrEmpty(resBodyStr))
            {
                // UNDONE: レスポンスのボディが空の場合のエラー処理
                return null;
            }
            // 中身のチェックなどを経て終了。
            return address;
        }


        /// <summary>
        /// 建物・テナント名称検索
        /// </summary>
        /// <param name="addressVal"></param>
        /// <returns></returns>
        public async Task<MapApiModel.Map_Building_Name_Local> GetBuildingNameAsync(string address_code)
        {
            String requestEndPoint = _baseUrl + "api/MapAddress/GetBuildingName?address_code=" + address_code;
            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, requestEndPoint);

            string resBodyStr;
            MapApiModel.Map_Building_Name_Local result;

            HttpStatusCode resStatusCoode = HttpStatusCode.NotFound;

            HttpResponseMessage response;

            // 通信実行。メンバ変数でhttpClientを持っているので、using(～)で囲いません。囲うと通信後にオブジェクトが破棄されます。
            // 引数にrequestを取る場合はGetAsyncやPostAsyncでなくSendAsyncメソッドになります。
            // 戻り値はTask<HttpResponseMessage>で、変数名.ResultとするとSystem.Net.Http.HttpResponseMessageクラスが取れます。
            try
            {
                response = await httpClient.SendAsync(request);
                resBodyStr = response.Content.ReadAsStringAsync().Result;
                resStatusCoode = response.StatusCode;

                result = JsonConvert.DeserializeObject<MapApiModel.Map_Building_Name_Local>(resBodyStr);

            }
            catch (HttpRequestException e)
            {
                // UNDONE: 通信失敗のエラー処理
                return null;
            }

            if (!resStatusCoode.Equals(HttpStatusCode.OK))
            {
                // UNDONE: レスポンスが200 OK以外の場合のエラー処理
                return null;
            }
            if (String.IsNullOrEmpty(resBodyStr))
            {
                // UNDONE: レスポンスのボディが空の場合のエラー処理
                return null;
            }
            // 中身のチェックなどを経て終了。
            return result;
        }

    }
}
