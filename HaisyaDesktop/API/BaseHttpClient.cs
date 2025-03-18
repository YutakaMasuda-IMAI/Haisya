using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HaisyaDesktop.API
{
    class BaseHttpClient : IDisposable
    {


        //// <summary>
        ///// 通信先のベースURL
        ///// </summary>
        //public string _baseUrl { set; get; }

        //// <summary>
        ///// 通信先のベースURL
        ///// </summary>
        //public string _baseMapUrl { set; get; }

        /// <summary>
        /// C#側のHttpクライアント
        /// </summary>
        protected readonly HttpClient httpClient;



        public BaseHttpClient()
        {
            

            // 通信するメソッドでその都度HttpClientをnewすると毎回ソケットを開いてリソースを消費するため、
            // メンバ変数で使い回す手法を取っています。
            this.httpClient = new HttpClient();
        }


        /// <summary>
        /// HTTPリクエストメッセージを生成する内部メソッドです。
        /// </summary>
        /// <param name="httpMethod">HTTPメソッドのオブジェクト</param>
        /// <param name="requestEndPoint">通信先のURL</param>
        /// <returns>HttpRequestMessage</returns>
        public HttpRequestMessage CreateRequest(HttpMethod httpMethod, string requestEndPoint)
        {
            var request = new HttpRequestMessage(httpMethod, requestEndPoint);
            return this.AddHeaders(request);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// HTTPリクエストにヘッダーを追加する内部メソッドです。
        /// </summary>
        /// <param name="request">リクエスト</param>
        /// <returns>HttpRequestMessage</returns>
        private HttpRequestMessage AddHeaders(HttpRequestMessage request)
        {
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Accept-Charset", "utf-8");
            // 同じようにして、例えば認証通過後のトークンが "Authorization: Bearer {トークンの文字列}"
            // のように必要なら適宜追加していきます。
            return request;
        }

        /// <summary>
        /// Http経由での汎用データ取得処理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<T> GetHttpData<T>(string url)
        {
            HttpRequestMessage request = CreateRequest(HttpMethod.Get, url);

            string resBodyStr;

            T result;

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

                if (!resStatusCoode.Equals(HttpStatusCode.OK))
                {
                    // UNDONE: レスポンスが200 OK以外の場合のエラー処理
                    return default;
                }
                if (String.IsNullOrEmpty(resBodyStr))
                {
                    // UNDONE: レスポンスのボディが空の場合のエラー処理
                    return default;
                }

                result = JsonConvert.DeserializeObject<T>(resBodyStr);

            }
            catch (HttpRequestException e)
            {
                // UNDONE: 通信失敗のエラー処理
                return default;
            }

            return (T)result;
        }

        /// <summary>
        /// Http経由での汎用データ更新処理
        /// </summary>
        /// <param name="t_Point"></param>
        /// <returns></returns>
        public async Task<Dto.MsterDataCommonResultValDto_Local> ExecHttpData<T>(T param, string url)
        {
            // MultipartFormDataContentのインスタンスをつくる。
            using MultipartFormDataContent multiContent = new MultipartFormDataContent();

            string jsonString = System.Text.Json.JsonSerializer.Serialize<T>(param);
            using StringContent content = new StringContent(jsonString, Encoding.UTF8);

            // 専用の形式にしたコンテンツを、MultipartFormDataContentにAddしていく。
            multiContent.Add(content, "name");

            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, url);
            request.Content = content;

            string resBodyStr;

            Dto.MsterDataCommonResultValDto_Local result;

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

                result = JsonConvert.DeserializeObject<Dto.MsterDataCommonResultValDto_Local>(resBodyStr);

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
