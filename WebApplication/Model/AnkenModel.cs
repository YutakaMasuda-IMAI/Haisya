using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;

namespace WebApplication.Model
{
    /// <summary>
    /// 案件モデルクラス
    /// </summary>
    public class AnkenModel : BaseModel
    {

        public AnkenModel(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// UpdateAnkenData の返却用
        /// </summary>
        public class UpdateAnkenDataDto
        {
            public T_Anken Anken { get; set; }

            public string ErrrMessage { get; set; }
        }

        /// <summary>
        /// 
        /// ※modelからの返却用
        /// </summary>
        public class DriveRouteListDto
        {
            // ルートの追加料金
            public List<ExtraChargeDto> ExchargeDataList { get; set; }

            public List<DriveRouteListDisplay> DriveRouteListDisplayList { get; set; }

            public string ErrrMessage { get; set; }
        }

        /// <summary>
        /// ルート検索リスト(画面表示用) 
        /// </summary>
        public class DriveRouteListDisplay : Model.DriveListItemEx2
        {
            //　ルート対応表示
            [Display(Name = "ルート")]
            public string RouteTypeDisplay { get; set; }
            //　ルート表示アイコン
            public string RouteTitleStyle { get; set; } = "GrayBlueGradientBrush";
            //　ルート表示アイコンクラス
            public string RouteTypeClass { get; set; }
            //　合計時間
            [Display(Name = "所要時間")]
            public string TotalTime { get; set; }
            //　合計距離
            [Display(Name = "距離(km)")]
            public double TotalDistance { get; set; }
            //　有料料金合計
            [Display(Name = "有料道路")]
            public double Totaltoll { get; set; }
            //　休息時間
            [Display(Name = "休息時間")]
            public string RestTimeDisplay { get; set; }
            //　休憩時間
            [Display(Name = "休憩時間")]
            public string BreakTimeDisplay { get; set; }

            public int CalcRoudoHour { get; set; }
        }

        /// <summary>
        /// 追加料金DTOクラス
        /// </summary>
        public class ExtraChargeDto
        {
            public int KoumokuId { get; set; }
            //　項目名
            public string Title { get; set; }
            //　時間
            public double WorkTime { get; set; }
            //　原価
            public double GrossAmount { get; set; }
            //　標準
            public double StdAmount { get; set; }

        }

        /// <summary>
        /// 案件追加料金DTOクラス
        /// </summary>
        public class AnkenExchargeDto : T_Anken_Excharge
        {
            [StringLength(20)]
            public string SIZE { get; set; }
            [Required]
            [StringLength(20)]
            public string Komoku_Key { get; set; }
            public int SortOrder { get; set; }
            [Required]
            [StringLength(50)]
            public string Komoku_Name { get; set; }
            [StringLength(50)]
            public string Komoku_Name_abbr { get; set; }
        }

        /// <summary>
        /// ドライブルートデータに対して、標準運賃及び見積原価計算を追加する
        /// </summary>
        /// <param name="driveListtEx">ドライブリスト拡張</param>
        /// <param name="area">エリア</param>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="syasyu">車種</param>
        /// <param name="kata">型</param>
        /// <param name="syasyuSize">車種サイズ</param>
        /// <param name="TsumiTime">積み時間</param>
        /// <param name="OroshiTime">卸し時間</param>
        /// <param name="DriverGrossCalc">乗務員原価計算区分</param>
        /// <param name="t_Anken_Excharges">案件追加料金リスト</param>
        /// <returns>トライブルート結果リスト</returns>
        public async Task<DriveRouteListDto> GetDriveRouteListExAsync(Model.DriveListEx driveListtEx,
                                                        int area,
                                                        int CompanyID,
                                                        string syasyu = null,
                                                        string kata = null,
                                                        string syasyuSize = null,
                                                        string TsumiTime = "00:00",
                                                        string OroshiTime = "00:00",
                                                        int DriverGrossCalc = 0,
                                                        List<AnkenModel.AnkenExchargeDto> t_Anken_Excharges = null
                                                        )
        {

            ////////////// マスターデータの取得//////////////////////////////////////////////
            ///運賃エリア
            M_Area m_Area = await _context.M_Areas.FirstOrDefaultAsync(m => m.Area_ID == area);
            // 原価（人件費単価）
            List<Data.M_PersonnelExpense> m_PersonnelExpenseList = await _context.M_PersonnelExpenses.Where(m => m.Company_ID == CompanyID && m.Syasyu == syasyu && m.Kata == kata).ToListAsync();
            if (m_PersonnelExpenseList == null || m_PersonnelExpenseList.Count == 0) { throw new Exception("人件費単価マスタに対象車種のデータがありません。"); }
            Data.M_PersonnelExpense m_PersonnelExpense = m_PersonnelExpenseList.First();
            // 標準運賃　荷待ち単価
            Data.M_DefaultMoney_WaitTimeForArea m_DefaultMoney_WaitTime = await _context.M_DefaultMoney_WaitTimeForAreas.FirstOrDefaultAsync(m => m.SyasyuSize == syasyuSize && m.Area == m_Area.Area);
            if (m_DefaultMoney_WaitTime == null) { throw new Exception("標準運賃（荷待ち単価）マスタに対象車種のデータがありません。"); }
            // 標準運賃　距離単価
            List<Data.M_DefaultMoney> m_DefaultMoney = await _context.M_DefaultMoneys.Where(m => m.Area == m_Area.Area && m.SyasyuSize == syasyuSize).ToListAsync();
            if (m_DefaultMoney.Count == 0) { throw new Exception("標準運賃（距離単価）マスタに対象車種のデータがありません。"); }
            // 燃料単価
            Data.M_FuelCost m_FuelCost = await _context.M_FuelCosts.OrderByDescending(m => m.FromDate).FirstOrDefaultAsync(m => m.Company_ID == CompanyID && m.FromDate <= DateOnly.Parse("2999/01/01"));
            if (m_FuelCost == null) { throw new Exception("燃料単価マスタにデータがありません。"); }
            // 原価　車輌距離単価
            List<Data.M_SyaryoCost> m_SyaryoCostList = await _context.M_SyaryoCosts.Where(m => m.Company_ID == CompanyID && m.Syasyu == syasyu && m.Kata == kata).ToListAsync();
            if (m_SyaryoCostList.Count  == 0) { throw new Exception("原価（車輌距離単価）マスタに対象車種のデータがありません。"); }


            // 返却用クラスの初期化
            DriveRouteListDto driveRouteListData = new()
            {
                ExchargeDataList = new(),
                DriveRouteListDisplayList = new(),
            };


            ////////////////// 追加料金計算///////////////////////////////////
            double ExtraCharge_Standard = 0;
            double ExtraCharge_Gross = 0;
            double ExtraCharge_GrossForLuggage = 0;

            //積み時間
            if (!"00:00".Equals(TsumiTime))
            {
                DateTime dtData = DateTime.Parse(TsumiTime);
                TimeSpan ts1 = new TimeSpan(dtData.Hour, dtData.Minute, 0);
                double totalHours = ts1.TotalHours - 0.5;

                ExtraChargeDto exchargeData = new();
                exchargeData.KoumokuId = 1;
                exchargeData.Title = "積み時間";
                exchargeData.WorkTime = totalHours;
                if (m_PersonnelExpense != null)
                {
                    double baseCost = Math.Round(totalHours * (double)m_PersonnelExpense.Base);
                    exchargeData.GrossAmount = baseCost + Math.Round(baseCost * ((double)m_PersonnelExpense.BenefitsCosts / 100))
                                                        + Math.Round(baseCost * ((double)m_PersonnelExpense.IndirectCosts / 100));

                    /////////デモ/////////
                    //exchargeData.GrossAmount = 99999;
                    /////////デモ/////////
                    ExtraCharge_GrossForLuggage += exchargeData.GrossAmount;
                }
                if (m_DefaultMoney_WaitTime != null)
                {
                    exchargeData.StdAmount = totalHours * (double)m_DefaultMoney_WaitTime.Amount; ExtraCharge_Standard += exchargeData.StdAmount;
                }
                driveRouteListData.ExchargeDataList.Add(exchargeData);
            }
            //卸し時間
            if (!"00:00".Equals(OroshiTime))
            {
                DateTime dtData = DateTime.Parse(OroshiTime);
                TimeSpan ts1 = new TimeSpan(dtData.Hour, dtData.Minute, 0);
                double totalHours = ts1.TotalHours - 0.5;

                ExtraChargeDto exchargeData = new();
                exchargeData.KoumokuId = 2;
                exchargeData.Title = "卸し時間";
                exchargeData.WorkTime = totalHours;
                if (m_PersonnelExpense != null)
                {
                    double baseCost = Math.Round(totalHours * (double)m_PersonnelExpense.Base);
                    exchargeData.GrossAmount = baseCost + Math.Round(baseCost * ((double)m_PersonnelExpense.BenefitsCosts / 100))
                                                        + Math.Round(baseCost * ((double)m_PersonnelExpense.IndirectCosts / 100));
                    /////////デモ/////////
                    //exchargeData.GrossAmount = 99999;
                    /////////デモ/////////
                    ExtraCharge_GrossForLuggage += exchargeData.GrossAmount;
                }
                if (m_DefaultMoney_WaitTime != null) { exchargeData.StdAmount = totalHours * (double)m_DefaultMoney_WaitTime.Amount; ExtraCharge_Standard += exchargeData.StdAmount; }
                driveRouteListData.ExchargeDataList.Add(exchargeData);
            }
            ////追加料金（荷待ち時間）
            //ExtraCharge_Gross += ExtraCharge_GrossForLuggage;


            //追加料金（選択項目）　※休日祝日積み・卸し　※深夜積み・卸し
            int exchargeData_KoumokuId = 3;
            if (t_Anken_Excharges != null && t_Anken_Excharges.Count > 0)
            {
                foreach(var data in t_Anken_Excharges)
                {
                    ExtraChargeDto dto = new() {
                        KoumokuId = exchargeData_KoumokuId,
                        Title = data.Komoku_Name,
                        StdAmount = (double)data.StdExcharge,
                        GrossAmount = (double)data.GrossExcharge,
                    };
                    driveRouteListData.ExchargeDataList.Add(dto);
                    
                    exchargeData_KoumokuId++;
                    ExtraCharge_Standard += dto.StdAmount;
                    ExtraCharge_Gross += dto.GrossAmount;
                }
            }

            //　
            if (driveListtEx.route != null)
            {
                GetDriveRouteListDto(driveRouteListData, driveListtEx.route, m_PersonnelExpense, m_DefaultMoney, m_FuelCost, m_SyaryoCostList,
                                        ExtraCharge_Standard, ExtraCharge_Gross, ExtraCharge_GrossForLuggage, DriverGrossCalc);
            } else
            {
                foreach (Model.DriveListItemEx item in driveListtEx.item)
                {
                    GetDriveRouteListDto(driveRouteListData, item, m_PersonnelExpense, m_DefaultMoney, m_FuelCost, m_SyaryoCostList,
                                        ExtraCharge_Standard, ExtraCharge_Gross, ExtraCharge_GrossForLuggage, DriverGrossCalc);
                }
            }


            return driveRouteListData;

        }

        /// <summary>
        /// 引数のドライブルートデータに対して標準運賃及び見積原価計算を追加する
        /// </summary>
        /// <param name="driveRouteListData">ドライブルートリストDTO</param>
        /// <param name="item">ドライブリストアイテム拡張</param>
        /// <param name="m_PersonnelExpense">人件費</param>
        /// <param name="m_DefaultMoney">標準運賃</param>
        /// <param name="m_FuelCost">燃料費</param>
        /// <param name="m_SyaryoCostList">車輌費リスト</param>
        /// <param name="ExtraCharge_Standard">追加料金（標準）</param>
        /// <param name="ExtraCharge_Gross">追加料金（原価）</param>
        /// <param name="ExtraCharge_GrossForLuggage">追加料金（荷物）</param>
        /// <param name="DriverGrossCalc">乗務員原価計算区分</param>
        private void GetDriveRouteListDto(DriveRouteListDto driveRouteListData,
                                                        Model.DriveListItemEx item,
                                                        Data.M_PersonnelExpense m_PersonnelExpense,
                                                        List<Data.M_DefaultMoney> m_DefaultMoney,
                                                        Data.M_FuelCost m_FuelCost,
                                                        List<Data.M_SyaryoCost> m_SyaryoCostList,
                                                        double ExtraCharge_Standard,
                                                        double ExtraCharge_Gross,
                                                        double ExtraCharge_GrossForLuggage,
                                                        int DriverGrossCalc = 0
                                                        )
        {
            int RecommendTime = 0;

            DriveRouteListDisplay driveRoute = new()
            {
                routeID = item.routeID,
                routeType = item.routeType,
                distance = item.distance,
                toll = item.toll,
                time = item.time,
                invalidFee = item.invalidFee,
                Height = item.Height,
                Width = item.Width,
                Weight = item.Weight,
                Syasyu = item.Syasyu,
                Kata = item.Kata,
                SyasyuSize = item.SyasyuSize,
                Nenpi = item.Nenpi,
                FuelConsume = item.FuelConsume,
                RestTime = item.RestTime,
                TotalDays = item.TotalDays,
                BreakTime = item.BreakTime,
                RoudoTime = item.RoudoTime,
                ZangyoTime = item.ZangyoTime,
                line = new(),
                link = new(),
            };

            if (int.Parse(item.routeType) == 0)
            {
                RecommendTime = item.time;
            }

            driveRoute.line = item.line;
            driveRoute.link = item.link;
            driveRoute.Totaltoll = item.toll;

            // 走行時間
            driveRoute.TotalDistance = item.distance > 0 ? item.distance / 1000 : 0;
            if (item.time > 0)
            {
                TimeSpan span = new TimeSpan(0, item.time, 0);
                int day = int.Parse(span.ToString(@"dd"));
                if (day > 0)
                {
                    int iHour = int.Parse(span.ToString(@"hh")) + (24 * day);
                    driveRoute.TotalTime = string.Format(span.ToString(@"\{\0\}\時mm\分"), iHour);
                }
                else
                {
                    driveRoute.TotalTime = span.ToString(@"hh\時mm\分");
                }
            }

            // 休憩時間
            if (item.BreakTime > 0)
            {
                driveRoute.BreakTimeDisplay = item.BreakTime + "分";
            }

            // 休息時間
            if (item.RestTime > 0)
            {
                driveRoute.RestTimeDisplay = item.RestTime + "分";
            }
            if (item.TotalDays > 1)
            {
                driveRoute.RestTimeDisplay += item.TotalDays.ToString(@"(0日)");
            }








            /////////////////////////////標準運賃計算///////////////////////////////////////
            // 標準運賃計算（距離）
            driveRoute.StdFreight = 0;
            if (item.distance > 0)
            {
                int distance = (int)driveRoute.distance / 1000;
                if (distance > 0)
                {
                    if (m_DefaultMoney != null)
                    {
                        M_DefaultMoney target = null;
                        target = m_DefaultMoney.OrderByDescending(m => m.To_Distance).FirstOrDefault(m => m.From_Distance <= distance && m.To_Distance >= distance);

                        if (target.Interval == 1)
                        {
                            driveRoute.StdFreight = (double)target.Amount;
                        }
                        else
                        {
                            int targetDistance = distance - target.From_Distance;
                            int bai = (int)(targetDistance / target.Interval);
                            driveRoute.StdFreight = (double)target.Amount + bai * (double)target.AdditionAmount;
                        }
                    }
                } else
                {
                    driveRoute.StdFreight = 0;
                }
                
            }


            // 標準運賃計算（時間割増）
            //driveRoute.TimeCharge = 0;
            //if (int.Parse(item.type) > 0 && (RecommendTime + 60) < item.time)
            //{
            //    int sa = item.time - RecommendTime;
            //    TimeSpan span = new TimeSpan(0, sa, 0);
            //    int houerSa = int.Parse(span.ToString(@"hh"));
            //    int daySa = int.Parse(span.ToString(@"dd"));
            //    if (daySa > 0)
            //    {
            //        houerSa += 24 * daySa;
            //    }

            //    List<M_DefaultMoneyOver_Local> list = (List<M_DefaultMoneyOver_Local>)await dataApi.GetDefaultMoneyOverList(syasyuSize, "中国");
            //    M_DefaultMoneyOver_Local data = list.FirstOrDefault();

            //    driveRoute.TimeCharge = (int)data.Money * houerSa;

            //}

            // 標準運賃　追加料金
            driveRoute.StdExcharge = ExtraCharge_Standard;

            // 標準運賃合計
            driveRoute.StdALLFreight = driveRoute.StdFreight + driveRoute.StdExcharge;

            // 請求標準運賃合計
            driveRoute.StdTotalFreight = driveRoute.StdALLFreight + item.toll;

            // 運賃原価
            if (driveRoute.distance > 0)
            {
                //　労働費計算
                driveRoute.GrossAmount = 0;
                driveRoute.GrossAmountForLaborCost = 0;

                if (m_PersonnelExpense != null)
                {
                    //労働費計算用勤務時間
                    int houer = driveRoute.RoudoTime + driveRoute.ZangyoTime;

                    int time = (driveRoute.RoudoTime * 60) + (driveRoute.ZangyoTime * 60);
                        TimeSpan span = new TimeSpan(0, time, 0);
                    int daySa = driveRoute.TotalDays;

                    if (DriverGrossCalc == 0)
                    {
                        //　日計算
                        int sa = houer;
                        if (daySa > 0)
                        {
                            sa = houer % (9 * daySa);
                        }
                        if (sa < 8) { houer += (8 - sa); }
                        }

                    driveRoute.CalcRoudoHour = houer;

                    double baseCost = Math.Round(driveRoute.CalcRoudoHour * (double)m_PersonnelExpense.Base);
                        driveRoute.GrossAmountForLaborCost = baseCost + Math.Round(baseCost * ((double)m_PersonnelExpense.BenefitsCosts / 100))
                                                            + Math.Round(baseCost * ((double)m_PersonnelExpense.IndirectCosts / 100));
                }


                // 燃料費計算
                driveRoute.GrossAmountForFuelCost = 0;
                if (m_FuelCost != null)
                {
                    driveRoute.GrossAmountForFuelCost = Math.Round(driveRoute.FuelConsume * (double)m_FuelCost.FuelAmount);
                }

                // 車輌費
                driveRoute.GrossAmountForSyaryoCost = 0;
                int distance = (int)driveRoute.distance / 1000;
                if (distance > 0)
                {
                    if (m_SyaryoCostList != null)
                    {
                        M_SyaryoCost target = null;
                        target = m_SyaryoCostList.OrderByDescending(m => m.To_Distance).FirstOrDefault(m => m.From_Distance <= distance && m.To_Distance >= distance);

                        if (target.Interval == 1)
                        {
                            driveRoute.GrossAmountForSyaryoCost = (double)target.Amount;
                        }
                        else
                        {
                            int targetDistance = distance - target.From_Distance;
                            int bai = (int)(targetDistance / target.Interval);
                            driveRoute.GrossAmountForSyaryoCost = (double)target.Amount + bai * (double)target.AdditionAmount;
                        }
                    }
                } else
                {
                    driveRoute.GrossAmountForSyaryoCost = 0;
                }


                //　荷物（荷待ち・荷積み・荷卸し）
                driveRoute.GrossAmountForLuggage = ExtraCharge_GrossForLuggage;

                // 運賃原価(その他追加料金）
                driveRoute.GrossAmountForExcharge = ExtraCharge_Gross;

                // 運賃原価
                driveRoute.GrossAmount = driveRoute.GrossAmountForLaborCost + driveRoute.GrossAmountForFuelCost +
                                        driveRoute.GrossAmountForSyaryoCost + driveRoute.GrossAmountForLuggage +
                                        driveRoute.GrossAmountForExcharge;

            }

            // 運賃原価総額
            driveRoute.GrossAmountTotal = driveRoute.GrossAmount + item.toll;



            ////////////デモ用///////////////
            //driveRoute.GrossAmountForLaborCost = 99999;
            //driveRoute.GrossAmountForFuelCost = 99999;
            //driveRoute.GrossAmountForSyaryoCost = 99999;
            //driveRoute.GrossAmountForLuggage = 99999;
            //driveRoute.GrossAmountForExcharge = 99999;
            //driveRoute.GrossAmount = 99999;
            //driveRoute.GrossAmountTotal = 99999;
            ////////////デモ用///////////////



            switch (driveRoute.routeType)
            {
                case "2":
                    driveRoute.RouteTypeDisplay = "一般道優先";
                    driveRoute.RouteTypeClass += "ex_route_name_1";
                    driveRoute.RouteTitleStyle = "RedGradientBrush";
                    break;
                case "3":
                    driveRoute.RouteTypeDisplay = "道幅優先";
                    driveRoute.RouteTypeClass += "ex_route_name_2";
                    driveRoute.RouteTitleStyle = "BlueGradientBrush";
                    break;
                case "4":
                    driveRoute.RouteTypeDisplay = "距離優先";
                    driveRoute.RouteTypeClass += "ex_route_name_3";
                    driveRoute.RouteTitleStyle = "GreenGradientBrush";
                    break;
                case "5":
                    driveRoute.RouteTypeDisplay = "別ルート";
                    driveRoute.RouteTypeClass += "ex_route_name_4";
                    driveRoute.RouteTitleStyle = "PinkGradientBrush";
                    break;
                default:
                    driveRoute.RouteTypeDisplay = "推奨";
                    driveRoute.RouteTypeClass += "ex_route_name_0";
                    driveRoute.RouteTitleStyle = "SkyBlueGradientBrush";
                    break;
            }

            driveRouteListData.DriveRouteListDisplayList.Add(driveRoute);
        }
    }
}
