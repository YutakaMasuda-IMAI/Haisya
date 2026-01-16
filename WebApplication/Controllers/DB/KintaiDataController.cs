using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Common;
using WebApplication.Data;
using WebApplication.Data.Kintai;
using WebApplication.Model;
using WebApplication.Services;

namespace WebApplication.Controllers.DB
{
    /// <summary>
    /// 勤怠データを管理するコントローラー
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class KintaiDataController : MyBaseController
    {
        private readonly ILogger<KintaiDataController> _logger;
        private readonly IAttendanceService _attendanceService;

        /// <summary>
        /// KintaiDataControllerのコンストラクタ
        /// </summary>
        /// <param name="logger">ロガー</param>
        /// <param name="contextKintai">勤怠データベースコンテキスト</param>
        /// <param name="context">アプリケーションデータベースコンテキスト</param>
        /// <param name="attendanceService">勤怠サービス</param>
        public KintaiDataController(ILogger<KintaiDataController> logger, ApplicationDbContextKintai contextKintai,
            ApplicationDbContext context, IAttendanceService attendanceService)
        {
            _logger = logger;
            _context = context;
            _contextKintai = contextKintai;
            _attendanceService = attendanceService;
        }

        [HttpGet("GetKintaiTest")]
        public string GetKintaiTest()
        {
            return _contextKintai.Database.GetConnectionString();


        }


        #region T_Kintai
        /// <summary>
        /// 指定された日付とドライバーIDに基づいて勤怠リストを取得します。
        /// </summary>
        /// <param name="day">日付</param>
        /// <param name="DriverID">ドライバーID</param>
        /// <returns>勤怠リスト</returns>
        [HttpGet("KintaiList")]
        public async Task<IList<T_Kintai>> KintaiList(string day, int DriverID = 0)
        {
            try
            {
                if (day == null) { return null; }
                DateOnly date = DateOnly.Parse(day);
                IQueryable<T_Kintai> query = _context.T_Kintais.Where(m => m.Day == date);
                if (DriverID > 0) query = query.Where(m => m.Driver_ID == DriverID);
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
        }
        #endregion T_Kintai

        #region T_Leave_Summary
        /// <summary>
        /// 指定された会社IDとドライバーIDに基づいて休暇サマリーリストを取得します。
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="DriverID">ドライバーID</param>
        /// <returns>休暇サマリーリスト</returns>
        [HttpGet("KintaiLeaveSummaryList")]
        public async Task<IEnumerable<T_Leave_Summary>> KintaiLeaveSummaryList(int CompanyID, int DriverID = 0)
        {
            try
            {
                if (CompanyID == 0) { return null; }
                //var builder = _contextKintai.T_Leave_Summaries.Where(m => m.Company_ID == CompanyID);
                IQueryable<T_Leave_Summary> builder = _contextKintai.T_Leave_Summaries.Where(m => 1 == 1);

                if (DriverID != 0)
                {
                    builder = builder.Where(m => m.乗務員CD == DriverID);    ///m.Driver_ID == DriverID
                }
                return await builder.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
        }
        #endregion T_Leave_Summary

        #region T_Kintai_Commit
        /// <summary>
        /// 指定された会社ID、日付範囲、ドライバーIDに基づいて勤怠コミットリストを取得します。
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="dateFrom">開始日</param>
        /// <param name="dateTo">終了日</param>
        /// <param name="DriverID">ドライバーID</param>
        /// <returns>勤怠コミットリスト</returns>
        [HttpGet("KintaiCommitList")]
        public async Task<IEnumerable<T_KINTAI_COMMIT>> KintaiCommitList(int CompanyID, string dateFrom, string dateTo, int DriverID = 0)
        {
            try
            {
                if (CompanyID == 0) { return null; }
                //var builder = _contextKintai.T_KINTAI_COMMITs.Include(t => t.休暇区分).Where(m => 1 == 1);    ///  m.Company_ID == CompanyID
                IQueryable<T_KINTAI_COMMIT> builder = _contextKintai.T_KINTAI_COMMITs.Where(m => 1 == 1);    ///  m.Company_ID == CompanyID

                if (DriverID != 0)
                {
                    builder = builder.Where(m => m.乗務員CD == DriverID);    //// m.Driver_ID == DriverID
                }

                builder = builder.Where(m => DateOnly.Parse(dateFrom) <= m.勤怠日 && DateOnly.Parse(dateTo) >= m.勤怠日);

                List<T_KINTAI_COMMIT> result = await builder.ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
        }
        #endregion T_Kintai_Commit

        #region T_KINTAI_COMMIT_NON_CREW
        /// <summary>
        /// 指定された会社ID、日付範囲、ドライバーIDに基づいて乗務外勤怠情報を取得します。
        /// </summary>
        /// <param name="CompanyId">会社ID</param>
        /// <param name="dateFrom">開始日</param>
        /// <param name="dateTo">終了日</param>
        /// <param name="driverId">ドライバーID</param>
        /// <returns>乗務外勤怠情報リスト</returns>
        [HttpGet("KintaiCommitNonCrewList")]
        public async Task<IActionResult> KintaiCommitNonCrewList(int CompanyId, string dateFrom, string dateTo, int driverId = 0)
        {
            try
            {
                if (CompanyId == 0 || dateFrom == null || dateTo == null) { throw new Exception("パラメーターエラー：CompanyID,dateFrom,dateTo"); }

                IQueryable<T_KINTAI_COMMIT_NON_CREW> query = _contextKintai.T_KINTAI_COMMIT_NON_CREWs;
                if (driverId > 0) query = query.Where(m => m.乗務員CD == driverId);
                if (dateFrom != null) query = query.Where(m => m.勤怠日 >= DateTime.Parse(dateFrom));
                if (dateTo != null) query = query.Where(m => m.勤怠日 <= DateTime.Parse(dateTo));
                List<T_KINTAI_COMMIT_NON_CREW> resultVal = await query.ToListAsync();
                return new OkObjectResult(resultVal);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }

        }
        #endregion T_KINTAI_COMMIT_NON_CREW

        #region M_LEAVE
        /// <summary>
        /// 休暇リストを取得します。
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <returns>休暇リスト</returns>
        [HttpGet("LeaveList")]
        public async Task<IActionResult> LeaveList(int CompanyID)
        {
            try
            {
                IEnumerable<M_LEAVE> resultVal = await _contextKintai.M_LEAVEs.ToListAsync();
                return new OkObjectResult(resultVal);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }
        }
        #endregion M_LEAVE

        #region M_LEAVE_REASON
        /// <summary>
        /// 休暇理由リストを取得します。
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <returns>休暇理由リスト</returns>
        [HttpGet("LeaveReasonList")]
        public async Task<IActionResult> LeaveReasonList(int CompanyID)
        {
            try
            {
                IEnumerable<M_LEAVE_REASON> res = await _contextKintai.M_LEAVE_REASONs.ToListAsync();
                return new OkObjectResult(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally { }
        }
        #endregion M_LEAVE_REASON

        #region M_Holiday
        /// <summary>
        /// 指定された日付範囲と会社IDに基づいて休日リストを取得します。
        /// </summary>
        /// <param name="dateFrom">開始日</param>
        /// <param name="dateTo">終了日</param>
        /// <param name="CompanyID">会社ID</param>
        /// <returns>休日リスト</returns>
        [HttpGet("GetHolidayList")]
        public async Task<IEnumerable<M_Holiday>> GetHolidayList(string dateFrom, string dateTo, int CompanyID)
        {
            try
            {
                if (CompanyID == 0) { return null; }

                IQueryable<M_Holiday> queryBuilder = null;

                queryBuilder = _contextKintai.M_Holidays.Where(m => 1 == 1);

                if (dateFrom != null && dateTo != null)
                {
                    queryBuilder = queryBuilder.Where(m => m.Day >= DateOnly.Parse(dateFrom) && m.Day <= DateOnly.Parse(dateTo));
                }

                return await queryBuilder.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 休暇設定画面の情報を取得します。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="date">日付</param>
        /// <param name="employeeNumber">従業員番号</param>
        /// <returns>休暇設定画面の情報</returns>
        [HttpGet("GetAttendanceModalViewModel")]
        public async Task<HaisyaDataModel.AttendanceModalViewModel> GetAttendanceModalViewModel(int userId, DateOnly date, int employeeNumber)
        {
            try
            {
                HaisyaDataModel.AttendanceModalViewModel attendanceData = await _attendanceService.GetAttendanceDataAsync(userId, date, employeeNumber);
                return attendanceData;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
        }
        #endregion M_Holiday

        #region V_TOTAL_WORKING_TIME
        /// <summary>
        /// 月次の乗務員毎の走行距離、総労働時間を取得します。
        /// </summary>
        /// <param name="CompanyId">会社ID</param>
        /// <param name="nengetu">年月</param>
        /// <param name="driverId">ドライバーID</param>
        /// <returns>総労働時間リスト</returns>
        [HttpGet("GetKintaiTotalWorkingTimeList")]
        public async Task<IEnumerable<V_TOTAL_WORKING_TIME>> GetKintaiTotalWorkingTimeList(int CompanyId, string nengetu, int driverId = 0)
        {
            try
            {
                if (CompanyId == 0) { return null; }

                IEnumerable<V_TOTAL_WORKING_TIME> resultData = await _contextKintai.V_TOTAL_WORKING_TIMEs.FromSqlRaw("EXECUTE [dbo].[PROC_V_TOTAL_WORKING_TIME] " +
                             "@COMPANY_ID = {0}, @NENGETSU = {1}, @DRIVER_ID = {2}", CompanyId, nengetu.Replace("/", "-"), driverId).AsNoTracking().ToListAsync();

                return resultData;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
        }
        #endregion V_TOTAL_WORKING_TIME


        //#region V_TOTAL_WORKING_TIME
        ///// <summary>
        ///// 月次の乗務員毎の走行距離、総労働時間を取得します。
        ///// </summary>
        ///// <param name="CompanyId">会社ID</param>
        ///// <param name="nengetu">年月</param>
        ///// <param name="driverId">ドライバーID</param>
        ///// <returns>総労働時間リスト</returns>
        //[HttpGet("GetKintaiTotalWorkingTimeList")]
        //public async Task<string> GetKintaiTotalWorkingTimeList(int CompanyId, string nengetu, int driverId = 0)
        //{
        //    try
        //    {
        //        if (CompanyId == 0) { return null; }

        //        IEnumerable<V_TOTAL_WORKING_TIME> resultData = await _contextKintai.V_TOTAL_WORKING_TIMEs.FromSqlRaw("EXECUTE [dbo].[PROC_V_TOTAL_WORKING_TIME] " +
        //                     "@COMPANY_ID = {0}, @NENGETSU = {1}, @DRIVER_ID = {2}", CompanyId, nengetu, driverId).AsNoTracking().ToListAsync();

        //        return "成功";
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Exception: " + ex.Message);
        //        Response.StatusCode = StatusCodes.Status400BadRequest;
        //        return ex.Message;
        //    }
        //}
        //#endregion V_TOTAL_WORKING_TIME

        #region PostRegisterHoliday
        /// <summary>
        /// ユーザーの休暇を登録します。
        /// </summary>
        /// <returns>登録結果</returns>
        [HttpPost("PostRegisterAttendanceModal")]
        public async Task<Dto.MsterDataCommonResultValDto> PostRegisterAttendanceModal()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                HaisyaDataModel.AttendanceModalViewModel dataDto = GetMultipartFormDataContentData<HaisyaDataModel.AttendanceModalViewModel>("name");

                await _attendanceService.PostRegisterHoliday(dataDto);
                resultVal.RetrunFlg = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                resultVal.ErrrMessage = ex.Message;
            }
            finally
            {

            }
            return resultVal;
        }
        #endregion PostRegisterHoliday
    }

    /// <summary>
    /// 休日モデル
    /// </summary>
    public class HolidayModel
    {
        /// <summary>
        /// 日付
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// ユーザーID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// ドライバー名
        /// </summary>
        public string DriverName { get; set; }
        /// <summary>
        /// 車両番号
        /// </summary>
        public string CarNumber { get; set; }
        /// <summary>
        /// 車両タイプ
        /// </summary>
        public string CarType { get; set; }
        /// <summary>
        /// 休日設定
        /// </summary>
        public string HolidaySetting { get; set; }
        /// <summary>
        /// 休日理由
        /// </summary>
        public string HolidayReason { get; set; }
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks { get; set; }
    }
}
