using HaisyaWeb.Dto;
using HaisyaWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HaisyaWeb.API.WebApp
{
    class SeikyuDataApi : BaseHttpClient
    {
        public SeikyuDataApi(MapApiSettings mapApiSetting) => _baseUrl = mapApiSetting.Api.WebAPIHosts;

        /// <summary>
        /// 請求データの返却
        /// </summary>
        /// <param name="iCompanyIDß"></param>
        /// <param name="seikyuNengetsu"></param>
        /// <param name="tokuisakiID"></param>
        /// <param name="tokuisakiIDTo"></param>
        /// <returns></returns>
        public async Task<List<V_SeikyuCheckDataList_Local>> GetSeikyuCheckDataList(int iCompanyID, string seikyuNengetsu, string tokuisakiID, string tokuisakiIDTo)
        {
            string url = _baseUrl + string.Format("SeikyuData/GetSeikyuCheckDataList?iCompanyID={0}", iCompanyID);
            if (seikyuNengetsu != null) { url += string.Format("&seikyuNengetsu={0}", seikyuNengetsu.Replace("/", "-")); }
            if (tokuisakiID != null) { url += string.Format("&tokuisakiID={0}", tokuisakiID); }
            if (tokuisakiIDTo != null) { url += string.Format("&tokuisakiIDTo={0}", tokuisakiIDTo); }
            return await GetHttpData<List<V_SeikyuCheckDataList_Local>>(url);
        }

        /// <summary>
        /// 集計データの返却
        /// </summary>
        /// <param name="Check_Seikyu_ID"></param>
        /// <returns></returns>
        public async Task<T_Print_Seikyu_Local> GetSyuukeiData(int? Check_Seikyu_ID)
        {
            string url = _baseUrl + string.Format("SeikyuData/GetT_Print_Seikyu?Check_Seikyu_ID={0}", Check_Seikyu_ID);
            return await GetHttpData<T_Print_Seikyu_Local>(url);
        }

        /// <summary>
        /// 入金情報の返却
        /// </summary>
        /// <param name="CheckSeikyu"></param>
        /// <returns></returns>
        public async Task<List<T_Nyukin_Local>> GetNyuukinDataList(T_Check_Seikyu_Local CheckSeikyu)
        {
            string url = _baseUrl + string.Format("SeikyuData/GetT_Nyukin?Customer_Branch_ID={0}&Seikyu_Month={1}&Shime_Day={2}",
                CheckSeikyu.Customer_Branch_ID,
                CheckSeikyu.Seikyu_Month,
                CheckSeikyu.Shime_Day);

            return await GetHttpData<List<T_Nyukin_Local>>(url);
        }

        /// <summary>
        /// 入金情報の返却
        /// </summary>
        /// <param name="seikyuId">id of seikyu</param>
        /// <param name="seikyuMonth">year and month of seikyu</param>
        /// <param name="shimeDay">締日</param>
        /// <returns>list data nyukin</returns>
        public async Task<List<T_Nyukin_Local>> GetNyuukinDataListBySeikyuID(int seikyuId, DateOnly seikyuMonth, int shimeDay)
        {
            string url = _baseUrl + string.Format("SeikyuData/GetT_NyukinBySeikyuId?Seikyu_ID={0}&Seikyu_Month={1}&Shime_Day={2}",
                seikyuId,
                seikyuMonth,
                shimeDay);

            return await GetHttpData<List<T_Nyukin_Local>>(url);
        }

        /// <summary>
        /// 集計データの返却
        /// </summary>
        /// <param name="Print_Seikyu_ID"></param>
        /// <returns></returns>
        public async Task<List<T_Print_Seikyu_Detail_Local>> GetT_PrintSeikyuDetailList(int Print_Seikyu_ID)
        {
            string url = _baseUrl + string.Format("SeikyuData/GetT_Print_Seikyu_Detail?Print_Seikyu_ID={0}", Print_Seikyu_ID);
            return await GetHttpData<List<T_Print_Seikyu_Detail_Local>>(url);
        }

        /// <summary>
        /// 集計データの返却
        /// </summary>
        /// <param name="Check_Seikyu_ID"></param>
        /// <returns></returns>
        public async Task<T_Print_Seikyu_Local> GetT_PrintSeikyu(int? Check_Seikyu_ID)
        {
            string url = _baseUrl + string.Format("SeikyuData/GetT_Print_Seikyu?Check_Seikyu_ID={0}", Check_Seikyu_ID);
            return await GetHttpData<T_Print_Seikyu_Local>(url);
        }

        /// <summary>
        /// 集計データの返却
        /// </summary>
        /// <param name="Seikyu_ID">id of seikyu</param>
        /// <returns></returns>
        public async Task<T_Print_Seikyu_Local> GetT_PrintSeikyuBySeikyuId(int? Seikyu_ID)
        {
            string url = _baseUrl + string.Format("SeikyuData/GetT_Print_SeikyuBySeikyuId?Seikyu_ID={0}", Seikyu_ID);
            return await GetHttpData<T_Print_Seikyu_Local>(url);
        }

        /// <summary>
        /// 集計データの返却
        /// </summary>
        /// <param name="Check_Seikyu_ID"></param>
        /// <returns></returns>
        public async Task<List<T_Check_Seikyu_Change_Local>> GetT_Check_Seikyu_ChangeList(int? Check_Seikyu_ID)
        {
            string url = _baseUrl + string.Format("SeikyuData/GetT_Check_Seikyu_Change?Check_Seikyu_ID={0}", Check_Seikyu_ID);
            return await GetHttpData<List<T_Check_Seikyu_Change_Local>>(url);
        }

        /// <summary>
        /// T_Check_Seikyu_Changeデータの返却
        /// </summary>
        /// <param name="Check_Seikyu_ID"></param>
        /// <param name="Uriage_Unchin_ID"></param>
        /// <returns></returns>
        public async Task<T_Check_Seikyu_Change_Local> GetT_Check_Seikyu_Change(int? Check_Seikyu_ID, int Uriage_Unchin_ID)
        {
            string url = _baseUrl + string.Format("SeikyuData/GetT_Check_Seikyu_ChangeData?Check_Seikyu_ID={0}&Uriage_Unchin_ID={1}", Check_Seikyu_ID, Uriage_Unchin_ID);
            return await GetHttpData<T_Check_Seikyu_Change_Local>(url);
        }

        /// <summary>
        /// T_Check_Seikyu_Detailデータの返却
        /// </summary>
        /// <param name="Check_Seikyu_ID"></param>
        /// <returns></returns>
        public async Task<T_Check_Seikyu_Detail_Local> GetT_Check_Seikyu_Detail(int? Check_Seikyu_ID)
        {
            string url = _baseUrl + string.Format("SeikyuData/GetT_Check_Seikyu_Detail?Check_Seikyu_ID={0}", Check_Seikyu_ID);
            return await GetHttpData<T_Check_Seikyu_Detail_Local>(url);
        }

        /// <summary>
        /// T_Check_Seikyu_Detailデータの返却
        /// </summary>
        /// <param name="Check_Seikyu_ID"></param>
        /// <returns></returns>
        public async Task<List<Dto.T_Check_Seikyu_Detail_Local>> GetT_Check_Seikyu_Detail_All(int? Check_Seikyu_ID)
        {
            string url = _baseUrl + string.Format("SeikyuData/GetT_Check_Seikyu_Detail_All?Check_Seikyu_ID={0}", Check_Seikyu_ID);
            return await GetHttpData<List<Dto.T_Check_Seikyu_Detail_Local>>(url);
        }

        /// <summary>
        /// T_Check_Seikyuデータの返却
        /// </summary>
        /// <param name="Check_Seikyu_ID"></param>
        /// <returns></returns>
        public async Task<T_Check_Seikyu_Local> GetT_Check_Seikyu(int? Check_Seikyu_ID)
        {
            string url = _baseUrl + string.Format("SeikyuData/GetT_Check_Seikyu?Check_Seikyu_ID={0}", Check_Seikyu_ID);
            return await GetHttpData<T_Check_Seikyu_Local>(url);
        }

        /// <summary>
        /// T_Check_Seikyu_Doneデータの返却
        /// </summary>
        /// <param name="Check_Seikyu_ID"></param>
        /// <returns></returns>
        public async Task<T_Check_Seikyu_Done_Local> GetT_Check_Seikyu_Done(int? Check_Seikyu_ID)
        {
            string url = _baseUrl + string.Format("SeikyuData/GetT_Check_Seikyu_Done?Check_Seikyu_ID={0}", Check_Seikyu_ID);
            return await GetHttpData<T_Check_Seikyu_Done_Local>(url);
        }

        /// <summary>
        /// T_Uriage_Unchinデータの返却
        /// </summary>
        /// <param name="Uriage_Unchin_ID"></param>
        /// <returns></returns>
        public async Task<Dto.T_Uriage_Unchin_Local> GetT_Uriage_Unchin(int Uriage_Unchin_ID)
        {
            string url = _baseUrl + string.Format("SeikyuData/GetT_Uriage_Unchin?Uriage_Unchin_ID={0}", Uriage_Unchin_ID);
            return await GetHttpData<T_Uriage_Unchin_Local>(url);
        }

        /// <summary>
        /// 指定されたIDに基づいてT_Anken_Detailの取得を取得
        /// </summary>
        /// <param name="uriageID">売上ID</param>
        /// <returns>T_Anken_Detail_Local</returns>
        public async Task<T_Anken_Detail_Local> GetTAnkenDetailByUriageID(int uriageID)
        {
            string url = _baseUrl + string.Format("SeikyuData/GetTAnkenDetailByUriageID?uriageID={0}", uriageID);
            //データ取得
            return await GetHttpData<T_Anken_Detail_Local>(url);
        }

        /// <summary>
        /// 指定されたIDに基づいてM_SyaryoManagementの取得を取得
        /// </summary>
        /// <param name="uriageID">売上ID</param>
        /// <returns>M_SyaryoManagement_Local</returns>
        public async Task<M_SyaryoManagement_Local> GetMSyaryoManagementByUriageID(int uriageID)
        {
            string url = _baseUrl + string.Format("SeikyuData/GetMSyaryoManagementByUriageID?uriageID={0}", uriageID);
            //データ取得
            return await GetHttpData<M_SyaryoManagement_Local>(url);
        }

        /// <summary>
        /// 指定されたIDに基づいてM_Syaryoの取得を取得
        /// </summary>
        /// <param name="uriageID">売上ID</param>
        /// <returns>M_Syaryo_Local</returns>
        public async Task<M_Syaryo_Local> GetMSyaryoByUriageID(int uriageID)
        {
            string url = _baseUrl + string.Format("SeikyuData/GetMSyaryoByUriageID?uriageID={0}", uriageID);
            //データ取得
            return await GetHttpData<Dto.M_Syaryo_Local>(url);
        }

        /// <summary>
        /// M_Customer_Branchデータの返却
        /// </summary>
        /// <param name="Customer_Branch_ID"></param>
        /// <returns></returns>
        public async Task<Dto.M_Customer_Branch_Local> GetM_Customer_Branch(int Customer_Branch_ID)
        {
            string url = _baseUrl + string.Format("SeikyuData/GetM_Customer_Branch?Customer_Branch_ID={0}", Customer_Branch_ID);
            return await GetHttpData<Dto.M_Customer_Branch_Local>(url);
        }

        /// <summary>
        /// T_Check_Seikyu_Changeの更新
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> PostBatchRegistration(BatchRegistrationModel data)
        {
            string url = _baseUrl + string.Format("SeikyuData/PostBatchRegistration?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<Dto.BatchRegistrationModel>(data, url);
        }

        /// <summary>
        /// T_Check_Seikyu_Change・T_Check_Seikyu_Detail・T_Uriage_Unchinの更新
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> PostModalApproval(ModalApprovalModel data)
        {
            string url = _baseUrl + string.Format("SeikyuData/PostModalApproval?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<Dto.ModalApprovalModel>(data, url);
        }

        /// <summary>
        /// T_Uriage・T_Check_Seikyu_Detail・T_Check_Seikyuの更新
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> UpdateApprovalStatus(ApprovalStatusModel data)
        {
            string url = _baseUrl + string.Format("SeikyuData/UpdateApprovalStatus?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<Dto.ApprovalStatusModel>(data, url);
        }

        /// <summary>
        /// T_Check_Seikyu_Changeの新規登録、更新
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<T_Check_Seikyu_Change_Local> InsertUpdateCheckSeikyuChange(T_Check_Seikyu_Change_Local data)
        {
            // MultipartFormDataContentのインスタンスをつくる。
            using MultipartFormDataContent multiContent = new();

            string jsonString = System.Text.Json.JsonSerializer.Serialize(data);

            using StringContent content = new(jsonString, Encoding.UTF8);

            // 専用の形式にしたコンテンツを、MultipartFormDataContentにAddしていく。
            multiContent.Add(content, "name");

            string url = _baseUrl + string.Format("SeikyuData/InsertUpdateT_Check_Seikyu_Change?jsonString={0}", "A");
            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, url);
            request.Content = content;

            string resBodyStr;

            T_Check_Seikyu_Change_Local result;

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

                if (resStatusCoode == HttpStatusCode.InternalServerError) throw new HttpRequestException(resBodyStr);
                if (resStatusCoode == HttpStatusCode.BadRequest) throw new HttpRequestException(resBodyStr);

                result = JsonConvert.DeserializeObject<T_Check_Seikyu_Change_Local>(resBodyStr);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                // UNDONE: 通信失敗のエラー処理
                throw;
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

        /// <summary>
        /// T_Check_Seikyu_Detailの更新
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<T_Check_Seikyu_Detail_Local> UpdateCheckSeikyuDetail(T_Check_Seikyu_Detail_Local data)
        {
            // MultipartFormDataContentのインスタンスをつくる。
            using MultipartFormDataContent multiContent = new();

            string jsonString = System.Text.Json.JsonSerializer.Serialize(data);

            using StringContent content = new(jsonString, Encoding.UTF8);

            // 専用の形式にしたコンテンツを、MultipartFormDataContentにAddしていく。
            multiContent.Add(content, "name");

            string url = _baseUrl + string.Format("SeikyuData/UpdateT_Check_Seikyu_Detail?jsonString={0}", "A");
            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, url);
            request.Content = content;

            string resBodyStr;

            T_Check_Seikyu_Detail_Local result;

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

                if (resStatusCoode == HttpStatusCode.InternalServerError) throw new HttpRequestException(resBodyStr);
                if (resStatusCoode == HttpStatusCode.BadRequest) throw new HttpRequestException(resBodyStr);

                result = JsonConvert.DeserializeObject<T_Check_Seikyu_Detail_Local>(resBodyStr);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                // UNDONE: 通信失敗のエラー処理
                throw;
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

        /// <summary>
        /// T_Check_Seikyuの更新
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<T_Check_Seikyu_Local> UpdateCheckSeikyu(T_Check_Seikyu_Local data)
        {
            // MultipartFormDataContentのインスタンスをつくる。
            using MultipartFormDataContent multiContent = new();

            string jsonString = System.Text.Json.JsonSerializer.Serialize(data);

            using StringContent content = new(jsonString, Encoding.UTF8);

            // 専用の形式にしたコンテンツを、MultipartFormDataContentにAddしていく。
            multiContent.Add(content, "name");

            string url = _baseUrl + string.Format("SeikyuData/UpdateT_Check_Seikyu?jsonString={0}", "A");
            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, url);
            request.Content = content;

            string resBodyStr;

            T_Check_Seikyu_Local result;

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

                if (resStatusCoode == HttpStatusCode.InternalServerError) throw new HttpRequestException(resBodyStr);
                if (resStatusCoode == HttpStatusCode.BadRequest) throw new HttpRequestException(resBodyStr);

                result = JsonConvert.DeserializeObject<T_Check_Seikyu_Local>(resBodyStr);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                // UNDONE: 通信失敗のエラー処理
                throw;
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

        /// <summary>
        /// T_Uriageの更新
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<T_Uriage_Local> UpdateUriage(T_Uriage_Local data)
        {
            // MultipartFormDataContentのインスタンスをつくる。
            using MultipartFormDataContent multiContent = new();

            string jsonString = System.Text.Json.JsonSerializer.Serialize(data);

            using StringContent content = new(jsonString, Encoding.UTF8);

            // 専用の形式にしたコンテンツを、MultipartFormDataContentにAddしていく。
            multiContent.Add(content, "name");

            string url = _baseUrl + string.Format("SeikyuData/UpdateT_Uriage?jsonString={0}", "A");
            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, url);
            request.Content = content;

            string resBodyStr;

            Dto.T_Uriage_Local result;

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

                if (resStatusCoode == HttpStatusCode.InternalServerError) throw new HttpRequestException(resBodyStr);
                if (resStatusCoode == HttpStatusCode.BadRequest) throw new HttpRequestException(resBodyStr);

                result = JsonConvert.DeserializeObject<Dto.T_Uriage_Local>(resBodyStr);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                // UNDONE: 通信失敗のエラー処理
                throw;
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

        /// <summary>
        /// 請求データの返却
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="seikyuNengetsu"></param>
        /// <param name="targetTokuisaki"></param>
        /// <param name="toTokuisaki"></param>
        /// <param name="SelectSeikyuTantou"></param>
        /// <param name="shimeDay"></param>
        /// <returns></returns>
        public async Task<List<Dto.V_SeikyuDataList_Local>> GetSeikyuDataList(int iCompanyID, string seikyuNengetsu, string targetTokuisaki, string toTokuisaki, string SelectSeikyuTantou, string shimeDay)
        {
            string url = _baseUrl + string.Format("SeikyuData/GetSeikyuDataList?iCompanyID={0}", iCompanyID);
            if (!String.IsNullOrEmpty(seikyuNengetsu)) { url += string.Format("&seikyuNengetsu={0}", seikyuNengetsu.Replace("/", "-")); }
            if (!String.IsNullOrEmpty(targetTokuisaki)) { url += string.Format("&targetTokuisaki={0}", targetTokuisaki); }
            if (!String.IsNullOrEmpty(toTokuisaki)) { url += string.Format("&toTokuisaki={0}", toTokuisaki); }
            if (!String.IsNullOrEmpty(SelectSeikyuTantou)) { url += string.Format("&selectSeikyuTantou={0}", SelectSeikyuTantou); }
            if (!String.IsNullOrEmpty(shimeDay)) { url += string.Format("&shimeDay={0}", shimeDay); }
            return await GetHttpData<List<Dto.V_SeikyuDataList_Local>>(url);
        }

        /// <summary>
        /// 請求済みデータの返却
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="seikyuNengetsu"></param>
        /// <param name="targetTokuisaki"></param>
        /// <param name="toTokuisaki"></param>
        /// <param name="tokuisakiName"></param>
        /// <param name="shimeDay"></param>
        /// <returns></returns>
        public async Task<List<Dto.V_SeikyuZumiDataList_Local>> GetSeikyuZumiDataList(int iCompanyID, string seikyuNengetsu, string targetTokuisaki, string toTokuisaki, string tokuisakiName, string shimeDay)
        {
            string url = _baseUrl + string.Format("SeikyuData/GetSeikyuZumiDataList?iCompanyID={0}", iCompanyID);
            if (!String.IsNullOrEmpty(seikyuNengetsu)) { url += string.Format("&seikyuNengetsu={0}", seikyuNengetsu.Replace("/", "-")); }
            if (!String.IsNullOrEmpty(targetTokuisaki)) { url += string.Format("&targetTokuisaki={0}", targetTokuisaki); }
            if (!String.IsNullOrEmpty(toTokuisaki)) { url += string.Format("&toTokuisaki={0}", toTokuisaki); }
            if (!String.IsNullOrEmpty(tokuisakiName)) { url += string.Format("&tokuisakiName={0}", tokuisakiName); }
            if (!String.IsNullOrEmpty(shimeDay)) { url += string.Format("&shimeDay={0}", shimeDay); }
            return await GetHttpData<List<Dto.V_SeikyuZumiDataList_Local>>(url);
        }
    }
}
