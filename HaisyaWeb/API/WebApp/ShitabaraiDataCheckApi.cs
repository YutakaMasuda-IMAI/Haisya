using HaisyaWeb.Dto;
using HaisyaWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HaisyaWeb.API.WebApp
{
    class ShitabaraiDataCheckApi : BaseHttpClient
    {
        public ShitabaraiDataCheckApi(MapApiSettings mapApiSetting)
        {
            _baseUrl = mapApiSetting.Api.WebAPIHosts;
        }

        /// <summary>
        /// 下払いリストの返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="printDate">印刷日</param>
        /// <param name="yosyasakiFrom">予社先From</param>
        /// <param name="yosyasakiTo">予社先To</param>
        /// <param name="shiharaiNengetu">支払年月</param>
        /// <param name="shimeDay">締め日</param>
        /// <param name="zeiKubun">税区分</param>
        /// <param name="shiharaiDateTo">支払日To</param>
        /// <param name="ShiharaiTantou">支払担当</param>
        /// <param name="checkShitabaraiId">下払いID</param>        
        /// <returns></returns>
        public async Task<List<V_ShitabaraiCheckDataList_Local>> GetShitabaraiCheckDataList(int CompanyID, string printDate, string yosyasakiFrom, string yosyasakiTo, string shiharaiNengetu, int? shimeDay, int? zeiKubun, string shiharaiDateTo, string ShiharaiTantou, int checkShitabaraiId = 0)
        {
            string url = _baseUrl + string.Format("Shitabarai/GetShitabaraiCheckDataListEx?CompanyID={0}&printDate={1}&yosyasakiFrom={2}&yosyasakiTo={3}&shiharaiNengetu={4}&shimeDay={5}&zeiKubun={6}&shiharaiDateTo={7}&ShiharaiTantou={8}&checkShitabaraiId={9}", CompanyID.ToString(), printDate, yosyasakiFrom ?? "", yosyasakiTo ?? "", shiharaiNengetu, shimeDay ?? 0, zeiKubun, shiharaiDateTo, ShiharaiTantou ?? "", checkShitabaraiId);
            //データ取得
            return await GetHttpData<List<V_ShitabaraiCheckDataList_Local>>(url);
        }

        /// <summary>
        /// 下払い問い合わせ発行処理
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> PublishShitabaraiCheckDataList(ShitabaraiCheckInquiryDto_Local dto)
        {
            string url = _baseUrl + string.Format("Shitabarai/PublishShitabaraiCheckDataList?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData(dto, url);
        }

        /// <summary>
        /// 指定されたIDに基づいて下払いチェック情報を取得
        /// </summary>
        /// <param name="checkShitabaraiId">下払いチェックID</param>
        /// <returns>下払いチェック情報</returns>
        public async Task<Dto.T_Check_Shitabarai_Local> GetTCheckShitabaraiById(int checkShitabaraiId)
        {
            string url = _baseUrl + string.Format("Shitabarai/GetTCheckShitabaraiById?checkShitabaraiId={0}", checkShitabaraiId);
            //データ取得
            return await GetHttpData<Dto.T_Check_Shitabarai_Local>(url);
        }

        /// <summary>
        /// 指定されたIDに基づいて下払いチェック詳細情報を取得
        /// </summary>
        /// <param name="checkShitabaraiId">下払いチェックID</param>
        /// <returns>下払いチェック詳細情報</returns>
        public async Task<Dto.T_Check_Shitabarai_Detail_Local> GetTCheckShitabaraiDetailById(int checkShitabaraiId)
        {
            string url = _baseUrl + string.Format("Shitabarai/GetTCheckShitabaraiDetailById?checkShitabaraiId={0}", checkShitabaraiId);
            //データ取得
            return await GetHttpData<Dto.T_Check_Shitabarai_Detail_Local>(url);
        }

        /// <summary>
        /// T_Check_Shitabarai_Detailを複数取得
        /// </summary>
        /// <param name="checkShitabaraiId">チェック下払いID</param>
        /// <returns><List<T_Check_Shitabarai_Detail_Local></returns>
        public async Task<List<Dto.T_Check_Shitabarai_Detail_Local>> GetTCheckShitabaraiDetailListById(int checkShitabaraiId)
        {
            string url = _baseUrl + string.Format("Shitabarai/GetTCheckShitabaraiDetailListById?checkShitabaraiId={0}", checkShitabaraiId);
            //データ取得
            return await GetHttpData<List<Dto.T_Check_Shitabarai_Detail_Local>>(url);
        }
        

        /// <summary>
        /// 指定されたIDに基づいて売上下払い情報を取得
        /// </summary>
        /// <param name="uriageShiharaiID">売上支払ID</param>
        /// <returns>売上下払い情報</returns>
        public async Task<Dto.T_Uriage_Shitabarai_Local> GetTUriageShitabaraiById(int uriageShiharaiID)
        {
            string url = _baseUrl + string.Format("Shitabarai/GetTUriageShitabaraiById?uriageShiharaiID={0}", uriageShiharaiID);
            //データ取得
            return await GetHttpData<Dto.T_Uriage_Shitabarai_Local>(url);
        }

        /// <summary>
        /// 指定されたIDに基づいてT_Anken_Detailの取得を取得
        /// </summary>
        /// <param name="uriageID">売上ID</param>
        /// <returns>T_Anken_Detail_Local</returns>
        public async Task<Dto.T_Anken_Detail_Local> GetTAnkenDetailByUriageID(int uriageID)
        {
            string url = _baseUrl + string.Format("Shitabarai/GetTAnkenDetailByUriageID?uriageID={0}", uriageID);
            //データ取得
            return await GetHttpData<Dto.T_Anken_Detail_Local>(url);
        }

        /// <summary>
        /// 指定されたIDに基づいてM_Syaryoの取得を取得
        /// </summary>
        /// <param name="uriageID">売上ID</param>
        /// <returns>M_Syaryo_Local</returns>
        public async Task<Dto.M_Syaryo_Local> GetMSyaryoByUriageID(int uriageID)
        {
            string url = _baseUrl + string.Format("Shitabarai/GetMSyaryoByUriageID?uriageID={0}", uriageID);
            //データ取得
            return await GetHttpData<Dto.M_Syaryo_Local>(url);
        }

        /// <summary>
        /// 指定されたIDに基づいて売上情報を取得
        /// </summary>
        /// <param name="uriageID">売上ID</param>
        /// <returns>売上情報</returns>
        public async Task<Dto.T_Uriage_Local> GetTUriageById(int uriageID)
        {
            string url = _baseUrl + string.Format("Shitabarai/GetTUriageById?uriageID={0}", uriageID);
            //データ取得
            return await GetHttpData<Dto.T_Uriage_Local>(url);
        }

        /// <summary>
        /// 指定されたIDに基づいて下払い詳細情報を取得
        /// </summary>
        /// <param name="uriageID">売上ID</param>
        /// <returns>下払い詳細情報</returns>
        public async Task<Dto.T_Shitabarai_Detail_Local> GetTShitabaraiDetail(int uriageID)
        {
            string url = _baseUrl + string.Format("Shitabarai/GetTShitabaraiDetail?uriageID={0}", uriageID);
            //データ取得
            return await GetHttpData<T_Shitabarai_Detail_Local>(url);
        }

        /// <summary>
        /// 指定されたIDに基づいて下払い情報を取得
        /// </summary>
        /// <param name="shitabaraiId">下払いID</param>
        /// <returns>下払い情報</returns>
        public async Task<Dto.T_Shitabarai_Local> GetTShitabarai(int shitabaraiId)
        {
            string url = _baseUrl + string.Format("Shitabarai/GetTShitabarai?shitabaraiId={0}", shitabaraiId);
            //データ取得
            return await GetHttpData<T_Shitabarai_Local>(url);
        }

        /// <summary>
        /// 指定されたIDに基づいて予社支払情報を取得
        /// </summary>
        /// <param name="shitabaraiID">下払いID</param>
        /// <returns>予社支払情報</returns>
        public async Task<Dto.T_YosyaShiharai_Local> GetTYosyaShiharai(int shitabaraiID)
        {
            string url = _baseUrl + string.Format("Shitabarai/GetTYosyaShiharai?shitabaraiID={0}", shitabaraiID);
            //データ取得
            return await GetHttpData<Dto.T_YosyaShiharai_Local>(url);
        }

        /// <summary>
        /// 指定された下払いチェックIDに基づいて印刷情報を取得
        /// </summary>
        /// <param name="checkShitabaraiId">下払いチェックID</param>
        /// <returns>下払い印刷情報</returns>
        public async Task<Dto.T_Print_Shitabarai_Local> GetTPrintShiharai(int checkShitabaraiId)
        {
            string url = _baseUrl + string.Format("Shitabarai/GetTPrintShiharai?checkShitabaraiId={0}", checkShitabaraiId);
            //データ取得
            return await GetHttpData<Dto.T_Print_Shitabarai_Local>(url);
        }

        /// <summary>
        /// 指定された印刷下払いIDに基づいて印刷詳細情報を取得
        /// </summary>
        /// <param name="printShitabaraiId">印刷下払いID</param>
        /// <returns>印刷下払い詳細情報のリスト</returns>
        public async Task<List<Dto.T_Print_Shitabarai_Detail_Local>> GetTPrintShiharaiDetail(int printShitabaraiId)
        {
            string url = _baseUrl + string.Format("Shitabarai/GetTPrintShiharaiDetail?printShitabaraiId={0}", printShitabaraiId);
            //データ取得
            return await GetHttpData<List<Dto.T_Print_Shitabarai_Detail_Local>>(url);
        }

        /// <summary>
        /// 指定された下払いチェックIDと売上支払IDに基づいて下払い変更情報を取得
        /// </summary>
        /// <param name="checkShitabaraiId">下払いチェックID</param>
        /// <param name="uriageShitabaraiId">売上支払ID (オプション)</param>
        /// <returns>下払い変更情報のリスト</returns>
        public async Task<List<T_Check_Shitabarai_Change_Local>> GetTCheckShitabaraiChange(int checkShitabaraiId, int? uriageShitabaraiId = null)
        {
            string url = _baseUrl + string.Format("Shitabarai/GetTCheckShitabaraiChange?checkShitabaraiId={0}", checkShitabaraiId);

            if (uriageShitabaraiId != null)
            {
                url += string.Format("&uriageShitabaraiId={0}", uriageShitabaraiId);
            }

            //データ取得
            return await GetHttpData<List<Dto.T_Check_Shitabarai_Change_Local>>(url);
        }

        /// <summary>
        /// 指定された予社支店IDに基づいて予社支店情報を取得
        /// </summary>
        /// <param name="yosyaBranchId">予社支店ID</param>
        /// <returns>予社支店情報</returns>
        public async Task<Dto.M_Yosya_Branch_Local> GetMYosyaBranch(int yosyaBranchId)
        {
            string url = _baseUrl + string.Format("Shitabarai/GetMYosyaBranch?yosyaBranchId={0}", yosyaBranchId);
            //データ取得
            return await GetHttpData<Dto.M_Yosya_Branch_Local>(url);
        }

        /// <summary>
        /// T_Check_Shitabarai_Changeの更新
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Dto.MsterDataCommonResultValDto_Local> ShitabaraiPostBatchRegistration(Dto.ShitabaraiBatchRegistrationModel_Local data)
        {
            string url = _baseUrl + string.Format("Shitabarai/ShitabaraiPostBatchRegistration?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<Dto.ShitabaraiBatchRegistrationModel_Local>(data, url);
        }


        /// <summary>
        /// 下払いバッチ登録のキャンセル処理
        /// </summary>
        /// <param name="data">下払いバッチ登録データ</param>
        /// <returns>非同期処理のタスク</returns>
        public async Task<Dto.MsterDataCommonResultValDto_Local> ShitabaraiPostBatchCancel(Dto.ShitabaraiBatchRegistrationModel_Local data)
        {
            string url = _baseUrl + string.Format("Shitabarai/ShitabaraiPostBatchCancel?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<Dto.ShitabaraiBatchRegistrationModel_Local>(data, url);
        }

        /// <summary>
        /// T_Check_Shitabarai_Change・T_Check_Shitabarai_Detail・T_Uriage_Shitabaraiの更新
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Dto.MsterDataCommonResultValDto_Local> PostModalApproval(Dto.ShitabaraiModalApprovalModel_Local data)
        {
            string url = _baseUrl + string.Format("Shitabarai/PostShitabaraiModalApproval?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<Dto.ShitabaraiModalApprovalModel_Local>(data, url);
        }

        /// <summary>
        /// T_Check_Shitabarai_Doneデータの返却
        /// </summary>
        /// <param name="Check_Shitabarai_ID"></param>
        /// <returns></returns>
        public async Task<Dto.T_Check_Shitabarai_Done_Local> GetT_Check_Shitabarai_Done(int Check_Shitabarai_ID)
        {
            string url = _baseUrl + string.Format("Shitabarai/GetT_Check_Shitabarai_Done?Check_Shitabarai_ID={0}", Check_Shitabarai_ID);
            return await GetHttpData<Dto.T_Check_Shitabarai_Done_Local>(url);
        }


        /// <summary>
        /// T_Uriage・T_Check_Shitabarai_Detail・T_Check_Shitabaraiの更新
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Dto.MsterDataCommonResultValDto_Local> UpdateApprovalStatus(Dto.ShitabaraiApprovalStatusModel_Local data)
        {
            string url = _baseUrl + string.Format("Shitabarai/UpdateApprovalStatusShitabarai?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<Dto.ShitabaraiApprovalStatusModel_Local>(data, url);
        }

    }
}