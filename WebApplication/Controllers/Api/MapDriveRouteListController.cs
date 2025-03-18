using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Model;

namespace WebApplication.Controllers
{
    /// <summary>
    /// 自動車ルート候補一覧を管理するコントローラー
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MapDriveRouteListController : ControllerBase
    {
        private readonly ILogger<MapDriveRouteListController> _logger;
        private readonly MapApiSettings _mapApiSettings;
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// MapDriveRouteListControllerのコンストラクタ
        /// </summary>
        /// <param name="logger">ロガー</param>
        /// <param name="mapApiSettings">マップAPI設定</param>
        /// <param name="context">データベースコンテキスト</param>
        public MapDriveRouteListController(ILogger<MapDriveRouteListController> logger,
                                            IOptions<MapApiSettings> mapApiSettings, ApplicationDbContext context)
        {
            _logger = logger;
            _mapApiSettings = mapApiSettings.Value;
            _context = context;
        }

        /// <summary>
        /// 自動車ルート候補一覧取得
        /// 緯度経度から自動車の経路候補を取得します。
        /// </summary>
        /// <param name="from">出発地点</param>
        /// <param name="to">到着地点</param>
        /// <param name="waypoint">経由地点</param>
        /// <param name="syasyu">車種</param>
        /// <param name="kata">型</param>
        /// <param name="height">高さ</param>
        /// <param name="width">幅</param>
        /// <param name="weight">重量</param>
        /// <param name="nenpi">燃費</param>
        /// <param name="fromstype">出発地点の種類</param>
        /// <param name="totype">到着地点の種類</param>
        /// <param name="waypointtype">経由地点の種類</param>
        /// <param name="searchparam">検索挙動変更</param>
        /// <param name="departuretime">出発時刻指定</param>
        /// <param name="regulationtype">規制考慮</param>
        /// <param name="tolltype">料金車種</param>
        /// <param name="smartic">スマートIC利用指定</param>
        /// <param name="timerestriction">時間制限</param>
        /// <param name="twouturn">2段階Uターン回避指定</param>
        /// <param name="ferry">フェリー考慮指定</param>
        /// <returns>自動車ルート候補一覧</returns>
        [HttpGet]
        public async Task<ActionResult<DriveListEx>> Get(string from, string to,
                                                        string waypoint = null,
                                                        string syasyu = null,
                                                        string kata = null,
                                                        double height = 0,
                                                        double width = 0,
                                                        double weight = 0,
                                                        double nenpi = 0,
                                                        string fromstype = "",
                                                        string totype = "",
                                                        string waypointtype = "",
                                                        int searchparam = 1,
                                                        string departuretime = null,
                                                        string regulationtype = "121100",
                                                        string tolltype = "large",
                                                        string smartic = "true",
                                                        string timerestriction = "true",
                                                        string twouturn = "false",
                                                        string ferry = "false")
        {
            DriveListEx driveListtEx = new();

            if (from == null || to == null)
            {
                driveListtEx.ErrrMessage = "自動車ルート候補一覧取得に失敗しました。:" + from + ":" + to;
                return driveListtEx;
            }

            string responseBody = "";
            string errorMessage = null;

            try
            {
                DriveRouteModel model = new(_mapApiSettings, _context);
                driveListtEx = await model.GetDriveSerchListEx(from, to, waypoint, syasyu, kata, null, height, width, weight, nenpi,
                                                                fromstype, totype, waypointtype,
                                                                searchparam, departuretime, regulationtype, tolltype, 
                                                                smartic, timerestriction, twouturn, ferry);

                if (driveListtEx.ErrrMessage != null)
                {
                    driveListtEx.ErrrMessage = "自動車ルート候補一覧取得に失敗しました：" + driveListtEx.ErrrMessage;
                    return driveListtEx;
                }
                else
                {
                    return driveListtEx;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(responseBody);
                if (errorMessage == null)
                {
                    errorMessage = e.Message;
                }

                driveListtEx.ErrrMessage = errorMessage;
                return driveListtEx;
            }
            finally
            {
            }
        }
    }
}
