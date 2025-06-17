using HaisyaWeb.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HaisyaWeb.API
{
    internal class BaseHttpClient : IDisposable
    {
        // <summary>
        /// 通信先のベースURL
        /// </summary>
        public string _baseUrl;

        //// <summary>
        ///// 通信先のベースURL
        ///// </summary>
        //public string _baseMapUrl;

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
            HttpRequestMessage request = new HttpRequestMessage(httpMethod, requestEndPoint);
            return AddHeaders(request);
        }

        public void Dispose()
        {
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
                if (response.IsSuccessStatusCode)
                {
                    resBodyStr = response.Content.ReadAsStringAsync().Result;

                    resStatusCoode = response.StatusCode;

                    if (resStatusCoode.Equals(HttpStatusCode.NoContent))
                    {
                        result = JsonConvert.DeserializeObject<T>(resBodyStr);
                    }
                    else
                    {
                        if (resStatusCoode == System.Net.HttpStatusCode.InternalServerError) throw new HttpRequestException(resBodyStr);
                        if (resStatusCoode == HttpStatusCode.BadRequest) throw new HttpRequestException(resBodyStr);
                        if (!(resStatusCoode.Equals(HttpStatusCode.OK))) throw new HttpRequestException(resStatusCoode.ToString());
                        if (String.IsNullOrEmpty(resBodyStr)) throw new HttpRequestException(resStatusCoode.ToString());
                        result = JsonConvert.DeserializeObject<T>(resBodyStr);
                    }

                }
                else
                {
                    resBodyStr = response.Content.ReadAsStringAsync().Result.Trim('\"');
                    Console.WriteLine("Error: " + response.StatusCode);
                    throw new HttpRequestException(resBodyStr);
                }


            }
            catch (HttpRequestException ex)
            {
                // UNDONE: 通信失敗のエラー処理
                Console.WriteLine("HttpRequestException: " + ex.Message + ":: URL:" + url);
                throw;
            }

            return (T)result;
        }

        /// <summary>
        /// Http経由での汎用データ更新処理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <param name="url"></param>
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
                // リクエストが成功したかどうかを確認する
                if (response.IsSuccessStatusCode)
                {
                    resBodyStr = response.Content.ReadAsStringAsync().Result;
                    resStatusCoode = response.StatusCode;

                    if (resStatusCoode == System.Net.HttpStatusCode.InternalServerError) throw new HttpRequestException(resBodyStr);
                    if (resStatusCoode == HttpStatusCode.BadRequest) throw new HttpRequestException(resBodyStr);
                    if (!resStatusCoode.Equals(HttpStatusCode.OK)) throw new HttpRequestException(resBodyStr);
                    if (String.IsNullOrEmpty(resBodyStr)) throw new HttpRequestException(resStatusCoode.ToString());
                    result = JsonConvert.DeserializeObject<Dto.MsterDataCommonResultValDto_Local>(resBodyStr);
                }
                else
                {
                    result = null;
                    resBodyStr = response.Content.ReadAsStringAsync().Result;
                    if (!String.IsNullOrEmpty(resBodyStr))
                    {
                        result = JsonConvert.DeserializeObject<Dto.MsterDataCommonResultValDto_Local>(resBodyStr);
                        Console.WriteLine("Error: " + response.StatusCode + ":::" + result);
                        return result;
                    }
                    else
                    {
                        Console.WriteLine("Error: " + response.StatusCode);
                        throw new HttpRequestException(resStatusCoode.ToString());
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("HttpRequestException: " + ex.Message);
                throw;
            }

            // 中身のチェックなどを経て終了。
            return result;
        }

        /// <summary>
        /// Http経由での汎用データ更新処理
        /// 指定型（TResponse）でDeserializeして返却
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="param"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<TResponse> ExecHttpDataToResponse<T, TResponse>(T param, string url)
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

            TResponse result = default;

            HttpStatusCode resStatusCoode = HttpStatusCode.NotFound;

            HttpResponseMessage response;

            // 通信実行。メンバ変数でhttpClientを持っているので、using(～)で囲いません。囲うと通信後にオブジェクトが破棄されます。
            // 引数にrequestを取る場合はGetAsyncやPostAsyncでなくSendAsyncメソッドになります。
            // 戻り値はTask<HttpResponseMessage>で、変数名.ResultとするとSystem.Net.Http.HttpResponseMessageクラスが取れます。
            try
            {
                response = await httpClient.PostAsync(request.RequestUri, multiContent);
                // リクエストが成功したかどうかを確認する
                if (response.IsSuccessStatusCode)
                {
                    resBodyStr = await response.Content.ReadAsStringAsync();
                    resStatusCoode = response.StatusCode;

                    if (resStatusCoode == System.Net.HttpStatusCode.InternalServerError) throw new HttpRequestException(resBodyStr);
                    if (resStatusCoode == HttpStatusCode.BadRequest) throw new HttpRequestException(resBodyStr);
                    if (!resStatusCoode.Equals(HttpStatusCode.OK)) throw new HttpRequestException(resBodyStr);
                    if (String.IsNullOrEmpty(resBodyStr)) throw new HttpRequestException(resStatusCoode.ToString());

                    // Deserialize with proper options
                    JsonSerializerOptions options = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true, };
                    result = System.Text.Json.JsonSerializer.Deserialize<TResponse>(resBodyStr, options);
                }
                else
                {
                    resBodyStr = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Error: " + response.StatusCode + "   Message:" + resBodyStr);
                    throw new HttpRequestException(resBodyStr);
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                throw;
            }

            // 中身のチェックなどを経て終了。
            return result;
        }

        /// <summary>
        ///  Http経由での汎用データ更新処理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <param name="url"></param>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public async Task MakePostRequestAsync<T>(T param, string url, string endpoint)
        {
            // HttpClientのインスタンスを作成する
            using (HttpClient client = new HttpClient())
            {
                // APIのベースアドレスを設定する
                client.BaseAddress = new Uri(url);

                try
                {
                    string json = System.Text.Json.JsonSerializer.Serialize<T>(param);
                    // リクエストで送信するコンテンツを準備する
                    StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    // POSTリクエストを送信する
                    HttpResponseMessage response = await client.PostAsync(endpoint, content);

                    // リクエストが成功したかどうかを確認する
                    if (response.IsSuccessStatusCode)
                    {
                        // レスポンスのコンテンツを読み取る
                        string responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Response: " + responseContent);
                    }
                    else
                    {
                        string resBodyStr = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Error: " + response.StatusCode + "   Message:" + resBodyStr);
                        throw new HttpRequestException(resBodyStr);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="param"></param>
        /// <param name="url"></param>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public async Task<ApiResponse<TResponse>> MakePostGetRequestAsync<T, TResponse>(T param, string url, string endpoint)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                try
                {
                    string json = System.Text.Json.JsonSerializer.Serialize(param);
                    StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(endpoint, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();

                        // Deserialize with proper options
                        JsonSerializerOptions options = new System.Text.Json.JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                            // Additional options can be added here if necessary
                        };
                        TResponse result = System.Text.Json.JsonSerializer.Deserialize<TResponse>(responseContent, options);
                        return new ApiResponse<TResponse>
                        {
                            Success = true,
                            Message = "Request succeeded",
                            Data = result
                        };
                    }
                    else
                    {
                        string errorMessage = response.Content.ReadAsStringAsync().Result.Trim('\"');
                        return new ApiResponse<TResponse>
                        {
                            Success = false,
                            Message = $"{errorMessage}",
                            Data = default
                        };
                    }
                }
                catch (Exception ex)
                {
                    return new ApiResponse<TResponse>
                    {
                        Success = false,
                        Message = $"Exception: {ex.Message}",
                        Data = default
                    };
                }
            }
        }
    }
}
