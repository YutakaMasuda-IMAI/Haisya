using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Model;


namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnkenController : ControllerBase
    {
        private readonly ILogger<HomeDataController> _logger;

        private readonly ApplicationDbContext _context;

        private readonly MapApiSettings _mapApiSettings;

        public AnkenController(ILogger<HomeDataController> logger, ApplicationDbContext context, IOptions<MapApiSettings> mapApiSettings)
        {
            _logger = logger;
            _context = context;
            _mapApiSettings = mapApiSettings.Value;
        }


        [HttpGet]
        public string Get()
        {

            return _context.Database.ProviderName;

        }



        /// <summary>
        /// 自動車ルート候補一覧検索
        /// </summary>
        /// <param name="DriveListEx">MapApiのルート検索結果</param>
        /// <param name="area">標準運賃計算エリア</param>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="from">出発地点</param>
        /// <param name="to">到着地点</param>
        /// <param name="waypoint">経由地点</param>
        /// <param name="searchparam">検索挙動変更</param>
        /// <param name="height">車種</param>
        /// <param name="width">型</param>
        /// <param name="departuretime">出発時刻指定</param>
        /// <param name="regulationtype">詳細車種</param>
        /// <param name="tolltype">料金車種</param>
        /// <param name="smartic">スマートIC利用指定</param>
        /// <param name="regulation">規制考慮</param>
        /// <param name="twouturn">2段階Uターン回避指定</param>
        /// <param name="ferry">フェリー考慮指定</param>
        /// <param name="TsumiTime">積み時間</param>
        /// <param name="OroshiTime">卸し時間</param>
        /// <param name="DriverGrossCalc">乗務員原価計算区分</param>
        /// <returns>正常：レスポンスのボディ / 異常：null</returns>
        [HttpPost("GetDriveRouteListEx")]
        public async Task<ActionResult<Model.AnkenModel.DriveRouteListDto>> GetDriveRouteListExAsync(
                                                                                        int area,
                                                                                        int CompanyID,
                                                                                        string from, string to,
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
                                                                                        string syasyuSize = null,
                                                                                        int searchparam = 1,
                                                                                        string departuretime = null,
                                                                                        string regulationtype = "121100",
                                                                                        string tolltype = "large",
                                                                                        string smartic = "true",
                                                                                        string timerestriction = "true",
                                                                                        string twouturn = "false",
                                                                                        string ferry = "false",
                                                                                        string TsumiTime = "00:00",
                                                                                        string OroshiTime = "00:00",
                                                                                        int DriverGrossCalc = 0
                                                                                        )
        {

            Model.AnkenModel.DriveRouteListDto resultVal = new();

            try
            {
                List<AnkenModel.AnkenExchargeDto> t_anken_excharge_s = null;
                IFormCollection form = Request.ReadFormAsync().Result;

                if (form.ContainsKey("AnkenExchargeList"))
                {
                    t_anken_excharge_s = new();
                    bool val = form.TryGetValue("AnkenExchargeList", out Microsoft.Extensions.Primitives.StringValues AnkenExchargeList);
                    t_anken_excharge_s = System.Text.Json.JsonSerializer.Deserialize<List<AnkenModel.AnkenExchargeDto>>(AnkenExchargeList);
                }

                DriveListEx driveListtEx = new();

                //ゼンリン地図APIを使用してドライブルートリストを取得する
                DriveRouteModel model = new(_mapApiSettings, _context);
                driveListtEx = await model.GetDriveSerchListEx(from, to, waypoint, syasyu, kata, syasyuSize, height, width, weight, nenpi,
                                                                fromstype, totype, waypointtype,
                                                                searchparam, departuretime, regulationtype, tolltype, smartic,
                                                                timerestriction, twouturn, ferry);

                if (driveListtEx == null) { throw new Exception("自動車ルート候補一覧検索に失敗しました。"); }

                if (driveListtEx.ErrrMessage != null)
                {
                    driveListtEx.ErrrMessage = "自動車ルート候補一覧取得に失敗しました：" + driveListtEx.ErrrMessage;
                    resultVal.ErrrMessage = driveListtEx.ErrrMessage;
                    return resultVal;
                }

                //ドライブルートリストデータに対して標準運賃、見積原価情報を追加する
                AnkenModel ankenModel = new(_context);
                resultVal = await ankenModel.GetDriveRouteListExAsync(driveListtEx, area, CompanyID, syasyu, kata, syasyuSize, 
                                                                    TsumiTime, OroshiTime, DriverGrossCalc, t_anken_excharge_s);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                resultVal.ErrrMessage = ex.Message;
            }
            finally
            {

            }
            return resultVal;
        }


        /// <summary>
        /// 自動車ルート検索
        /// </summary>
        /// <param name="routeID"></param>
        /// <param name="routeType"></param>
        /// <param name="area"></param>
        /// <param name="CompanyID"></param>
        /// <param name="syasyu"></param>
        /// <param name="kata"></param>
        /// <param name="syasyuSize"></param>
        /// <param name="TsumiTime"></param>
        /// <param name="OroshiTime"></param>
        /// <param name="searchparam"></param>
        /// <param name="datum"></param>
        /// <param name="llunit"></param>
        /// <returns></returns>
        [HttpGet("GetDriveDetail")]
        public async Task<ActionResult<Model.AnkenModel.DriveRouteListDto>> GetDriveDetailAsync(string routeID,
                                                                                            int routeType,
                                                                                            int area,
                                                                                            int CompanyID,
                                                                                            string syasyu,
                                                                                            string kata,
                                                                                            string syasyuSize,
                                                                                            string TsumiTime = "00:00",
                                                                                            string OroshiTime = "00:00",
                                                                                            string datum = "JGD",
                                                                                            string llunit = "dec"
                                                                                            )
        {

            Model.AnkenModel.DriveRouteListDto resultVal = new();

            try
            {

                DriveListEx driveListEx = new();

                DriveRouteModel model = new(_mapApiSettings, _context);
                driveListEx = await model.GetDriveDetailAsync(routeID, routeType, CompanyID, syasyu, kata, datum, llunit);

                if (driveListEx == null) { throw new Exception("自動車ルート検索に失敗しました。"); }

                if (driveListEx.ErrrMessage != null)
                {
                    driveListEx.ErrrMessage = "自動車ルート検索に失敗しました：" + driveListEx.ErrrMessage;
                    throw new Exception(driveListEx.ErrrMessage);
                }

                AnkenModel ankenModel = new(_context);
                resultVal = await ankenModel.GetDriveRouteListExAsync(driveListEx, area, CompanyID, syasyu, kata, syasyuSize, TsumiTime, OroshiTime);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                resultVal.ErrrMessage = ex.Message;
            }
            finally
            {

            }
            return resultVal;
        }


        /// <summary>
        /// 指定ユーザーIDのM_Customerの登録件数の降順の指定件数を返却
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="UserID"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        [HttpGet("GetCustomerListForUser")]
        public async Task<ActionResult<List<Data.M_Customer_Branch>>> GetCustomerListForUser(int CompanyID, int UserID, int Top)
        {
            try
            {
                List<int> lst = await _context.V_Anken_Details.Where(m => m.Insert_User == UserID).GroupBy(m => m.KokyakuId).Select(x => new { Name = x.Key, Sum = x.Sum(y => y.KokyakuId) }).OrderByDescending(m => m.Sum).Select(m => m.Name).Take(Top).ToListAsync();

                List<Data.M_Customer_Branch> datalist = new();

                foreach (var data in lst)
                {
                    M_Customer_Branch target = await _context.M_Customer_Branches.FirstOrDefaultAsync(m => m.Customer_Branch_ID == data);
                    if (target != null) { datalist.Add(target); }
                }
                return datalist;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return null;
            }
            finally { }
        }

        /// <summary>
        /// 指定ユーザーIDのM_Customerの直近の登録データの指定件数を返却
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="UserID"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        [HttpGet("GetCustomerListToMostRecentForUser")]
        public async Task<ActionResult<List<Data.M_Customer_Branch>>> GetCustomerListToMostRecentForUser(int CompanyID, int UserID, int Top)
        {
            try
            {
                List<int> lst = await _context.V_Anken_Details.Where(m => m.Insert_User == UserID).GroupBy(m => m.KokyakuId).Select(x => new { Name = x.Key, Max = x.Max(y => y.Insert_Datetime) }).OrderByDescending(m => m.Max).Select(m => m.Name).Take(Top).ToListAsync();

                List<Data.M_Customer_Branch> datalist = new();

                foreach (var data in lst)
                {
                    M_Customer_Branch target = await _context.M_Customer_Branches.FirstOrDefaultAsync(m => m.Customer_Branch_ID == data);
                    if (target != null) { datalist.Add(target); }
                }
                return datalist;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return null;
            }
            finally { }
        }

    }
}
