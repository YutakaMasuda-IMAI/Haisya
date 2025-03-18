using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebApplication.Data;

namespace WebApplication.Model
{
    /// <summary>
    /// ドライブルートモデルクラス
    /// </summary>
    public class DriveRouteModel : BaseModel
    {
        private readonly MapApiSettings _mapApiSettings;

        public DriveRouteModel(MapApiSettings mapApiSettings, ApplicationDbContext context)
        {
            _mapApiSettings = mapApiSettings;
            _context = context;
        }

        /// <summary>
        /// 自動車ルート候補一覧取得
        /// 緯度経度から自動車の経路候補を取得します。
        /// </summary>
        /// <param name="from">出発地点</param>
        /// <param name="to">到着地点</param>
        /// <param name="mpoints">経由地点</param>
        /// <param name="height">車種</param>
        /// <param name="width">型</param>
        /// <param name="searchparam">検索挙動変更</param>
        /// <param name="departuretime">出発時刻指定</param>
        /// <param name="cardetailinfo">詳細車種</param>
        /// <param name="tolltype">料金車種</param>
        /// <param name="smartic">スマートIC利用指定</param>
        /// <param name="regulation">規制考慮</param>
        /// <param name="twouturn">2段階Uターン回避指定</param>
        /// <param name="ferry">フェリー考慮指定</param>
        /// <param name="datum">測地系</param>
        /// <returns>ドライブリスト</returns>
        public async Task<DriveListEx> GetDriveSerchListEx(string from, string to,
                                                        string mpoints = null,
                                                        string syasyu = null,
                                                        string kata = null,
                                                        string syasyuSize = null,
                                                        double height = 0,
                                                        double width = 0,
                                                        double weight = 0,
                                                        double nenpi = 0,
                                                        string fromstype = "",
                                                        string totype = "",
                                                        string mpointstype = "",
                                                        int searchparam = 1,
                                                        string departuretime = null, string cardetailinfo = "B", string tolltype = "large",
                                                        string smartic = "T", string regulation = "season,time", string twouturn = "F", string ferry = "F",
                                                        string datum = "JGD")
        {
            // 自動車ルート候補一覧取得
            ZenrinMapAPI api = new(_mapApiSettings);
            DriveList driveList = await api.GetDriveSerchList(from, to, mpoints, searchparam, height, width, weight,
                                                                fromstype, totype, mpointstype, departuretime, cardetailinfo,
                                                                tolltype, smartic, regulation, twouturn, ferry, datum);

            DriveListEx driveListEx = GetDriveRouteListAndCalcCost(driveList, height, width, weight, nenpi, syasyu, kata, syasyuSize);

            return driveListEx;
        }

        /// <summary>
        /// ドライブルートリストに対する運行に係る時間や燃料計算を実施する
        /// </summary>
        /// <param name="routeID">ルートID</param>
        /// <param name="routeType">ルートタイプ</param>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="syasyu">車種</param>
        /// <param name="kata">型</param>
        /// <param name="datum">測地系</param>
        /// <param name="llunit">緯度経度単位</param>
        /// <returns>ドライブリスト拡張</returns>
        public async Task<DriveListEx> GetDriveDetailAsync(string routeID,
                                                int routeType,
                                                int CompanyID,
                                                string syasyu,
                                                string kata,
                                                string datum = "JGD",
                                                string llunit = "dec")
        {
            ////////////// マスターデータの取得//////////////////////////////////////////////
            // 車輌マスタ
            Data.M_Syaryo m_Syaryo = await _context.M_Syaryos.FirstOrDefaultAsync(m => m.Company_ID == CompanyID && m.KATA == kata);
            if (m_Syaryo == null) { throw new Exception("車輌マスタに対象車種のデータがありません。"); }

            // 自動車ルート候補一覧取得
            ZenrinMapAPI api = new(_mapApiSettings);
            DriveList driveList = await api.GetDriveDetail(routeID, datum, llunit);

            if (driveList.route == null) { throw new Exception("自動車ルートの詳細データが取得出来ません。"); }

            DriveListItem target = driveList.route;

            DriveListEx driveListEx = new()
            {
                route = new(),
            };

            DriveListItemEx driveListItemEx = new()
            {
                Height = (double)m_Syaryo.HEIGHT,
                Width = (double)m_Syaryo.WIDTH,
                Weight = (double)m_Syaryo.HEIGHT,
                Nenpi = (double)m_Syaryo.AVG_FUEL_COSTS,
                Syasyu = syasyu,
                Kata = kata,
                routeID = target.routeID,
                routeType = routeType.ToString(),
                distance = target.distance,
                toll = target.toll,
                invalidFee = target.invalidFee,
                time = target.time,
                passage = target.passage,
                vicsTimeStamp = target.vicsTimeStamp,
                mPointsOrder = target.mPointsOrder,
                line = new(),
                link = new(),
                detailedTime = new(),
            };

            driveListItemEx = SetCalcCost(driveListItemEx, target, (double)m_Syaryo.AVG_FUEL_COSTS);

            driveListEx.route = driveListItemEx;

            return driveListEx;
        }

        /// <summary>
        /// ドライブルートリストに対する運行に係る時間や燃料計算を実施する
        /// </summary>
        /// <param name="driveList">ドライブリスト</param>
        /// <param name="height">高さ</param>
        /// <param name="width">幅</param>
        /// <param name="weight">重量</param>
        /// <param name="nenpi">燃費</param>
        /// <param name="syasyu">車種</param>
        /// <param name="kata">型</param>
        /// <returns>ドライブリスト拡張</returns>
        public DriveListEx GetDriveRouteListAndCalcCost(DriveList driveList,
                                                        double height, double width, double weight,
                                                        double nenpi, string syasyu, string kata, string syasyuSize)
        {
            DriveListEx driveListEx = new();
            driveListEx.item = new();

            driveListEx.ErrrMessage = driveList.ErrrMessage;

            if (driveList.item != null)
            {
                foreach (DriveListItem target in driveList.item)
                {
                    DriveListItemEx driveListItemEx = new()
                    {
                        Height = height,
                        Width = width,
                        Weight = weight,
                        Nenpi = nenpi,
                        Syasyu = syasyu,
                        Kata = kata,
                        SyasyuSize = syasyuSize,
                        routeID = target.routeID,
                        routeType = target.routeType,
                        distance = target.distance,
                        toll = target.toll,
                        invalidFee = target.invalidFee,
                        time = target.time,
                        passage = target.passage,
                        vicsTimeStamp = target.vicsTimeStamp,
                        mPointsOrder = target.mPointsOrder,
                        line = new(),
                        link = new(),
                        detailedTime = new(),
                    };

                    driveListItemEx = SetCalcCost(driveListItemEx, target, nenpi);

                    driveListEx.item.Add(driveListItemEx);
                }
            }
            return driveListEx;
        }

        /// <summary>
        /// ドライブルートアイテムに対する運行に係る時間や燃料計算を実施する
        /// </summary>
        /// <param name="driveListItemEx">ドライブルートアイテム拡張</param>
        /// <param name="target">ドライブルートアイテム</param>
        /// <param name="nenpi">燃費</param>
        /// <returns>ドライブルートアイテム拡張</returns>
        private DriveListItemEx SetCalcCost(DriveListItemEx driveListItemEx, DriveListItem target, double nenpi)
        {
            driveListItemEx.line = target.line;
            driveListItemEx.link = target.link;
            driveListItemEx.detailedTime = target.detailedTime;

            // 消費燃料計算
            driveListItemEx.FuelConsume = Math.Round((target.distance / Convert.ToDouble(1000)) / nenpi, 2, MidpointRounding.AwayFromZero);

            // 案件トータル日程
            driveListItemEx.TotalDays = 0;

            //休憩休息時間計算
            if (driveListItemEx.time > 0)
            {
                var span = new TimeSpan(0, driveListItemEx.time, 0);
                int day = int.Parse(span.ToString(@"dd"));
                int houer = int.Parse(span.ToString(@"hh")) + (24 * day);
                int minute = int.Parse(span.ToString(@"mm")); //
                if (minute >= 30) { houer += 1; } // 所要時間(分)が30分以上は運転時間１時間繰り上げ

                int days = 0;

                ////////////////////運行１日目///////////////////////////////

                if (houer <= 4)
                {
                    driveListItemEx.TotalDays = days;
                    driveListItemEx.BreakTime = 0;
                    driveListItemEx.RestTime = 480 * days;

                    driveListItemEx.RoudoTime = houer;
                }
                else if (houer > 4 && houer <= 8) //初日運転　１度目の休憩後　４時間運転可能
                {
                    driveListItemEx.TotalDays = days;
                    driveListItemEx.BreakTime = 30 * 1;
                    driveListItemEx.RestTime = 480 * days;

                    driveListItemEx.RoudoTime = houer;
                }
                else if (houer == 9) //初日運転　２度目の休憩後　１時間運転可能（１日の運転時間は９時間以内） 
                {
                    driveListItemEx.TotalDays = days;
                    driveListItemEx.BreakTime = 30 * 2;
                    driveListItemEx.RestTime = 480 * days;

                    driveListItemEx.RoudoTime = 8;
                    driveListItemEx.ZangyoTime = days + 1;
                }

                ////////////////////運行２日目///////////////////////////////
                days = 1;

                if (houer > 9 && houer <= 13)  //翌日運転（１度目の休息）　最初の４時間運転　
                {
                    driveListItemEx.TotalDays = days;
                    driveListItemEx.BreakTime = 30 * 2;
                    driveListItemEx.RestTime = 480 * days;

                    driveListItemEx.RoudoTime = houer - days;
                    driveListItemEx.ZangyoTime = days;
                }
                else if (houer > 13 && houer <= 17)  //翌日運転（１度目の休息）１度目の休憩後　４時間運転可能
                {
                    driveListItemEx.TotalDays = days;
                    driveListItemEx.BreakTime = 30 * 3;
                    driveListItemEx.RestTime = 480 * days;

                    driveListItemEx.RoudoTime = houer - days;
                    driveListItemEx.ZangyoTime = days;
                }
                else if (houer == 18)  //翌日運転（１度目の休息）２度目の休憩後　１時間運転可能
                {
                    driveListItemEx.TotalDays = days;
                    driveListItemEx.BreakTime = 30 * 4;
                    driveListItemEx.RestTime = 480 * days;

                    driveListItemEx.RoudoTime = houer - (days + 1);
                    driveListItemEx.ZangyoTime = days + 1;
                }

                ////////////////////運行３日目///////////////////////////////
                days = 2;

                if (houer > 18 && houer <= 22) //翌々日運転（２度目の休息） 最初の４時間運転　
                {
                    driveListItemEx.TotalDays = days;
                    driveListItemEx.BreakTime = 30 * 4;
                    driveListItemEx.RestTime = 480 * days;

                    driveListItemEx.RoudoTime = houer - days;
                    driveListItemEx.ZangyoTime = days;
                }
                else if (houer > 22 && houer < 26)  //翌々日運転（２度目の休息）１度目の休憩後　４時間運転可能
                {
                    driveListItemEx.TotalDays = days;
                    driveListItemEx.BreakTime = 30 * 5;
                    driveListItemEx.RestTime = 480 * days;

                    driveListItemEx.RoudoTime = houer - days;
                    driveListItemEx.ZangyoTime = days;
                }
                else if (houer == 27)  //翌々日運転（２度目の休息）２度目の休憩後　１時間運転可能
                {
                    driveListItemEx.TotalDays = days;
                    driveListItemEx.BreakTime = 30 * 6;
                    driveListItemEx.RestTime = 480 * days;

                    driveListItemEx.RoudoTime = houer - (days + 1);
                    driveListItemEx.ZangyoTime = days + 1;
                }

                ////////////////////運行４日目///////////////////////////////
                days = 3;

                if (houer > 27 && houer <= 31)  //翌々々日運転（３度目の休息）最初の４時間運転　
                {
                    driveListItemEx.TotalDays = days;
                    driveListItemEx.BreakTime = 30 * 6;
                    driveListItemEx.RestTime = 480 * days;

                    driveListItemEx.RoudoTime = houer - days;
                    driveListItemEx.ZangyoTime = days;
                }
                else if (houer > 31 && houer <= 35)  //翌々々日運転（３度目の休息）１度目の休憩後　４時間運転可能
                {
                    driveListItemEx.TotalDays = days;
                    driveListItemEx.BreakTime = 30 * 7;
                    driveListItemEx.RestTime = 480 * days;

                    driveListItemEx.RoudoTime = houer - days;
                    driveListItemEx.ZangyoTime = days;
                }
                else if (houer == 36)  //翌々々日運転（３度目の休息）２度目の休憩後　１時間運転可能
                {
                    driveListItemEx.TotalDays = days;
                    driveListItemEx.BreakTime = 30 * 8;
                    driveListItemEx.RestTime = 480 * days;

                    driveListItemEx.RoudoTime = houer - (days + 1);
                    driveListItemEx.ZangyoTime = days + 1;
                }

                ////////////////////運行５日目///////////////////////////////
                days = 4;

                if (houer > 36 && houer <= 40)  //翌々々々日運転（４度目の休息）最初の４時間運転　
                {
                    driveListItemEx.TotalDays = days;
                    driveListItemEx.BreakTime = 30 * 8;
                    driveListItemEx.RestTime = 480 * days;

                    driveListItemEx.RoudoTime = houer - days;
                    driveListItemEx.ZangyoTime = days;
                }
                else if (houer > 40 && houer <= 44)  //翌々々々日運転（４度目の休息）１度目の休憩後　４時間運転可能
                {
                    driveListItemEx.TotalDays = days;
                    driveListItemEx.BreakTime = 30 * 9;
                    driveListItemEx.RestTime = 480 * days;

                    driveListItemEx.RoudoTime = houer - days;
                    driveListItemEx.ZangyoTime = days;
                }
                else if (houer == 45)  //翌々々々日運転（４度目の休息）２度目の休憩後　１時間運転可能
                {
                    driveListItemEx.TotalDays = days;
                    driveListItemEx.BreakTime = 30 * 10;
                    driveListItemEx.RestTime = 480 * days;

                    driveListItemEx.RoudoTime = houer - (days + 1);
                    driveListItemEx.ZangyoTime = days + 1;
                }

                ////////////////////運行６日目///////////////////////////////
                days = 5;

                if (houer > 45 && houer <= 49)  //翌々々々々日運転（５度目の休息）最初の４時間運転　
                {
                    driveListItemEx.TotalDays = days;
                    driveListItemEx.BreakTime = 30 * 10;
                    driveListItemEx.RestTime = 480 * days;

                    driveListItemEx.RoudoTime = houer - days;
                    driveListItemEx.ZangyoTime = days;
                }
                else if (houer > 49 && houer <= 53)  //翌々々々々日運転（５度目の休息）１度目の休憩後　４時間運転可能
                {
                    driveListItemEx.TotalDays = days;
                    driveListItemEx.BreakTime = 30 * 11;
                    driveListItemEx.RestTime = 480 * days;

                    driveListItemEx.RoudoTime = houer - days;
                    driveListItemEx.ZangyoTime = days;
                }
                else if (houer == 54)  //翌々々々々日運転（５度目の休息）２度目の休憩後　１時間運転可能
                {
                    driveListItemEx.TotalDays = days;
                    driveListItemEx.BreakTime = 30 * 12;
                    driveListItemEx.RestTime = 480 * days;

                    driveListItemEx.RoudoTime = houer - (days + 1);
                    driveListItemEx.ZangyoTime = days + 1;
                }

                ////////////////////運行７日目///////////////////////////////
                days = 6;

                if (houer > 54 && houer <= 58)  //翌々々々々日運転（６度目の休息）最初の４時間運転　
                {
                    //実際には無いよ！！　改善基準違反です。
                    driveListItemEx.TotalDays = days;
                    driveListItemEx.BreakTime = 30 * 12;
                    driveListItemEx.RestTime = 480 * days;

                    driveListItemEx.RoudoTime = houer - days;
                    driveListItemEx.ZangyoTime = days;
                }
                else if (houer > 58 && houer <= 62)  //翌々々々々日運転（６度目の休息）１度目の休憩後　４時間運転可能
                {
                    //実際には無いよ！！　改善基準違反です。
                    driveListItemEx.TotalDays = days;
                    driveListItemEx.BreakTime = 30 * 13;
                    driveListItemEx.RestTime = 480 * days;

                    driveListItemEx.RoudoTime = houer - days;
                    driveListItemEx.ZangyoTime = days;
                }
                else if (houer == 63)  //翌々々々々日運転（６度目の休息）２度目の休憩後　１時間運転可能
                {
                    //実際には無いよ！！　改善基準違反です。
                    driveListItemEx.TotalDays = days;
                    driveListItemEx.BreakTime = 30 * 14;
                    driveListItemEx.RestTime = 480 * days;

                    driveListItemEx.RoudoTime = houer - (days + 1);
                    driveListItemEx.ZangyoTime = days + 1;
                }

                ////////////////////運行８日目///////////////////////////////
                days = 7;

                if (houer >= 62)  //7日以降です。　ダメです。1運行6日以内です
                {
                    //実際には無いよ！！　改善基準違反です。
                    driveListItemEx.TotalDays = days;
                    driveListItemEx.BreakTime = 0;
                    driveListItemEx.RestTime = 0;

                    driveListItemEx.RoudoTime = houer - (days + 1);
                    driveListItemEx.ZangyoTime = days + 1;
                }

                
            }

            return driveListItemEx;
        }


    }

    /// <summary>
    /// ドライブリスト拡張クラス
    /// </summary>
    public class DriveListEx
    {
        public string ErrrMessage { get; set; } = null;

        public List<DriveListItemEx> item { get; set; }

        public DriveListItemEx route { get; set; }
    }

    public class DriveListItemEx2 : Model.DriveListItemEx
    {
        //標準運賃
        public double StdFreight { get; set; }

        // 標準運賃　追加料金
        public double StdExcharge { get; set; }

        //標準運賃合計
        [Display(Name = "標準運賃")]
        public double StdALLFreight { get; set; }

        //請求標準運賃合計
        public double StdTotalFreight { get; set; }

        // 運賃原価(労務費）
        public double GrossAmountForLaborCost { get; set; }

        // 運賃原価(燃料費）
        public double GrossAmountForFuelCost { get; set; }

        // 運賃原価(車輌費）
        public double GrossAmountForSyaryoCost { get; set; }

        // 運賃原価(荷物（荷待ち・荷積み・荷卸し））
        public double GrossAmountForLuggage { get; set; }

        // 運賃原価(その他追加料金）
        public double GrossAmountForExcharge { get; set; }

        // 運賃原価
        public double GrossAmount { get; set; }

        // 運賃原価総額
        [Display(Name = "見積原価")]
        public double GrossAmountTotal { get; set; }

        // 追加料金
        [Display(Name = "追加料金")]
        public double ExtraCharge { get; set; }
    }

    /// <summary>
    /// ドライブリストアイテム拡張クラス
    /// </summary>
    public class DriveListItemEx : DriveListItem
    {
        //消費燃料
        public double FuelConsume { get; set; }
        //休息時間
        public int RestTime { get; set; }
        //休憩時間
        public int BreakTime { get; set; }
        
        //車種
        public string Syasyu { get; set; }
        //型
        public string Kata { get; set; }
        //サイズ
        public string SyasyuSize { get; set; }

        //高さ
        public double Height { get; set; }
        //幅
        public double Width { get; set; }
        //重量
        public double Weight { get; set; }
        //燃費
        public double Nenpi { get; set; }

        // 案件トータル日程
        public int TotalDays { get; set; }

        // 労働時間
        public int RoudoTime { get; set; } = 0;
        // 残業時間
        public int ZangyoTime { get; set; } = 0;
    }
}
