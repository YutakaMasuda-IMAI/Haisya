using HaisyaWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HaisyaWeb.API.WebApp
{
    class DailyReportDataApi : BaseHttpClient
    {
        public DailyReportDataApi(MapApiSettings mapApiSetting)
        {
            _baseUrl = mapApiSetting.Api.WebAPIHosts;
            httpClient.BaseAddress = new Uri(_baseUrl);
        }

        /// <summary>
        /// 日報（案件）の詳細情報を取得します。
        /// </summary>
        /// <param name="Anken_ID">案件の一意の識別子。</param>
        /// <param name="AnkenDisplay_ID">案件の一意の識別子。</param>
        /// <param name="DriverCd">案件の一意の識別子。</param>
        /// <param name="Syaban">案件の一意の識別子。</param>
        /// <param name="KokyakuId">顧客ID（nullable）。</param>
        /// <param name="Driver_ID">運転手ID（nullable）。</param>
        /// <param name="Haisya_Kubun">配車区分（nullable）。</param>
        /// <param name="SyaryoManagement_ID">車両管理ID（nullable）。</param>
        /// <param name="Haisya_ID">配車ID（nullable）。</param>
        /// <param name="StartDatetime">レポートの開始日時。</param>
        /// <param name="EndDatetime">レポートの終了日時。</param>
        /// <returns>非同期操作を表すタスク。結果として<see cref="DailyReportModel.DailyReportRegistrationDetailModel"/>を返します。</returns>
        public async Task<DailyReportModel.DailyReportRegistrationDetailModel> GetDailyReportAnken(int Anken_ID, int AnkenDisplay_ID, int DriverCd, int Syaban,
                                  int? KokyakuId, int? Driver_ID, int? Haisya_Kubun, int? SyaryoManagement_ID, int? Haisya_ID,
                                  DateTime StartDatetime, DateTime EndDatetime)
        {
            string url = string.Format("DailyReport/GetDailyReportAnken?Anken_ID={0}", Anken_ID);
            url += string.Format("&AnkenDisplay_ID={0}", AnkenDisplay_ID);
            url += string.Format("&DriverCd={0}", DriverCd);
            url += string.Format("&Syaban={0}", Syaban);
            url += string.Format("&KokyakuId={0}", KokyakuId ?? 0);
            url += string.Format("&Driver_ID={0}", Driver_ID ?? 0);
            url += string.Format("&Haisya_Kubun={0}", Haisya_Kubun ?? 0);
            url += string.Format("&SyaryoManagement_ID={0}", SyaryoManagement_ID ?? 0);
            url += string.Format("&Haisya_ID={0}", Haisya_ID ?? 0);
            url += string.Format("&StartDatetime={0}", StartDatetime);
            url += string.Format("&EndDatetime={0}", EndDatetime);
            //データ取得 
            return await GetHttpData<DailyReportModel.DailyReportRegistrationDetailModel>(url);
        }

        /// <summary>
        /// 日報の詳細情報を取得します。
        /// </summary>
        /// <param name="Anken_ID">案件の一意の識別子。</param>
        /// <param name="AnkenDisplay_ID">案件の一意の識別子。</param>
        /// <param name="DriverCd">案件の一意の識別子。</param>
        /// <param name="Syaban">案件の一意の識別子。</param>
        /// <param name="KokyakuId">顧客ID（nullable）。</param>
        /// <param name="Driver_ID">運転手ID（nullable）。</param>
        /// <param name="Haisya_Kubun">配車区分（nullable）。</param>
        /// <param name="SyaryoManagement_ID">車両管理ID（nullable）。</param>
        /// <param name="Haisya_ID">配車ID（nullable）。</param>
        /// <param name="StartDatetime">レポートの開始日時。</param>
        /// <param name="EndDatetime">レポートの終了日時。</param>
        /// <returns>非同期操作を表すタスク。結果として<see cref="DailyReportModel.DailyReportRegistrationDetailModel"/>を返します。</returns>
        public async Task<DailyReportModel.DailyReportRegistrationDetailModel> GetDailyReportDetail(int Anken_ID, int AnkenDisplay_ID, int DriverCd, int Syaban,
                                  int? KokyakuId, int? Driver_ID, int? Haisya_Kubun, int? SyaryoManagement_ID, int? Haisya_ID, 
                                  DateTime StartDatetime, DateTime EndDatetime)
        {
            string url = string.Format("DailyReport/GetDailyReportDetail?Anken_ID={0}", Anken_ID);
            url += string.Format("&AnkenDisplay_ID={0}", AnkenDisplay_ID);
            url += string.Format("&DriverCd={0}", DriverCd);
            url += string.Format("&Syaban={0}", Syaban);
            url += string.Format("&Customer_Branch_ID={0}", KokyakuId ?? 0);
            url += string.Format("&Driver_ID={0}", Driver_ID ?? 0);
            url += string.Format("&Haisya_Kubun={0}", Haisya_Kubun ?? 0);
            url += string.Format("&SyaryoManagement_ID={0}", SyaryoManagement_ID ?? 0);
            url += string.Format("&Haisya_ID={0}", Haisya_ID ?? 0);
            url += string.Format("&StartDatetime={0}", StartDatetime);
            url += string.Format("&EndDatetime={0}", EndDatetime);
            //データ取得 
            return await GetHttpData<DailyReportModel.DailyReportRegistrationDetailModel>(url);
        }

        /// <summary>   
        /// Certificationリクエストモデルを取得します。
        /// </summary>
        /// <returns>非同期操作を表すタスク。結果として<see cref="DailyReportModel.CertificationRequestModel"/>を返します。</returns>
        public async Task<Dto.CertificationRequestModel> GetCertificationRequestModel(int Nippou_Approval_ID)
        {
            string url = string.Format("DailyReport/GetCertificationRequestModel?Nippou_Approval_ID={0}",Nippou_Approval_ID);
            return await GetHttpData<Dto.CertificationRequestModel>(url);
        }

        /// <summary>
        /// CertificationRequestModelを送信します        
        /// </summary>
        /// <param name="data">送信する認証リクエストモデル。</param>
        /// <returns>非同期操作を表すタスク。</returns>
        public async Task<Dto.MsterDataCommonResultValDto_Local> PostCertificationRequest(Dto.CertificationRequestModel data)
        {
            string url = string.Format("DailyReport/PostCertificationReques");
            //データ更新
            return await ExecHttpData(data, url);
        }

        /// <summary>
        /// 日報の仮登録を行います。
        /// </summary>
        /// <param name="data">仮登録する日報の詳細モデル。</param>
        /// <returns>非同期操作を表すタスク。</returns>
        public async Task<Dto.MsterDataCommonResultValDto_Local> ProvisionalRegistration(DailyReportModel.DailyReportRegistrationDetailModel data)
        {
            string url = string.Format("DailyReport/PostProvisionalRegistration");
            //データ更新
            return await ExecHttpData(data, url);
        }

        /// <summary>
        ///  日報の一覧を取得します。
        /// </summary>
        /// <returns></returns>
        public async Task<List<Dto.T_Nippou_Local>> GetTNippous()
        {
            string url = string.Format("DailyReport/GetTNippous");
            return await GetHttpData<List<Dto.T_Nippou_Local>>(url);
        }

        /// <summary>
        /// 指定AnkenDisplay_IDの日報データを取得します。
        /// </summary>
        /// <returns></returns>
        public async Task<List<Dto.T_Nippou_Local>> GetNippou(int AnkenDisplay_ID)
        {
            string url = string.Format("DailyReport/GetNippou?AnkenDisplay_ID={0}", AnkenDisplay_ID);
            return await GetHttpData<List<Dto.T_Nippou_Local>>(url);
        }
    }
}
