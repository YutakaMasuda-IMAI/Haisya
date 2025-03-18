//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using WebApplication.Data;
//using WebApplication.Data.Kintai;
//using System.Collections.Generic;
//using Newtonsoft.Json;
//using System.Globalization;
//using Microsoft.Extensions.Logging;

//namespace WebApplication.Repositories
//{
//    public class MonthScheduleRepository 
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly ApplicationDbContextKintai _contextKintai;

//        private readonly ILogger<MonthScheduleRepository> _logger;

//        public MonthScheduleRepository(ApplicationDbContext context, ApplicationDbContextKintai contextKintai, ILogger<MonthScheduleRepository> logger)
//        {
//            _context = context;
//            _contextKintai = contextKintai;
//            _logger = logger;
//        }

//        ///// <summary>
//        ///// ドライバー名の取得
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <returns>driverName</returns>
//        //public async Task<string> GetDriverNameAsync(int userId)
//        //{
//        //    var driverName = await _context.M_CompanyDrivers
//        //        .Where(driver => driver.Driver_ID == userId)
//        //        .Select(driver => driver.Display_Name)
//        //        .FirstOrDefaultAsync();

//        //    return driverName;
//        //}

//        ///// <summary>
//        ///// 支店名の取得
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <returns>driverBranch</returns>
//        //public async Task<string> GetDriverBranchAsync(int userId)
//        //{
//        //    var driverBranch = await _context.M_CompanyDrivers
//        //        .Join(_context.M_CompanyBranches,
//        //            driver => driver.Branch_ID, // ドライバーの支店IDを支店テーブルの支店IDと結合します。
//        //            branch => branch.Branch_ID,
//        //            (driver, branch) => new { Driver = driver, Branch = branch }) // 結合結果からドライバーと支店の情報を選択します。
//        //        .Where(x => x.Driver.Driver_ID == userId) // 指定されたユーザーIDのドライバーをフィルタリングします。
//        //        .Select(x => x.Branch.Branch_Name) // 支店名を選択します。
//        //        .FirstOrDefaultAsync(); // 最初の結果を非同期に取得します。

//        //    return driverBranch;
//        //}

//        /// <summary>
//        /// 今月残拘束時間の取得（@TODO: 仕様が決まり次第実装）
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <param name="date"></param>
//        /// <returns>テスト用の固定値</returns>
//        public async Task<string> GetRemainingMonthlyWorkingTimeAsync(int userId, DateTime date)
//        {
//            //     var monthlyWorkingTime = await _contextKintai.T_KINTAI_BASEs
//            //         .Where(kintai => kintai.Driver_ID == userId && kintai.勤怠日 == new DateTime(date.Year, date.Month, 1))
//            //         .Select(kintai => kintai.システム月次拘束時間)
//            //         .FirstOrDefaultAsync();

//            //     var workingTimeUntilPreviousDay = await _contextKintai.T_KINTAI_BASEs
//            //         .Where(kintai => kintai.Driver_ID == userId && kintai.勤怠日 < date)
//            //         .Select(kintai => kintai.拘束時間)
//            //         .SumAsync();

//            //     var remainingWorkingTime = monthlyWorkingTime - workingTimeUntilPreviousDay;

//            return "10:00";
//        }

//        /// <summary>
//        /// 対象月の拘束時間合計の取得（@TODO: 仕様が決まり次第実装）
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <param name="date"></param>
//        /// <returns>>テスト用の固定値</returns>
//        public async Task<string> GetTotalWorkingTimeAsync(int userId, DateTime date)
//        {
//            //         var totalWorkingTime = await _contextKintai.T_KINTAI_BASEs
//            // .Where(kintai => kintai.乗務員CD == userId && kintai.勤怠日.Value.Year == date.Year && kintai.勤怠日.Value.Month == date.Month)
//            // .Select(kintai => kintai.総労働時間.HasValue ? kintai.拘束時間.Value.TimeOfDay : TimeSpan.Zero)
//            // .ToListAsync();

//            //         var sum = totalWorkingTime.Aggregate(TimeSpan.Zero, (acc, time) => acc + time);

//            //         string formattedTime = sum.ToString(@"hh\:mm");

//            //         return formattedTime;
//            return "10:00";
//        }

//        ///// <summary>
//        ///// 対象月の総労働時間合計
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <param name="date"></param>
//        ///// <returns>formattedTime</returns>
//        //public async Task<string> GetTotalLaborTimeAsync(int userId, DateTime date)
//        //{
//        //    // 指定されたユーザーIDと年月に基づいて、労働時間データを取得します。
//        //    var totalLaborTime = await _contextKintai.T_KINTAI_COMMITs
//        //        .Where(kintai => kintai.乗務員CD == userId && kintai.勤怠日.Value.Year == date.Year && kintai.勤怠日.Value.Month == date.Month)
//        //        .Select(kintai => kintai.総労働時間.HasValue ? kintai.総労働時間.Value.TimeOfDay : TimeSpan.Zero)
//        //        .ToListAsync();
//        //    // 労働時間の合計を計算します。
//        //    var sum = totalLaborTime.Aggregate(TimeSpan.Zero, (acc, time) => acc + time);

//        //    // 合計時間を "hh:mm" 形式でフォーマットします。
//        //    string formattedTime = sum.ToString(@"hh\:mm");

//        //    return formattedTime;
//        //}

//        ///// <summary>
//        ///// 対象月の残業時間合計
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <param name="date"></param>
//        ///// <returns></returns>
//        //public async Task<string> GetTotalOvertimeAsync(int userId, DateTime date)
//        //{
//        //    // 指定されたユーザーIDと年月に基づいて、残業時間データを取得します。
//        //    var totalLaborTime = await _contextKintai.T_KINTAI_COMMITs
//        //    .Where(commit => commit.乗務員CD == userId && commit.勤怠日.Year == date.Year && commit.勤怠日.Month == date.Month)
//        //    .Select(commit => commit.残業時間.HasValue ? commit.残業時間.Value.TimeOfDay : TimeSpan.Zero)
//        //    .ToListAsync();
//        //    // 残業時間の合計を計算します。
//        //    var sum = totalLaborTime.Aggregate(TimeSpan.Zero, (acc, time) => acc + time);
//        //    // 合計時間を "hh:mm" 形式でフォーマットします。
//        //    string formattedTime = sum.ToString(@"hh\:mm");

//        //    return formattedTime;
//        //}

//        ///// <summary>
//        ///// 対象月の公休労働時間の合計
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <param name="date"></param>
//        ///// <returns></returns>
//        //public async Task<string> GetTotalPublicHolidayWorkingTimeAsync(int userId, DateTime date)
//        //{
//        //    // 指定されたユーザーIDと年月に基づいて、公休労働時間データを取得します。
//        //    var totalLaborTime = await _contextKintai.T_KINTAI_COMMITs
//        //    .Where(commit => commit.乗務員CD == userId && commit.勤怠日.Year == date.Year && commit.勤怠日.Month == date.Month)
//        //    .Select(commit => commit.公休労働時間.HasValue ? commit.公休労働時間.Value.TimeOfDay : TimeSpan.Zero)
//        //    .ToListAsync();
//        //    // 公休労働時間の合計を計算します。
//        //    var sum = totalLaborTime.Aggregate(TimeSpan.Zero, (acc, time) => acc + time);
//        //    // 合計時間を "hh:mm" 形式でフォーマットします。
//        //    string formattedTime = sum.ToString(@"hh\:mm");

//        //    return formattedTime;
//        //}

//        ///// <summary>
//        ///// 対象月の法定休労働時間の合計
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <param name="date"></param>
//        ///// <returns></returns>
//        //public async Task<string> GetTotalLegalHolidayWorkingTimeAsync(int userId, DateTime date)
//        //{
//        //    // 指定されたユーザーIDと年月に基づいて、法定休労働時間データを取得します。
//        //    var totalLaborTime = await _contextKintai.T_KINTAI_COMMITs
//        //    .Where(commit => commit.乗務員CD == userId && commit.勤怠日.Year == date.Year && commit.勤怠日.Month == date.Month)
//        //    .Select(commit => commit.法定休労働時間.HasValue ? commit.法定休労働時間.Value.TimeOfDay : TimeSpan.Zero)
//        //    .ToListAsync();

//        //    // 法定休労働時間の合計を計算します。
//        //    var sum = totalLaborTime.Aggregate(TimeSpan.Zero, (acc, time) => acc + time);

//        //    // 合計時間を "hh:mm" 形式でフォーマットします。
//        //    string formattedTime = sum.ToString(@"hh\:mm");

//        //    return formattedTime;
//        //}

//        ///// <summary>
//        ///// 対象月の実深夜労働時間の合計
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <param name="date"></param>
//        ///// <returns></returns>
//        //public async Task<string> GetTotalLateNightWorkingTimeAsync(int userId, DateTime date)
//        //{
//        //    // 指定されたユーザーIDと年月に基づいて、実深夜労働時間データを取得します。
//        //    var totalLaborTime = await _contextKintai.T_KINTAI_BASEs
//        //        .Where(kintai => kintai.乗務員CD == userId && kintai.勤怠日.Value.Year == date.Year && kintai.勤怠日.Value.Month == date.Month)
//        //        .Select(kintai => kintai.実深夜労働時間.HasValue ? kintai.実深夜労働時間.Value.TimeOfDay : TimeSpan.Zero)
//        //        .ToListAsync();
//        //    // 実深夜労働時間の合計を計算します。
//        //    var sum = totalLaborTime.Aggregate(TimeSpan.Zero, (acc, time) => acc + time);
//        //    // 合計時間を "hh:mm" 形式でフォーマットします。
//        //    string formattedTime = sum.ToString(@"hh\:mm");

//        //    return formattedTime;
//        //}

//        ///// <summary>
//        ///// 対象月の走行距離の合計
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <param name="date"></param>
//        ///// <returns></returns>
//        //public async Task<double?> GetTotalTravelDistanceAsync(int userId, DateTime date)
//        //{
//        //    // 指定されたユーザーIDと年月に基づいて、走行距離データを取得し、合計を計算します。
//        //    var totalTravelDistance = await _contextKintai.T_KINTAI_BASEs
//        //        .Where(commit => commit.乗務員CD == userId && commit.勤怠日.Value.Month == date.Month && commit.勤怠日.Value.Year == date.Year)
//        //        .Select(kintai => kintai.走行距離)
//        //        .SumAsync();

//        //    return totalTravelDistance;
//        //}

//        ///// <summary>
//        ///// 有給情報の取得
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <param name="date"></param>
//        ///// <returns></returns>
//        //public string /*GetKyukaData*/(int userId, DateTime date)
//        //{
//        //    // dateの月の範囲を取得
//        //    DateTime startDate = new DateTime(date.Year, date.Month, 1);
//        //    DateTime endDate = startDate.AddMonths(1).AddDays(-1);

//        //    // T_Kintai_Commitからデータを取得
//        //    var kyukaData = _contextKintai.T_KINTAI_COMMITs
//        //        .Where(kintai => kintai.乗務員CD == userId && kintai.勤怠日 >= startDate && kintai.勤怠日 <= endDate && kintai.勤怠区分 == 2)
//        //        .Select(kintai => new
//        //        {
//        //            Day = kintai.勤怠日.Day,
//        //            KyukaKubun = kintai.休暇区分
//        //        })
//        //        .ToList();
//        //    // 日付ごとにグループ化
//        //    var groupedData = kyukaData.GroupBy(kintai => kintai.Day)
//        //                               .ToDictionary(g => g.Key.ToString(), g => g.Select(k => k.KyukaKubun).ToList());

//        //    // 辞書をJSON形式に変換
//        //    var json = "{";
//        //    foreach (var kvp in groupedData)
//        //    {
//        //        json += $"'{kvp.Key}': '{string.Join(",", kvp.Value)}',";
//        //    }
//        //    json = json.TrimEnd(',') + "}";

//        //    return json;
//        //}

//        /// <summary>
//        /// 乗務外情報の取得
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <param name="date"></param>
//        /// <returns></returns>
//        public string GetNocrewData(int userId, DateTime date)
//        {
//            // dateの月の範囲を取得
//            DateTime startDate = new DateTime(date.Year, date.Month, 1);
//            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

//            // T_Kintai_Commitからデータを取得
//            var kyukaData = _contextKintai.T_KINTAI_COMMITs
//                .Where(kintai => kintai.乗務員CD == userId && kintai.勤怠日 >= startDate && kintai.勤怠日 <= endDate && kintai.勤怠区分 != 2)
//                .Select(kintai => new
//                {
//                    Day = kintai.勤怠日.Day,
//                    KyukaKubun = kintai.勤務区分
//                })
//                .ToList();
//            // 日付ごとにグループ化
//            var groupedData = kyukaData.GroupBy(kintai => kintai.Day)
//                                       .ToDictionary(g => g.Key.ToString(), g => g.Select(k => k.KyukaKubun).ToList());

//            // 辞書をJSON形式に変換
//            var json = "{";
//            foreach (var kvp in groupedData)
//            {
//                json += $"'{kvp.Key}': '{string.Join(",", kvp.Value)}',";
//            }
//            json = json.TrimEnd(',') + "}";

//            return json;
//        }

//        /// <summary>
//        /// 案件情報の取得
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <param name="date"></param>
//        /// <returns></returns>
//        public string GetAnkenData(int userId, DateTime date)
//        {
//            // カレンダー表示範囲を計算
//            DateTime startDate = new DateTime(date.Year, date.Month, 1);
//            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
//            // 渡された月内の特定のユーザーの仕事の詳細を取得するクエリを作成
//            var data = _context.T_Haisyas
//                .Join(_context.M_CompanyDrivers,
//                    haisya => haisya.Driver_ID,
//                    companyDriver => companyDriver.Driver_ID,
//                    (haisya, companyDriver) => new { Haisya = haisya, CompanyDriver = companyDriver })
//                .Join(_context.T_Anken_Displays,
//                    hc => hc.Haisya.AnkenDisplay_ID,
//                    ankenDisplay => ankenDisplay.AnkenDisplay_ID,
//                    (hc, ankenDisplay) => new { HaisyaCompanyDriver = hc, AnkenDisplay = ankenDisplay })
//                .Where(joined => joined.HaisyaCompanyDriver.CompanyDriver.Driver_ID == userId &&
//                ((joined.AnkenDisplay.StartDatetime >= startDate && joined.AnkenDisplay.StartDatetime <= endDate) ||
//                (joined.AnkenDisplay.EndDatetime >= startDate && joined.AnkenDisplay.EndDatetime <= endDate) ||
//                (joined.AnkenDisplay.StartDatetime < startDate && joined.AnkenDisplay.EndDatetime > endDate)))
//                .Select(joined =>
//                    new
//                    {
//                        Day = (joined.AnkenDisplay.StartDatetime >= startDate && joined.AnkenDisplay.StartDatetime <= endDate)
//                    ? joined.AnkenDisplay.StartDatetime.Date
//                    : (joined.AnkenDisplay.EndDatetime >= startDate && joined.AnkenDisplay.EndDatetime <= endDate)
//                        ? joined.AnkenDisplay.EndDatetime.Date
//                        : (joined.AnkenDisplay.StartDatetime < startDate && joined.AnkenDisplay.EndDatetime > endDate)
//                            ? endDate
//                            : joined.AnkenDisplay.StartDatetime.Date,
//                        AnkenDisplayId = joined.HaisyaCompanyDriver.Haisya.AnkenDisplay_ID,
//                        StartBuildingName = joined.AnkenDisplay.Start_BuildingName ?? $"{joined.AnkenDisplay.Start_Address2} {joined.AnkenDisplay.Start_Address3}",
//                        EndBuildingName = joined.AnkenDisplay.End_BuildingName ?? $"{joined.AnkenDisplay.End_Address2} {joined.AnkenDisplay.End_Address3}",
//                        Length = (joined.AnkenDisplay.EndDatetime - joined.AnkenDisplay.StartDatetime).Days + 1,
//                        CrossesMultipleMonths = joined.AnkenDisplay.StartDatetime < startDate
//                    })
//                .ToList();
//            // データを日付でグループ化し、グループごとに指定された形式で情報を変換します。
//            var groupedData = data.AsEnumerable()
//                      .GroupBy(d => d.Day)
//                      .ToDictionary(
//                          g => g.Key.Day.ToString(), // グループのキー（各日付）を文字列形式で取得します。
//                          g => g.Select(d => new
//                          {
//                              AnkenRange = $"{d.StartBuildingName}~{d.EndBuildingName}",  // 開始ビル名と終了ビル名を結合して、範囲を表現します。
//                              Length = d.Length, // データの長さを取得します。
//                              CrossesMultipleMonths = d.CrossesMultipleMonths // 複数の月を跨ぐかどうかの情報を取得します。
//                          }).ToArray() // 各グループ内の情報を配列形式で取得します。
//                      );
//            // グループ化されたデータをJSON形式でシリアライズします。
//            var json = JsonConvert.SerializeObject(groupedData);
//            return json;
//        }
//    }
//    public class YourEntityType
//    {
//        public int AnkenValue { get; set; }
//        public string KintaiValue { get; set; }
//    }

//    //    public interface IMonthScheduleRepository
//    //    {
//    //        Task<string> GetDriverNameAsync(int userId);
//    //        Task<string> GetDriverBranchAsync(int userId);
//    //        Task<string> GetRemainingMonthlyWorkingTimeAsync(int userId, DateTime date);
//    //        Task<string> GetTotalWorkingTimeAsync(int userId, DateTime date);
//    //        Task<string> GetTotalLaborTimeAsync(int userId, DateTime date);
//    //        Task<string> GetTotalOvertimeAsync(int userId, DateTime date);
//    //        Task<string> GetTotalPublicHolidayWorkingTimeAsync(int userId, DateTime date);
//    //        Task<string> GetTotalLegalHolidayWorkingTimeAsync(int userId, DateTime date);
//    //        Task<string> GetTotalLateNightWorkingTimeAsync(int userId, DateTime date);
//    //        Task<double?> GetTotalTravelDistanceAsync(int userId, DateTime date);
//    //        string GetKyukaData(int userId, DateTime date);
//    //        string GetNocrewData(int userId, DateTime date);
//    //        string GetAnkenData(int userId, DateTime date);
//    //    }
//}