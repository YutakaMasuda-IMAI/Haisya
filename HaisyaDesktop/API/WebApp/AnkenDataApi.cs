using HaisyaDesktop.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static HaisyaDesktop.Models.AnkenModel;

namespace HaisyaDesktop.API.WebApp
{
    class AnkenDataApi : BaseHttpClient
    {

        public AnkenDataApi()
        {
        }

        #region 案件データ
        /// <summary>
        /// 案件データの新規登録
        /// </summary>
        /// <param name="ankenDataModelDto"></param>
        /// <returns></returns>
        public async Task<Dto.T_Anken_Local> AddNewAnkenData(AnkenDataModelDto ankenDataModelDto)
        {
            // MultipartFormDataContentのインスタンスをつくる。
            using MultipartFormDataContent multiContent = new MultipartFormDataContent();

            string jsonString = System.Text.Json.JsonSerializer.Serialize<AnkenDataModelDto>(ankenDataModelDto);
            //dynamic json = Newtonsoft.Json.JsonConvert.SerializeObject(ankenDataModelDto);

            using StringContent content = new StringContent(jsonString, Encoding.UTF8);

            // 専用の形式にしたコンテンツを、MultipartFormDataContentにAddしていく。
            multiContent.Add(content, "name");

            string url = PublicObjects.GetWebAPIHosts() + string.Format("AnkenData/AddNewAnkenData?jsonString={0}", "A");
            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, url);
            request.Content = content;

            string resBodyStr;

            Dto.T_Anken_Local result;

            HttpStatusCode resStatusCoode = HttpStatusCode.NotFound;

            HttpResponseMessage response;

            // 通信実行。メンバ変数でhttpClientを持っているので、using(～)で囲いません。囲うと通信後にオブジェクトが破棄されます。
            // 引数にrequestを取る場合はGetAsyncやPostAsyncでなくSendAsyncメソッドになります。
            // 戻り値はTask<HttpResponseMessage>で、変数名.ResultとするとSystem.Net.Http.HttpResponseMessageクラスが取れます。
            try
            {
                response = await httpClient.PostAsync(request.RequestUri, multiContent);
                resBodyStr = response.Content.ReadAsStringAsync().Result;
                resStatusCoode = response.StatusCode;

                result = JsonConvert.DeserializeObject<Dto.T_Anken_Local>(resBodyStr);

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

        /// <summary>
        /// 案件データの更新
        /// </summary>
        /// <param name="ankenDataModelDto"></param>
        /// <returns></returns>
        public async Task<UpdateAnkenDataDto> UpdateAnkenData(AnkenDataModelDto ankenDataModelDto)
        {
            // MultipartFormDataContentのインスタンスをつくる。
            using MultipartFormDataContent multiContent = new MultipartFormDataContent();

            string jsonString = System.Text.Json.JsonSerializer.Serialize<AnkenDataModelDto>(ankenDataModelDto);
            //dynamic json = Newtonsoft.Json.JsonConvert.SerializeObject(ankenDataModelDto);

            using StringContent content = new StringContent(jsonString, Encoding.UTF8);

            // 専用の形式にしたコンテンツを、MultipartFormDataContentにAddしていく。
            multiContent.Add(content, "name");

            string url = PublicObjects.GetWebAPIHosts() + string.Format("AnkenData/UpdateAnkenData?jsonString={0}", "A");
            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, url);
            request.Content = content;

            string resBodyStr;

            UpdateAnkenDataDto result;

            HttpStatusCode resStatusCoode = HttpStatusCode.NotFound;

            HttpResponseMessage response;

            // 通信実行。メンバ変数でhttpClientを持っているので、using(～)で囲いません。囲うと通信後にオブジェクトが破棄されます。
            // 引数にrequestを取る場合はGetAsyncやPostAsyncでなくSendAsyncメソッドになります。
            // 戻り値はTask<HttpResponseMessage>で、変数名.ResultとするとSystem.Net.Http.HttpResponseMessageクラスが取れます。
            try
            {
                response = await httpClient.PostAsync(request.RequestUri, multiContent);
                resBodyStr = response.Content.ReadAsStringAsync().Result;
                resStatusCoode = response.StatusCode;

                result = JsonConvert.DeserializeObject<UpdateAnkenDataDto>(resBodyStr);

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

        /// <summary>
        /// 案件データリストの取得
        /// </summary>
        /// <param name="targetDate"></param>
        /// <param name="targetDateFrom"></param>
        /// <param name="targetDateTo"></param>
        /// <param name="iCompanyID"></param>
        /// <param name="branchID"></param>
        /// <returns></returns>
        public async Task<List<Dto.V_AnkenDataList_Local>> GetAnkenDataList(string targetDate, string targetDateFrom, string targetDateTo,
                                                                            int iCompanyID, int branchID = 0)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("AnkenData/GetAnkenDataList?CcompanyID={0}&branchID={1}", iCompanyID, branchID);

            if (targetDate != null) { url += string.Format("&targetDate={0}", targetDate.Replace("/", "-")); }
            if (targetDateFrom != null) { url += string.Format("&targetDateFrom={0}", targetDateFrom.Replace("/", "-")); }
            if (targetDateTo != null) { url += string.Format("&targetDateTo={0}", targetDateTo.Replace("/", "-")); }

            return await GetHttpData<List<Dto.V_AnkenDataList_Local>>(url);
        }

        /// <summary>
        /// 案件データの取得
        /// </summary>
        /// <param name="AnkenId"></param>
        /// <returns></returns>
        public async Task<AnkenDataModelDto> GetAnkenData(int AnkenId)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("AnkenData/GetAnkenData?AnkenId={0}", AnkenId);
            return await GetHttpData<AnkenDataModelDto>(url);
        }

        #endregion 案件データ

        #region ゼンリンAPI
        /// <summary>
        /// 【ゼンリンAPI】ドライブルートリストの取得
        /// </summary>
        /// <param name="eria">標準運賃計算エリア</param>
        /// <param name="iCompanyID">会社ID</param>
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
        public async Task<DriveRouteListDto_Local> GetDriveRouteListExAsync(
                                                        string eria,
                                                        int iCompanyID,
                                                        string from, string to,
                                                        string mpoint = "",
                                                        string syasyu = null,
                                                        string kata = null,
                                                        double height = 0,
                                                        double width = 0,
                                                        double weight = 0,
                                                        double nenpi = 0,
                                                        string mpointstype = "",
                                                        string syasyuSize = null,
                                                        int searchparam = 1,
                                                        string departuretime = null,
                                                        string cardetailinfo = "B",
                                                        string tolltype = "large",
                                                        string smartic = "T",
                                                        string regulation = "season,time",
                                                        string twouturn = "F",
                                                        string ferry = "F",
                                                        string TsumiTime = "00:00",
                                                        string OroshiTime = "00:00",
                                                        List<Dto.AnkenExchargeDto_Local> t_Anken_Excharge_s = null)
        {

            string url = PublicObjects.GetWebAPIHosts() + string.Format("Anken/GetDriveRouteListEx?CompanyID={0}", iCompanyID);
            url += string.Format("&eria={0}", eria);
            url += string.Format("&from={0}", from);
            url += string.Format("&to={0}", to);
            if (mpoint.Length > 0) { url += string.Format("&mpoint={0}", mpoint); }
            if (syasyu != null) { url += string.Format("&syasyu={0}", syasyu); }
            if (kata != null) { url += string.Format("&kata={0}", kata); }
            url += string.Format("&height={0}", height);
            url += string.Format("&width={0}", width);
            url += string.Format("&weight={0}", weight);
            url += string.Format("&nenpi={0}", nenpi);
            if (mpointstype.Length > 0) { url += string.Format("&mpointstype={0}", mpointstype); }
            if (syasyuSize != null) { url += string.Format("&syasyuSize={0}", syasyuSize); }
            url += string.Format("&searchparam={0}", searchparam);
            if (departuretime != null) { url += string.Format("&departuretime={0}", departuretime); }
            url += string.Format("&cardetailinfo={0}", cardetailinfo);
            url += string.Format("&tolltype={0}", tolltype);
            url += string.Format("&smartic={0}", smartic);
            url += string.Format("&regulation={0}", regulation);
            url += string.Format("&twouturn={0}", twouturn);
            url += string.Format("&ferry={0}", ferry);
            url += string.Format("&TsumiTime={0}", TsumiTime);
            url += string.Format("&OroshiTime={0}", OroshiTime);

            // MultipartFormDataContentのインスタンスをつくる。
            using MultipartFormDataContent multiContent = new MultipartFormDataContent();

            string jsonString = System.Text.Json.JsonSerializer.Serialize<List<Dto.AnkenExchargeDto_Local>>(t_Anken_Excharge_s);
            using StringContent content = new StringContent(jsonString, Encoding.UTF8);

            // 専用の形式にしたコンテンツを、MultipartFormDataContentにAddしていく。
            multiContent.Add(content, "AnkenExchargeList");

            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, url);
            request.Content = content;

            string resBodyStr;

            DriveRouteListDto_Local result;

            HttpStatusCode resStatusCoode = HttpStatusCode.NotFound;

            HttpResponseMessage response;

            // 通信実行。メンバ変数でhttpClientを持っているので、using(～)で囲いません。囲うと通信後にオブジェクトが破棄されます。
            // 引数にrequestを取る場合はGetAsyncやPostAsyncでなくSendAsyncメソッドになります。
            // 戻り値はTask<HttpResponseMessage>で、変数名.ResultとするとSystem.Net.Http.HttpResponseMessageクラスが取れます。
            try
            {
                response = await httpClient.PostAsync(request.RequestUri, multiContent);
                resBodyStr = response.Content.ReadAsStringAsync().Result;
                resStatusCoode = response.StatusCode;

                result = JsonConvert.DeserializeObject<DriveRouteListDto_Local>(resBodyStr);

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


        /// <summary>
        /// ドライブルート詳細の取得
        /// </summary>
        /// <param name="routeID"></param>
        /// <param name="iCompanyID"></param>
        /// <param name="syasyu"></param>
        /// <param name="kata"></param>
        /// <param name="syasyuSize"></param>
        /// <param name="TsumiTime"></param>
        /// <param name="OroshiTime"></param>
        /// <param name="searchparam"></param>
        /// <param name="datum"></param>
        /// <param name="llunit"></param>
        /// <returns></returns>
        public async Task<DriveRouteListDto_Local> GetDriveDetailAsync(
                                                                        int iCompanyID,
                                                                        string routeID,
                                                                        string routeType,
                                                                        string syasyu,
                                                                        string kata,
                                                                        string syasyuSize,
                                                                        string TsumiTime = "00:00",
                                                                        string OroshiTime = "00:00",
                                                                        double searchparam = 1,
                                                                        string datum = "JGD",
                                                                        string llunit = "dec"
                                                                        )
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("Anken/GetDriveDetail?CompanyID={0}", iCompanyID);
            url += string.Format("&routeID={0}", routeID);
            url += string.Format("&routeType={0}", routeType);
            url += string.Format("&syasyu={0}", syasyu);
            url += string.Format("&kata={0}", kata);
            url += string.Format("&syasyuSize={0}", syasyuSize);
            url += string.Format("&searchparam={0}", searchparam);
            if (datum != null) { url += string.Format("&datum={0}", datum); }
            if (llunit != null) { url += string.Format("&llunit={0}", llunit); }
            url += string.Format("&TsumiTime={0}", TsumiTime);
            url += string.Format("&OroshiTime={0}", OroshiTime);

            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, url);

            string resBodyStr;

            DriveRouteListDto_Local resultList;

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

                resultList = JsonConvert.DeserializeObject<DriveRouteListDto_Local>(resBodyStr);

            }
            catch (HttpRequestException e)
            {
                // UNDONE: 通信失敗のエラー処理
                return null;
            }

            if (!resStatusCoode.Equals(HttpStatusCode.OK))
            {
                // UNDONE: レスポンスが200 OK以外の場合のエラー処理
                return resultList;
            }
            if (String.IsNullOrEmpty(resBodyStr))
            {
                // UNDONE: レスポンスのボディが空の場合のエラー処理
                return null;
            }
            // 中身のチェックなどを経て終了。
            return resultList;


        }

        #endregion ゼンリンAPI

        #region T_Point
        /// <summary>
        /// T_Point
        /// </summary>
        /// <param name="AreaList"></param>
        /// <returns></returns>
        public async Task<Dto.T_Point_Local> T_PointData(string zip, int userId, int groupId)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("AnkenData/T_PointData?zip={0}", zip);
            url += string.Format("&userId={0}", userId);
            url += string.Format("&groupId={0}", groupId);

            //データ取得
            return await GetHttpData<Dto.T_Point_Local>(url);

        }

        /// <summary>
        /// UserIdを元にT_Pointのリストを抽出いて返却
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task<List<Dto.T_Point_Local>> T_PointListFromUserId(int userId, int groupId)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("AnkenData/T_PointListFromUserId?dummy=1");
            url += string.Format("&userId={0}", userId);
            url += string.Format("&groupId={0}", groupId);

            //データ取得
            return await GetHttpData<List<Dto.T_Point_Local>>(url);

        }

        /// <summary>
        /// T_Pointへのデータ更新
        /// </summary>
        /// <param name="t_Point"></param>
        /// <returns></returns>
        public async Task<Dto.MsterDataCommonResultValDto_Local> InsertUpdatePointData(Dto.T_Point_Local t_Point)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("AnkenData/InsertUpdatePointData?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<Dto.T_Point_Local>(t_Point, url);
        }
        #endregion T_Point_Local


        #region M_Customer
        /// <summary>
        /// M_Customerリストの指定ユーザーの
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="UserID"></param>
        /// <param name="Top"></param>
        /// <returns></returns>
        public async Task<List<Dto.M_Customer_Local>> GetCustomerListForUser(int CompanyID, int UserID, int Top)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("Anken/GetCustomerListForUser?CompanyID={0}", CompanyID.ToString());
            url += string.Format("&UserID={0}", UserID);
            url += string.Format("&Top={0}", Top);
            return await GetHttpData<List<Dto.M_Customer_Local>>(url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="UserID"></param>
        /// <param name="Top"></param>
        /// <returns></returns>
        public async Task<List<Dto.M_Customer_Local>> GetCustomerListToMostRecentForUser(int CompanyID, int UserID, int Top)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("Anken/GetCustomerListToMostRecentForUser?CompanyID={0}", CompanyID.ToString());
            url += string.Format("&UserID={0}", UserID);
            url += string.Format("&Top={0}", Top);
            return await GetHttpData<List<Dto.M_Customer_Local>>(url);
        }
        #endregion M_Customer
    }
}
