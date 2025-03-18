using System;
using System.Threading.Tasks;
using WebApplication.Data.Kintai;
using WebApplication.Model;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        /// <summary>
        /// 指定された日付に対する休暇の設定、理由、および備考を登録または更新します。
        /// </summary>
        /// <param name="model">休暇設定画面モデル</param>
        /// <returns>非同期操作を表すタスク。操作が成功した場合はtrueを返します。</returns>
        /// <remarks>
        /// このメソッドは、指定された日付とユーザーIDに基づいてドライバーIDを取得し、該当日の勤怠情報を取得します。
        /// その後、休暇設定や理由、備考をデータベースに登録または更新します。
        /// </remarks>
        public async Task<bool> PostRegisterHoliday(HaisyaDataModel.AttendanceModalViewModel model)
        {
            // 引数で受け取った日付を、データベースで扱うのに適した形式（00:00:00の時刻）に変換
            DateTime databaseCompliantDateTime = new DateTime(model.Date.Year, model.Date.Month, model.Date.Day, 0, 0, 0);
            bool result = await _attendanceRepository.PostRegisterHoliday(model, databaseCompliantDateTime);

            return result;
        }

        /// <summary>
        /// 指定されたユーザーIDと日付に対する勤怠データを取得します。
        /// </summary>
        /// <param name="driverId">ドライバーのID。</param>
        /// <param name="date">勤怠データを取得する日付。</param>
        /// <param name="employeeNumber">従業員番号。</param>
        /// <returns>Dto.AttendanceModalViewModelオブジェクトを含むTask。</returns>
        /// <remarks>
        /// このメソッドは、指定されたユーザーIDに対応するドライバーIDを取得し、
        /// ドライバーの名前、車両の番号、車種、車両型、休暇設定、休暇理由、および備考を取得して、
        /// それらのデータを含むDto.AttendanceModalViewModelオブジェクトを返します。
        /// </remarks>
        public async Task<HaisyaDataModel.AttendanceModalViewModel> GetAttendanceDataAsync(int driverId, DateTime date, int employeeNumber)
        {
            // 引数で受け取った日付を、データベースで扱うのに適した形式（00:00:00の時刻）に変換
            DateTime databaseCompliantDateTime = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);

            //V_CompanyDriver
            Data.V_CompanyDriver companyDriver = await _attendanceRepository.GetV_CompanyDriver(driverId);

            // ドライバーIDと変換した日付で、勤怠のコミット状態を取得
            T_KINTAI_COMMIT kintaiCommit = await _attendanceRepository.GetKintaiCommit(employeeNumber, databaseCompliantDateTime);

            // 休暇日かどうか
            M_Holiday holiday = await _attendanceRepository.GetHoliday(date);

            //一覧用備考の取得
            string remark = await _attendanceRepository.GetRemarkAsync(driverId, date);

            int weekday = (int)date.DayOfWeek;

            bool editFlg = false;
            if (kintaiCommit == null) editFlg = true;
            if (kintaiCommit != null && kintaiCommit.勤務区分 == 2 && kintaiCommit.確定区分 == 2) editFlg = true;
            if (kintaiCommit != null && kintaiCommit.勤務区分 == 0) editFlg = true;
            if (weekday== 0 || weekday == 6) editFlg = false;
            if (holiday != null) editFlg = false;

            return new HaisyaDataModel.AttendanceModalViewModel
            {
                KintaiCommit = kintaiCommit,
                CompanyDriver = companyDriver,
                Remark = remark,
                EditEnabledForKintai = editFlg,
                Date = date,
            };
        }
    }
    public interface IAttendanceService
    {
        /// <summary>
        /// 指定された日付に対する休暇の設定、理由、および備考を登録または更新します。
        /// </summary>
        /// <param name="model">休暇設定画面モデル</param>
        /// <returns>非同期操作を表すタスク。操作が成功した場合はtrueを返します。</returns>
        Task<bool> PostRegisterHoliday(HaisyaDataModel.AttendanceModalViewModel model);

        /// <summary>
        /// 指定されたユーザーIDと日付に対する勤怠データを取得します。
        /// </summary>
        /// <param name="driverId">ドライバーのID。</param>
        /// <param name="date">勤怠データを取得する日付。</param>
        /// <param name="employeeNumber">従業員番号。</param>
        /// <returns>Dto.AttendanceModalViewModelオブジェクトを含むTask。</returns>
        Task<HaisyaDataModel.AttendanceModalViewModel> GetAttendanceDataAsync(int driverId, DateTime date, int employeeNumber);
    }
}