using HaisyaWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static HaisyaWeb.Models.HaisyaModel;

namespace HaisyaWeb.API.WebApp
{
    class KinTaiDataApi : BaseHttpClient
    {
        public KinTaiDataApi(MapApiSettings mapApiSetting)
        {
            _baseUrl = mapApiSetting.Api.WebAPIHosts;
            httpClient.BaseAddress = new Uri(_baseUrl);
        }

        /// <summary>
        /// 指定された日付とユーザーIDに基づいて、休日登録のリクエストを送信します。
        /// </summary>
        /// <param name="model">AttendanceModalViewModel</param>
        /// <remarks>
        /// 休日情報を保持する `HolidayModel` オブジェクトを作成し、それを用いて休日登録APIにPOSTリクエストを送信します。
        /// </remarks>
        /// <returns></returns>
        public async Task<Dto.MsterDataCommonResultValDto_Local> PostRegisterAttendanceModal(AttendanceModalViewModel model) {

            string url = "KintaiData/PostRegisterAttendanceModal";
            return await ExecHttpData<AttendanceModalViewModel>(model, url);
        }

        /// <summary>
        /// 指定されたユーザーIDと日付に基づいて、有休画面に必要な情報を取得します。
        /// </summary>
        /// <param name="userId">情報を取得する対象ユーザーのID。</param>
        /// <param name="date">対象日付。この日付に基づいてデータを取得します。</param>
        /// <returns>
        /// 取得した有休情報を含む <see cref="AttendanceModalViewModel"/> オブジェクトを返します。
        /// </returns>
        /// <remarks>
        /// 指定されたユーザーと日付に対応する有休データを取得するために、外部APIエンドポイントに対してHTTPリクエストを送信します。
        /// </remarks>
        public async Task<AttendanceModalDto> GetSelectUserData(int userId, DateTime date, int employeeNumber)
        {
            string url = string.Format("KintaiData/GetAttendanceModalViewModel?userId={0}&date={1}&employeeNumber={2}", userId, date, employeeNumber);
            //データ取得 
            return await GetHttpData<AttendanceModalDto>(url);
        }

        /// <summary>
        /// 確定勤怠情報の返却
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="DriverId"></param>
        /// <returns></returns>
        public async Task<List<Dto.T_Kintai_Commit_Local>> GetKintaiCommit(int CompanyId, string dateFrom, string dateTo, int DriverId = 0)
        {
            string url = string.Format("KintaiData/KintaiCommitList?CompanyID={0}&DriverID={1}&dateFrom={2}&dateTo={3}", CompanyId, DriverId, dateFrom, dateTo);
            return await GetHttpData<List<Dto.T_Kintai_Commit_Local>>(url);
        }

        /// <summary>
        /// 休暇区分マスタの返却
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public async Task<List<Dto.M_LEAVE_Local>> GetLeaveList(int CompanyID)
        {
            string url = string.Format("KintaiData/LeaveList?CompanyID={0}", CompanyID);
            return await GetHttpData<List<Dto.M_LEAVE_Local>>(url);
        }

        /// <summary>
        /// 休暇理由マスタの返却
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public async Task<List<Dto.M_LEAVE_REASON_Local>> GetLeaveReasonList(int CompanyID)
        {
            string url = string.Format("KintaiData/LeaveReasonList?CompanyID={0}", CompanyID);
            return await GetHttpData<List<Dto.M_LEAVE_REASON_Local>>(url);
        }

        /// <summary>
        /// 乗務外勤怠情報の返却
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="DriverId"></param>
        /// <returns></returns>
        public async Task<List<Dto.T_KINTAI_COMMIT_NON_CREW_Local>> GetKintaiCommitNonCrew(int CompanyId, string dateFrom, string dateTo, int DriverId = 0)
        {
            string url = string.Format("KintaiData/KintaiCommitNonCrewList?CompanyID={0}&driverId={1}&dateFrom={2}&dateTo={3}", CompanyId, DriverId, dateFrom, dateTo);
            return await GetHttpData<List<Dto.T_KINTAI_COMMIT_NON_CREW_Local>>(url);
        }

        /// <summary>
        /// 残有休日数リストの返却
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="DriverId"></param>
        /// <returns></returns>
        public async Task<List<Dto.T_Leave_Summary_Local>> GetKintaiLeaveSummary(int CompanyId, int DriverId = 0)
        {
            string url = string.Format("KintaiData/KintaiLeaveSummaryList?CompanyID={0}&DriverID={1}", CompanyId, DriverId);
            return await GetHttpData<List<Dto.T_Leave_Summary_Local>>(url);
        }

        /// <summary>
        /// 休日リストの返却
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public async Task<List<Dto.M_Holiday_Local>> GetHolidayList(int CompanyId, string dateFrom = null, string dateTo = null)
        {
            string url = string.Format("KintaiData/GetHolidayList?CompanyID={0}&dateFrom={1}&dateTo={2}", CompanyId, dateFrom, dateTo);
            return await GetHttpData<List<Dto.M_Holiday_Local>>(url);
        }

        /// <summary>
        /// V_TOTAL_WORKING_TIMEの返却
        /// 月次の乗務員毎の走行距離、総労働時間
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="nengetu"></param>
        /// <param name="driverId"></param>
        /// <returns></returns>
        public async Task<List<Dto.V_TOTAL_WORKING_TIME_Local>> GetKintaiTotalWorkingTimeList(int CompanyId, string nengetu, int driverId = 0)
        {
            string url = string.Format("KintaiData/GetKintaiTotalWorkingTimeList?CompanyID={0}&nengetu={1}&driverId={2}", CompanyId, nengetu, driverId);
            return await GetHttpData<List<Dto.V_TOTAL_WORKING_TIME_Local>>(url);
        }

        /// <summary>
        /// 指定されたユーザーIDと日付に基づいて、個別月次配車表に必要な情報を取得します。
        /// </summary>
        /// <param name="userId">対象となるユーザーのID。</param>
        /// <param name="date">対象月の任意の日付。この日付に基づいて月次配車表のデータを取得します。</param>
        /// <returns>
        /// 取得した配車表のデータを含む <see cref="MonthScheduleModelViewModel"/> オブジェクトを返します。
        /// </returns>
        /// <remarks>
        /// 指定されたユーザーと月に対応する配車情報を取得するために、外部APIエンドポイントに対してHTTPリクエストを送信します。
        /// リクエストURLは、ユーザーIDと日付をクエリパラメータとして構成されます。
        /// </remarks>
        public async Task<MonthScheduleModelViewModel> GetSelectUserMonthScheduleData(int userId, DateTime date)
        {
            string url = string.Format("KintaiData/MonthScheduleData?userId={0}&date={1}", userId, date);
            return await GetHttpData<MonthScheduleModelViewModel>(url);
        }

        /// <summary>
        /// T_Kintaiデータの返却
        /// 連続勤務日数、拘束時間
        /// </summary>
        /// <param name="day"></param>
        /// <param name="DriverID"></param>
        /// <returns></returns>
        public async Task<List<Dto.T_Kintai_Local>> GetKintaiList(string day = null, int driverID = 0)
        {
            string url = string.Format("KintaiData/KintaiList?day={0}&DriverID={1}", day, driverID);
            return await GetHttpData<List<Dto.T_Kintai_Local>>(url);
        }
    }
}
