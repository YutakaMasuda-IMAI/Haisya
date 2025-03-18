using PartnerWeb.Dto;
using PartnerWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PartnerWeb.API.WebApp
{
    class MasterDataApi : BaseHttpClient
    {


        public MasterDataApi(MapApiSettings mapApiSetting)
        {
            _baseUrl = mapApiSetting.Api.WebAPIHosts;
        }


        public async Task<List<M_CompanyUser_Local>> GetCompanyUserList(int iCompanyID, string kubun = "all")
        {

            string url = _baseUrl + string.Format("MasterData/M_CompanyUsersList?CompanyID={0}", iCompanyID);
            if (kubun != null) { url += string.Format("&kubun={0}", kubun); }
            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, url);

            string resBodyStr;

            List<M_CompanyUser_Local> resultList;

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

                resultList = JsonConvert.DeserializeObject<List<M_CompanyUser_Local>>(resBodyStr);

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
            return resultList;
        }


        public async Task<List<Dto.V_LoginUser_Local>> GetLoginUserList(int iCompanyID)
        {

            string url = _baseUrl + string.Format("MasterData/V_LoginUserList?CompanyID={0}", iCompanyID);

            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, url);

            string resBodyStr;

            List<Dto.V_LoginUser_Local> resultList;

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

                resultList = JsonConvert.DeserializeObject<List<Dto.V_LoginUser_Local>>(resBodyStr);

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
            return resultList;
        }


        public async Task<Dto.V_LoginUser_Local> GetLoginUserList(string companyCode, string loginID, int loginUserID = 0)
        {

            string url = _baseUrl + string.Format("MasterData/V_LoginUser?");
            if (companyCode != null) { url += string.Format("&CompanyCode={0}", companyCode); }
            if (loginID != null) { url += string.Format("&LoginID={0}", loginID); }
            if (loginUserID > 0)  { url += string.Format("&LoginUserID={0}", loginUserID); }

            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, url);

            string resBodyStr;

            Dto.V_LoginUser_Local result;

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

                result = JsonConvert.DeserializeObject<Dto.V_LoginUser_Local>(resBodyStr);

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

        public async Task<IEnumerable<M_CompanyBranch_Local>> GetCompanyBranchList(int iCompanyID)
        {

            string url = _baseUrl + string.Format("MasterData/M_CompanyBranchList?CompanyID={0}", iCompanyID.ToString());
            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, url);

            string resBodyStr;

            IEnumerable<M_CompanyBranch_Local> resultList;

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

                resultList = JsonConvert.DeserializeObject<IEnumerable<M_CompanyBranch_Local>>(resBodyStr);

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
            return resultList;
        }




        public async Task<M_Syaryo_Local> GetSyaryoData(int iCompanyID, string Syasyu, string Kata)
        {

            string url = _baseUrl + string.Format("MasterData/M_SyaryoList?CompanyID={0}", iCompanyID);
            if (Syasyu != null) { url += string.Format("&Syasyu={0}",Syasyu); }
            if (Kata != null) { url += string.Format("&Kata={0}", Kata); }

            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, url);

            string resBodyStr;

            M_Syaryo_Local result;
            IEnumerable<M_Syaryo_Local> resultVal;

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

                resultVal = JsonConvert.DeserializeObject<IEnumerable<M_Syaryo_Local>>(resBodyStr);

                if (resultVal != null && resultVal.Count() > 0)
                {
                    result = resultVal.First();
                } else
                {
                    result = new();
                }
                

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

        public async Task<List<M_Syaryo_Local>> GetSyaryoList(int iCompanyID, string Syasyu = null, string Kata = null)
        {

            string url = _baseUrl + string.Format("MasterData/M_SyaryoList?CompanyID={0}", iCompanyID);
            if (Syasyu != null) { url += string.Format("&Syasyu={0}", Syasyu); }
            if (Kata != null) { url += string.Format("&Kata={0}", Kata); }

            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, url);

            string resBodyStr;

            List<M_Syaryo_Local> result;

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

                result = JsonConvert.DeserializeObject<List<M_Syaryo_Local>>(resBodyStr);

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


        public async Task<List<M_DefaultMoney_Local>> GetDefaultMoneyList(string Syasyu, string Eria)
        {

            string url = _baseUrl + string.Format("MasterData/M_DefaultMoneyList?");
            if (Syasyu != null) { url += string.Format("&Syasyu={0}", Syasyu); }
            if (Eria != null) { url += string.Format("&Eria={0}", Eria); }
            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, url);

            string resBodyStr;

            List<M_DefaultMoney_Local> result;

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

                result = JsonConvert.DeserializeObject<List<M_DefaultMoney_Local>>(resBodyStr);

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


        public async Task<M_DefaultMoney_WaitTimeForArea_Local> GetDefaultMoneyWaitTimeData(string Syasyu, string Eria)
        {

            string url = _baseUrl + string.Format("MasterData/M_DefaultMoneyWaitTimeData?Syasyu={0}&Eria={1}", Syasyu, Eria);
            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, url);

            string resBodyStr;

            M_DefaultMoney_WaitTimeForArea_Local result;

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

                result = JsonConvert.DeserializeObject<M_DefaultMoney_WaitTimeForArea_Local>(resBodyStr);

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

        public async Task<List<M_DefaultMoney_WaitTimeForArea_Local>> GetDefaultMoneyWaitTimeForEriaList(string Syasyu, string Eria)
        {

            string url = _baseUrl + string.Format("MasterData/M_DefaultMoneyWaitTimeForEriaList?");
            if (Syasyu != null) { url += string.Format("&Syasyu={0}", Syasyu); }
            if (Eria != null) { url += string.Format("&Eria={0}", Eria); }
            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, url);

            string resBodyStr;

            List<M_DefaultMoney_WaitTimeForArea_Local> result;

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

                result = JsonConvert.DeserializeObject<List<M_DefaultMoney_WaitTimeForArea_Local>>(resBodyStr);

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

        public async Task<List<M_PersonnelExpense_Local>> GetPersonnelExpenseList(int iCompanyID, string Syasyu = null, string Kata = null)
        {

            string url = _baseUrl + string.Format("MasterData/M_PersonnelExpenseList?CompanyID={0}", iCompanyID);
            if (Syasyu != null) { url += string.Format("&Syasyu={0}", Syasyu); }
            if (Kata != null) { url += string.Format("&Kata={0}", Kata); }
            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, url);

            string resBodyStr;

            List<M_PersonnelExpense_Local> result;

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

                result = JsonConvert.DeserializeObject<List<M_PersonnelExpense_Local>>(resBodyStr);

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

        
        public async Task<M_FuelCost_Local> GetFuelCostData(int iCompanyID)
        {

            string url = _baseUrl + string.Format("MasterData/M_FuelCostData?CompanyID={0}", iCompanyID);
            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, url);

            string resBodyStr;

            M_FuelCost_Local result;

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

                result = JsonConvert.DeserializeObject<M_FuelCost_Local>(resBodyStr);

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


        public async Task<IEnumerable<M_PostCode_Local>> M_AddressToShiKuChoList(string AreaList)
        {

            string url = _baseUrl + string.Format("MasterData/M_AddressToShiKuChoList?AreaList={0}", AreaList);
            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, url);

            string resBodyStr;

            IEnumerable<M_PostCode_Local> result;

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

                result = JsonConvert.DeserializeObject<IEnumerable<M_PostCode_Local>>(resBodyStr);

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="Syasyu"></param>
        /// <param name="Kata"></param>
        /// <returns></returns>
        public async Task<List<M_SyaryoCost_Local>> GetSyaryoCostList(int iCompanyID, string Syasyu = null, string Kata = null)
        {

            string url = _baseUrl + string.Format("MasterData/M_SyaryoCostList?CompanyID={0}", iCompanyID);
            if (Syasyu != null) { url += string.Format("&Syasyu={0}", Syasyu); }
            if (Kata != null) { url += string.Format("&Kata={0}", Kata); }
            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, url);

            string resBodyStr;

            List<M_SyaryoCost_Local> result;

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

                result = JsonConvert.DeserializeObject<List<M_SyaryoCost_Local>>(resBodyStr);

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



        public async Task<List<M_PostCode_Local>> GetGetAddressList(int iArea)
        {

            string url = _baseUrl + string.Format("MasterData/GetAddressList?iArea={0}", iArea);
            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, url);

            string resBodyStr;

            List<M_PostCode_Local> result;

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

                result = JsonConvert.DeserializeObject<List<M_PostCode_Local>>(resBodyStr);

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

        public async Task<List<M_PublishGroup_Local>> M_PublishGroupList(int CompanyID, int BranchID, int UserID)
        {

            string url = _baseUrl + string.Format("MasterData/M_PublishGroupList?");
            if (CompanyID > 0 ) { url += string.Format("&CompanyID={0}", CompanyID); }
            if (BranchID > 0) { url += string.Format("&BranchID={0}", BranchID); }
            if (UserID > 0) { url += string.Format("&UserID={0}", UserID); }

            HttpRequestMessage request = this.CreateRequest(HttpMethod.Get, url);

            string resBodyStr;

            List<M_PublishGroup_Local> result;

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

                result = JsonConvert.DeserializeObject<List<M_PublishGroup_Local>>(resBodyStr);

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
