using HaisyaWeb.Dto;
using HaisyaWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HaisyaWeb.API.WebApp
{
    class HaisyaDataApi : BaseHttpClient
    {

        public HaisyaDataApi(MapApiSettings mapApiSetting)
        {
            _baseUrl = mapApiSetting.Api.WebAPIHosts;
            httpClient.BaseAddress = new Uri(_baseUrl);
        }

        /// <summary>
        /// 配車データの取得
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="targetDate"></param>
        /// <param name="targetDateFrom"></param>
        /// <param name="targetDateTo"></param>
        /// <param name="customerId"></param>
        /// <param name="ankenId"></param>
        /// <param name="senzokuId"></param>
        /// <param name="driverId"></param>
        /// <param name="targetDateUnderLastest"></param>
        /// <returns></returns>
        public async Task<List<Dto.V_HaisyaDataList_Local>> GetHaisyaDataList(int companyId, string targetDate, 
                                                        string targetDateFrom, string targetDateTo, int customerId, 
                                                        int ankenId = 0, int senzokuId = 0, int driverId = 0, int ankenDisplayId = 0,
                                                        string targetDateUnderLastest = null)
        {
            string url = string.Format("HaisyaData/GetHaisyaDataList?companyId={0}", companyId);
            if (targetDate != null) { url += string.Format("&targetDate={0}", targetDate.Replace("/", "-")); }
            if (targetDateFrom != null) { url += string.Format("&targetDateFrom={0}", targetDateFrom.Replace("/", "-")); }
            if (targetDateTo != null) { url += string.Format("&targetDateTo={0}", targetDateTo.Replace("/", "-")); }
            if (customerId != 0) { url += string.Format("&customerId={0}", customerId); }
            if (ankenId != 0) { url += string.Format("&ankenId={0}", ankenId); }
            if (senzokuId != 0) { url += string.Format("&senzokuId={0}", senzokuId); }
            if (driverId != 0) { url += string.Format("&driverId={0}", driverId); }
            if (ankenDisplayId != 0) { url += string.Format("&ankenDisplayId={0}", ankenDisplayId); }
            if (targetDateUnderLastest != null) { url += string.Format("&targetDateUnderLastest={0}", targetDateUnderLastest.Replace("/", "-")); }
            //データ取得
            return await GetHttpData<List<Dto.V_HaisyaDataList_Local>>(url);
        }

        /// <summary>
        /// 配車データを取得する
        /// </summary>
        /// <param name="haisyaId">配車のId</param>
        /// <returns>配車データ</returns>
        public async Task<T_Haisya_Local> GetHaisyaData(int haisyaId)
        {
            string url = string.Format("HaisyaData/GetHaisyaData?haisyaId={0}", haisyaId);
            //データ取得
            return await GetHttpData<T_Haisya_Local>(url);
        }

        /// <summary>
        /// 新規配車割当て処理
        /// </summary>
        /// <param name="dto">HaisyaDataModelDto</param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> AddNewHaisyaData(HaisyaModel.HaisyaDataModelDto dto)
        {
            string url = string.Format("HaisyaData/AddNewHaisyaData");
            //データ更新
            return await ExecHttpData<HaisyaModel.HaisyaDataModelDto>(dto, url);
        }

        /// <summary>
        /// 配車割当て解除処理
        /// </summary>
        /// <param name="haisya">T_Haisya_Local</param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> UnassignHaisyaData(T_Haisya_Local haisya)
        {
            string url = string.Format("HaisyaData/UnassignHaisyaData");
            //データ更新
            return await ExecHttpData<Dto.T_Haisya_Local>(haisya, url);
        }

        /// <summary>
        /// 配車割当ての移動処理
        /// </summary>
        /// <param name="dto">HaisyaDataModelDto</param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> MoveHaisyaData(HaisyaModel.HaisyaDataModelDto dto)
        {
            string url = string.Format("HaisyaData/MoveHaisyaData");
            //データ更新
            return await ExecHttpData<HaisyaModel.HaisyaDataModelDto>(dto, url);
        }

        /// <summary>
        /// 配車ステータスの更新
        /// </summary>
        /// <param name="HaisyaID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> UpdateStatusHaisya(int HaisyaID, int Status)
        {
            string url = string.Format("HaisyaData/UpdateStatusHaisya?HaisyaID={0}&Status={1}", HaisyaID, Status);
            //データ取得
            return await GetHttpData<MsterDataCommonResultValDto_Local>(url);
        }

        ///// <summary>
        ///// 指定AnkenIDの配車済み判定を返却
        ///// </summary>
        ///// <returns></returns>
        public async Task<ContactAbility> CheckEnableContact(int Anken_ID)
        {
            string url = string.Format("HaisyaData/CheckEnableContact?AnkenID={0}", Anken_ID);
            //データ取得
            return await GetHttpData<ContactAbility>(url);
        }

        /// <summary>
        /// T_Haisya_Aroundを取得して返却する
        /// </summary>
        /// <returns></returns>
        public async Task<List<T_Haisya_Around_Local>> GetHaisyaAroundList(String targetDate, int companyId)
        {
            string url = string.Format("HaisyaData/GetHaisyaAroundList?targetDate={0}&companyId={1}", targetDate, companyId);
            //データ取得
            return await GetHttpData<List<T_Haisya_Around_Local>>(url);
        }

        /// <summary>
        /// 傭車ドライバーの配車データを登録する
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="AnkenDisplayID"></param>
        /// <param name="YosyaDriverID"></param>
        /// <param name="YosyaDriverSyaryoID"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> RegisterYosyaDriver(HaisyaModel.HaisyaYosyaDriverRegisterDto dto)
        {
            string url = string.Format("HaisyaData/RegisterYosyaDriver");
            //データ更新
            return await ExecHttpData<HaisyaModel.HaisyaYosyaDriverRegisterDto>(dto, url);
        }

        /// <summary>
        /// 指定された案件IDに基づいて運行指示を取得
        /// </summary>
        /// <param name="AnkenID"></param>
        /// <returns></returns>
        public async Task<OperationInstructionsModel> GetOperationInstructions(int AnkenID)
        {
            string url = _baseUrl + string.Format("HaisyaData/GetOperationInstructions?ankenId={0}", AnkenID);
            //データ取得 
            return await GetHttpData<OperationInstructionsModel>(url);
        }

        /// <summary>
        /// 担当者別配車連絡データの取得
        /// </summary>
        /// <param name="selectTantou">担当</param>
        /// <param name="startDate">開始日</param>
        /// <param name="endDate">終了日</param>
        /// <param name="filter">フィルター</param>
        /// <returns>担当者別配車連絡データの取得</returns>
        public async Task<List<SyabanRenrakuModel>> GetSyabanRenraku(int companyID, int selectTantou, DateOnly selectedDate, DateOnly selectedEndDate, int filter)
        {
            string url = _baseUrl + string.Format("HaisyaData/GetSyabanRenraku?companyId={0}&selectTantou={1}&selectedDate={2}&selectedEndDate={3}&filter={4}", companyID, selectTantou, selectedDate, selectedEndDate, filter);
            //データ取得 
            return await GetHttpData<List<SyabanRenrakuModel>>(url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="customerId"></param>
        /// <param name="customerTantouId"></param>
        /// <param name="targetDate"></param>
        /// <param name="groupID"></param>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public async Task<List<Dto.V_HaisyaDataList_Local>> GetSyabanRenrakuList(int companyId, int customerId, int customerTantouId, String targetDate, int groupID, int Ids)
        {
            string url = _baseUrl + string.Format("HaisyaData/GetSyabanRenrakuList?companyId={0}&customerId={1}&customerTantouId={2}&groupID={3}&Ids={4}", companyId, customerId, customerTantouId, groupID, Ids);
            if (targetDate != null) { url += string.Format("&targetDate={0}", targetDate.Replace("/", "-")); }
            //データ取得
            return await GetHttpData<List<Dto.V_HaisyaDataList_Local>>(url);
        }

        /// <summary>
        /// 車番連絡データの登録
        /// </summary>
        /// <param name="syabanRenrakuPostModel"></param>
        public async Task PostSyabanRenraku(List<SyabanRenrakuPostModel> syabanRenrakuPostModel)
            => await MakePostRequestAsync(syabanRenrakuPostModel, _baseUrl, "HaisyaData/PostSyabanRenraku");

        /// <summary>
        /// 指定条件でT_Haisya_Driver_Day_Remarkを返却する
        /// </summary>
        /// <param name="targetDay"></param>
        /// <param name="DriverId"></param>
        /// <returns></returns>
        public async Task<List<T_Haisya_Driver_Day_Remark_Local>> GetHaisyaDriverDayRemarks(string targetDay, int DriverId = 0)
        {
            string url = string.Format("HaisyaData/GetHaisyaDriverDayRemarks?targetDay={0}&DriverId={1}", targetDay, DriverId);
            //データ取得
            return await GetHttpData<List<T_Haisya_Driver_Day_Remark_Local>>(url);
        }

        /// <summary>
        /// T_Haisya_Yosyaを指定HaisyaIDで取得して返却する
        /// </summary>
        /// <param name="HaisyaId"></param>
        /// <returns></returns>
        #region T_Haisya_Yosya
        public async Task<Dto.T_Haisya_Yosya_Local> GetHaisyaYosyaData(int HaisyaId)
        {
            string url = string.Format("HaisyaData/GetHaisyaYosyaData?HaisyaId={0}", HaisyaId);
            //データ取得
            return await GetHttpData<Dto.T_Haisya_Yosya_Local>(url);
        }
        #endregion T_Haisya_Yosya

    }
}
