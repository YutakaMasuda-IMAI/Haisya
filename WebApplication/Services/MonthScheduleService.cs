//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using WebApplication.Data;
//using WebApplication.Data.Kintai;
//using WebApplication.Model;
//using WebApplication.Repositories;

//namespace WebApplication.Services
//{
//    public class MonthScheduleService
//    {
//        private readonly IMonthScheduleRepository _monthScheduleRepository;

//        public MonthScheduleService(IMonthScheduleRepository monthScheduleRepository)
//        {
//            _monthScheduleRepository = monthScheduleRepository;
//        }

//        / <summary>
//        / 指定されたユーザーIDと日付に基づいて、月間スケジュールデータを取得します。
//        / </summary>
//        / <param name = "userId" > ユーザーのID。</param>
//        / <param name = "date" > スケジュールを取得する対象月の日付。</param>
//        / <returns>
//        / 月間スケジュールデータを格納したDto.MonthScheduleModelViewModelオブジェクトを表すTask。
//        / </returns>
//        / <remarks>
//        / このメソッドは、指定されたユーザーの月間スケジュールデータを取得し、
//        / ドライバー名、支店名、今月の残り拘束時間、対象月の拘束時間合計、労働時間合計、
//        / 残業時間合計、公休労働時間合計、法定休労働時間合計、深夜労働時間合計、走行距離合計、
//        / そしてカレンダーに表示する案件データ、休暇データ、乗務員なしデータを含むオブジェクトを返します。
//        / </remarks>
//        public async Task<Dto.MonthScheduleModelViewModel> GetMonthScheduleDataAsync(int userId, DateTime date)
//        {
//            //ドライバー名の取得
//            var driverName = await _monthScheduleRepository.GetDriverNameAsync(userId);
//            //支店名の取得の取得
//            var driverBranch = await _monthScheduleRepository.GetDriverBranchAsync(userId);
//            //今月残拘束時間の取得
//            var remainingMonthlyWorkingTime = await _monthScheduleRepository.GetRemainingMonthlyWorkingTimeAsync(userId, date);
//            //対象月の拘束時間合計の取得
//            var totalWorkingTime = await _monthScheduleRepository.GetTotalWorkingTimeAsync(userId, date);
//            //対象月の総労働時間合計
//            var totalLaborTime = await _monthScheduleRepository.GetTotalLaborTimeAsync(userId, date);
//            //対象月の残業時間合計
//            var totalOvertime = await _monthScheduleRepository.GetTotalOvertimeAsync(userId, date);
//            //対象月の公休労働時間の合計
//            var totalPublicHolidayWorkingTime = await _monthScheduleRepository.GetTotalPublicHolidayWorkingTimeAsync(userId, date);
//            //対象月の法定休労働時間の合計
//            var totalLegalHolidayWorkingTime = await _monthScheduleRepository.GetTotalLegalHolidayWorkingTimeAsync(userId, date);
//            //対象月の実深夜労働時間の合計
//            var totalLateNightWorkingTime = await _monthScheduleRepository.GetTotalLateNightWorkingTimeAsync(userId, date);
//            //対象月の走行距離の合計
//            var totalTravelDistance = await _monthScheduleRepository.GetTotalTravelDistanceAsync(userId, date);
//            カレンダーに表示する情報の取得
//           var ankenData = _monthScheduleRepository.GetAnkenData(userId, date);

//            var kyukaData = _monthScheduleRepository.GetKyukaData(userId, date);

//            var nocrewData = _monthScheduleRepository.GetNocrewData(userId, date);


//            return new Dto.MonthScheduleModelViewModel
//            {
//                DriverName = driverName,
//                DriverBranch = driverBranch,
//                RemainingMonthlyWorkingTime = remainingMonthlyWorkingTime,
//                TotalWorkingTime = totalWorkingTime,
//                TotalLaborTime = totalLaborTime,
//                TotalOvertime = totalOvertime,
//                TotalPublicHolidayWorkingTime = totalPublicHolidayWorkingTime,
//                TotalLegalHolidayWorkingTime = totalLegalHolidayWorkingTime,
//                TotalLateNightWorkingTime = totalLateNightWorkingTime,
//                TotalTravelDistance = totalTravelDistance,
//                AnkenData = ankenData,
//                KyukaData = kyukaData,
//                NocrewData = nocrewData,
//            };
//        }
//    }

//    public interface IMonthScheduleService
//    {
//        Task<Dto.MonthScheduleModelViewModel> GetMonthScheduleDataAsync(int userId, DateTime date);
//    }
//}