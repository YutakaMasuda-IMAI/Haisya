using HaisyaWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HaisyaWeb.API.WebApp
{
    class SalesDataApi : BaseHttpClient
    {
        public SalesDataApi(MapApiSettings mapApiSetting)
        {
            _baseUrl = mapApiSetting.Api.WebAPIHosts;
        }

        /// <summary>
        /// 指定された案件ID、顧客ID、ドライバーID、配車IDに基づいて売上情報を取得します。
        /// </summary>
        /// <param name="Anken_ID">売上情報を取得する案件のID（null 許可）。</param>
        /// <param name="KokyakuId">売上情報を取得する顧客のID（null 許可）。</param>
        /// <param name="Driver_ID">売上情報を取得するドライバーのID（null 許可）。</param>
        /// <param name="Haisya_ID">売上情報を取得する配車のID（null 許可）。</param>
        /// <returns>
        /// 取得した売上情報を含む <see cref="Dto.SalesModel"/> オブジェクトを返します。
        /// </returns>
        /// <remarks>
        /// 指定されたパラメータに基づいて、外部APIエンドポイントから売上データを取得するためにHTTPリクエストを送信します。
        /// URLに各パラメータを含めることで、適切な売上情報をフィルタリングして取得します。
        /// </remarks>
        public async Task<Dto.SalesModel> GetSales(int? Anken_ID, int? KokyakuId, int? Driver_ID, int? Haisya_ID)
        {
            string url = _baseUrl + string.Format("Sales/GetSale?Anken_ID={0}&KokyakuId={1}&Driver_ID={2}&Haisya_ID={3}", Anken_ID, KokyakuId, Driver_ID, Haisya_ID);
            //データ取得 
            return await GetHttpData<Dto.SalesModel>(url);
        }

        /// <summary>
        /// 指定された売上IDに基づいて売上情報を取得します。
        /// </summary>
        /// <param name="Uriage_ID">売上情報を取得する売上のID（null 許可）。</param>
        /// <returns>
        /// 取得した売上情報を含む <see cref="Dto.SalesModel"/> オブジェクトを返します。
        /// </returns>
        /// <remarks>
        /// 指定されたパラメータに基づいて、外部APIエンドポイントから売上データを取得するためにHTTPリクエストを送信します。
        /// URLに各パラメータを含めることで、適切な売上情報をフィルタリングして取得します。
        /// </remarks>
        public async Task<Dto.SalesModel> GetUriagebyId(int? Uriage_ID)
        {
            string url = _baseUrl + string.Format("Sales/GetUriage?Uriage_ID={0}", Uriage_ID);
            //データ取得 
            return await GetHttpData<Dto.SalesModel>(url);
        }

        /// <summary>
        /// 顧客情報の取得
        /// </summary>
        /// <param name="Customer_ID"></param>
        /// <returns>M_Customer_Local</returns>
        public async Task<Dto.M_Customer_Branch_Local> GetCustomer(int Customer_ID)
        {
            string url = _baseUrl + string.Format("Sales/GetCustome?Customer_ID={0}", Customer_ID);
            //データ取得 
            return await GetHttpData<Dto.M_Customer_Branch_Local>(url);
        }

        /// <summary>
        /// 顧客情報の取得
        /// </summary>
        /// <param name="Senzoku_ID"></param>
        /// <param name="date"></param>
        /// <returns>M_Customer_Local</returns>
        public async Task<Dto.SenzokuModel> GetSenzokuData(int Senzoku_ID, DateTime? date)
        {
            string url = _baseUrl + string.Format("Sales/GetSenzokuDat?Senzoku_ID={0}&date={1}", Senzoku_ID, date);
            //データ取得 
            return await GetHttpData<Dto.SenzokuModel>(url);
        }

        /// <summary>
        /// 専属と顧客情報のみの取得
        /// </summary>
        /// <param name="Senzoku_ID"></param>
        /// <returns>M_Customer_Local</returns>
        public async Task<Dto.SenzokuModel> GetSenzoku(int Senzoku_ID)
        {
            string url = _baseUrl + string.Format("Sales/GetSenzok?Senzoku_ID={0}", Senzoku_ID);
            //データ取得 
            return await GetHttpData<Dto.SenzokuModel>(url);
        }

        /// <summary>
        /// 売上情報の登録（月額専属）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Dto.MsterDataCommonResultValDto_Local> PostUriageData(Dto.PostUriageDataModel data)
        {
            string url = _baseUrl + string.Format("Sales/PostUriageDat?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<Dto.PostUriageDataModel>(data, url);
        }

        /// <summary>
        /// 確定売上情報の登録（月額専属）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Dto.MsterDataCommonResultValDto_Local> PostKakuteiUriageData(List<int> uriageIds)
        {
            string url = _baseUrl + string.Format("Sales/PostKakuteiUriageDat?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<List<int>>(uriageIds, url);
        }

        /// <summary>
        /// 売上情報の登録
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Dto.MsterDataCommonResultValDto_Local> PostSales(Dto.SalesModel data)
        {
            string url = _baseUrl + string.Format("Sales/PostSale?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<Dto.SalesModel>(data, url);
        }

        /// <summary>
        /// 売上情報の削除
        /// </summary>
        /// <param name="Uriage_ID"></param>
        /// <returns></returns>
        public async Task<Dto.MsterDataCommonResultValDto_Local> DeleteSales(Dto.SalesModel data)
        {
            string url = _baseUrl + string.Format("Sales/DeleteSale?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<Dto.SalesModel>(data, url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_ID"></param>
        /// <returns></returns>
        public async Task<DateTime?> GetCommitSeikyuShimeTime(int customer_ID)
        {
            string url = _baseUrl + string.Format("Sales/GetCommitSeikyuShimeTime?customer_Id={0}", customer_ID);
            //データ更新
            return await GetHttpData<DateTime?>(url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_ID"></param>
        /// <returns></returns>
        public async Task<DateTime?> GetCommitShitabaraiShimeTime(int customer_ID)
        {
            string url = _baseUrl + string.Format("Sales/GetCommitShitabaraiShimeTime?customer_Id={0}", customer_ID);
            //データ更新
            return await GetHttpData<DateTime?>(url);
        }
    }
}
