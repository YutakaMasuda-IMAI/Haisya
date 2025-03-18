using HaisyaWeb.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HaisyaWeb.API.Map
{
    internal class DriveRouteListApi : BaseHttpClient
    {

        public DriveRouteListApi(MapApiSettings mapApiSetting)
        {
            _baseUrl = mapApiSetting.Api.WebAPIHosts;
        }

        /// <summary>
        /// 情報がURLに載ったGETリクエストを送受信するサンプル。
        /// </summary>
        /// <param name="from">出発地点</param>
        /// <param name="to">到着地点</param>
        /// <param name="mpoint">経由地点</param>
        /// <param name="searchparam">検索挙動変更</param>
        /// <param name="height">車高</param>
        /// <param name="width">車幅</param>
        /// <param name="weight">車重</param>
        /// <param name="departuretime">出発時刻指定</param>
        /// <param name="cardetailinfo">詳細車種</param>
        /// <param name="tolltype">料金車種</param>
        /// <param name="smartic">スマートIC利用指定</param>
        /// <param name="regulation">規制考慮</param>
        /// <param name="twouturn">2段階Uターン回避指定</param>
        /// <param name="ferry">フェリー考慮指定</param>
        /// <returns>正常：レスポンスのボディ / 異常：null</returns>
        public async Task<MapApiModel.DriveList_Local> GetDriveRouteListAsync(string from, string to,
                                                        string mpoint = null,
                                                        int searchparam = 1,
                                                        double height = 0, double width = 0, double weight = 0, string mpointstype = "",
                                                        string departuretime = null, string cardetailinfo = "B", string tolltype = "large",
                                                        string smartic = "T", string regulation = "season,time", string twouturn = "F", string ferry = "F")
        {




            String requestEndPoint = _baseUrl + "api/MapDriveRouteList?from=" + from + "&to=" + to + "&mpoint=" + mpoint + "&searchparam=" + searchparam + "&height=" + height +
                "&width=" + width + "&weight=" + weight + "&departuretime=" + departuretime + "&cardetailinfo=" + cardetailinfo + "&tolltype=" + tolltype + "&mpointstype=" + mpointstype +
                "&smartic=" + smartic + "&regulation=" + regulation + "&twouturn=" + twouturn + "&ferry=" + ferry;
            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, requestEndPoint);

            string resBodyStr;
            MapApiModel.DriveList_Local driveList;

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

                driveList = JsonConvert.DeserializeObject<MapApiModel.DriveList_Local>(resBodyStr);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
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
            return driveList;
        }



        /// <summary>
        /// 情報がURLに載ったGETリクエストを送受信するサンプル。
        /// </summary>
        /// <param name="from">出発地点</param>
        /// <param name="to">到着地点</param>
        /// <param name="mpoint">経由地点</param>
        /// <param name="searchparam">検索挙動変更</param>
        /// <param name="height">車種</param>
        /// <param name="width">型</param>
        /// <param name="departuretime">出発時刻指定</param>
        /// <param name="cardetailinfo">詳細車種</param>
        /// <param name="tolltype">料金車種</param>
        /// <param name="smartic">スマートIC利用指定</param>
        /// <param name="regulation">規制考慮</param>
        /// <param name="twouturn">2段階Uターン回避指定</param>
        /// <param name="ferry">フェリー考慮指定</param>
        /// <returns>正常：レスポンスのボディ / 異常：null</returns>
        public async Task<MapApiModel.DriveListEx_Local> GetDriveRouteListExAsync(string from, string to,
                                                        string mpoint = null,
                                                        string syasyu = null,
                                                        string kata = null,
                                                        double height = 0,
                                                        double width = 0,
                                                        double weight = 0,
                                                        double nenpi = 0,
                                                        string mpointstype = "",
                                                        int searchparam = 1,
                                                        string departuretime = null, string cardetailinfo = "B", string tolltype = "large",
                                                        string smartic = "T", string regulation = "season,time", string twouturn = "F", string ferry = "F")
        {




            String requestEndPoint = _baseUrl + "api/MapDriveRouteList?from=" + from + "&to=" + to + "&mpoint=" + mpoint + "&searchparam=" + searchparam + "&syasyu=" + syasyu +
                "&kata=" + kata + "&departuretime=" + departuretime + "&cardetailinfo=" + cardetailinfo + "&tolltype=" + tolltype +
                "&height=" + height + "&width=" + width + "&weight=" + weight + "&nenpi=" + nenpi + "&mpointstype=" + mpointstype +
                "&smartic=" + smartic + "&regulation=" + regulation + "&twouturn=" + twouturn + "&ferry=" + ferry;
            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, requestEndPoint);

            string resBodyStr;
            MapApiModel.DriveListEx_Local driveList = null;

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

                if (resStatusCoode.Equals(HttpStatusCode.OK)) { 
                    driveList = JsonConvert.DeserializeObject<MapApiModel.DriveListEx_Local>(resBodyStr);
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
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
            return driveList;
        }


    }
}
