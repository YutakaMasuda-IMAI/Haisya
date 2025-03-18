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
    internal class ConvertCrsApi : BaseHttpClient
    {

        public ConvertCrsApi()
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        public async Task<Latlon> GetConvert_CrsForJpnToWorld(string lat, string lng)
        {

            String requestEndPoint = PublicObjects.GetWebAPIHosts() + "api/MapConvertCrs/GetJpnToWorld?";
            requestEndPoint += string.Format("lat={0}&lng={1}", lat, lng);
            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, requestEndPoint);

            string resBodyStr;
            Latlon result = null;

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

                MapApiModel.GenericResult val = JsonConvert.DeserializeObject<MapApiModel.GenericResult>(resBodyStr);

                result = val.Latlon;

                Console.WriteLine(val);


            }
            catch (HttpRequestException e)
            {
                // UNDONE: 通信失敗のエラー処理
                return null;
            }

            if (!resStatusCoode.Equals(HttpStatusCode.OK))
            {
                // UNDONE: レスポンスが200 OK以外の場合のエラー処理
                return result;
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
