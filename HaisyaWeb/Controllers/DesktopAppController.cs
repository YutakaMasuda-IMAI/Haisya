using HaisyaWeb.API.WebApp;
using HaisyaWeb.Dto;
using HaisyaWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using static HaisyaWeb.Models.AnkenModel;

namespace HaisyaWeb.Controllers
{
    public class DesktopAppController : Controller
    {
        private readonly MapApiSettings _mapApiSettiong;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public DesktopAppController(IOptions<MapApiSettings> mapApiSetting, IHttpContextAccessor httpContextAccessor)
        {
            _mapApiSettiong = mapApiSetting.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index(int webViewFlg = 0)
        {

            DesktopAppModel model = new()
            {
                WebViewFlg = webViewFlg,
                MapApiSettings = _mapApiSettiong
            };
            string url = _mapApiSettiong.WebUri.JavaScriptAPI + "/auth/jsapi/loader.htm";
            url += GetMapApiUrlPram();
            model.MapsApiForJSUrl = url;

            return View(model);
        }

        public IActionResult Index2(int webViewFlg = 0)
        {
            DesktopAppModel model = new()
            {
                WebViewFlg = webViewFlg,
                MapApiSettings = _mapApiSettiong
            };
            string url = _mapApiSettiong.WebUri.JavaScriptAPI + "/auth/jsapi/loader.htm";
            url += GetMapApiUrlPram();
            model.MapsApiForJSUrl = url;

            return View(model);
        }

        public async Task<IActionResult> DriveRouteDetail(int companyID, string routeID,
                                                            string sasyuDisp, string tsumiTaskTime, string oroshiTaskTime, int area,
                                                            string routeType = "", int webViewFlg = 0)
        {
            //// セッションから文字列を読み込む
            //string sasyuDisp = HttpContext.Session.GetString("SyasyuDisplay");
            //string tsumiTaskTime = HttpContext.Session.GetString("TsumiTaskTime");
            //string oroshiTaskTime = HttpContext.Session.GetString("OroshiTaskTime");
            try
            {
                // セッション情報のエラーチェック
                if (!(sasyuDisp != null && sasyuDisp.Length > 0)) { return null; }
                if (!(tsumiTaskTime != null && tsumiTaskTime.Length > 0)) { return null; }
                if (!(oroshiTaskTime != null && oroshiTaskTime.Length > 0)) { return null; }

                //////車種設定
                //item.Key.syasyu + '-' + item.Key.kata + '-' + item.Key.size
                //車種
                string Syasyu = sasyuDisp.Substring(0, sasyuDisp.IndexOf("-"));
                //型
                string Kata = sasyuDisp.Substring(sasyuDisp.IndexOf("-") + 1, sasyuDisp.IndexOf("-", sasyuDisp.IndexOf("-") + 1) - sasyuDisp.IndexOf("-") - 1);
                //サイズ
                string SyasyuSize = sasyuDisp.Substring(sasyuDisp.IndexOf("-", sasyuDisp.IndexOf("-") + 1) + 1);


                // 検索ルート検索処理
                AnkenDataApi ankenDataApi = new(_mapApiSettiong);
                DriveRouteListDto_Local driveListtEx = await ankenDataApi.GetDriveDetailAsync(companyID, routeID, routeType, area,
                                                                                            Syasyu, Kata, SyasyuSize,
                                                                                            tsumiTaskTime, oroshiTaskTime)
                    ?? throw new Exception("ネットワーク接続エラー");
                if (driveListtEx.ErrrMessage != null && driveListtEx.ErrrMessage.Length > 0) { throw new Exception(driveListtEx.ErrrMessage); }

                string url = _mapApiSettiong.WebUri.JavaScriptAPI + "/auth/jsapi/loader.htm";
                url += GetMapApiUrlPram();

                RouteDetailModel model = new()
                {
                    driveList = driveListtEx.DriveRouteListDisplayList[0],
                    MapApiSettings = _mapApiSettiong,
                    MapsApiForJSUrl = url,
                    CenterLatlon = new(),
                    WebViewFlg = GetMapsServerLocal()
                };

                // 中心マーカーの算出
                int i = model.driveList.link.Count / 2;
                model.CenterLatlon.lat = model.driveList.link[i].line[0].lat;
                model.CenterLatlon.lng = model.driveList.link[i].line[0].lng;

                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel()
                {
                    RequestId = ex.HelpLink ?? "強制エラー処理",
                    Message = ex.Message
                });
            }
        }

        //public async Task<MapApiModel.DriveList_Local> MapGetAddressFromPosition(string routeID)
        //{

        //    if (routeID == null)
        //    {
        //        return null;
        //    }

        //    string responseBody = "";
        //    string errorMessage = null;

        //    try
        //    {

        //        MapApiModel.DriveList_Local driveRoute = new();
        //        ZenrinMapAPI api = new(_mapApiSettiong);
        //        driveRoute = await api.GetDriveDetail(routeID);

        //        if (driveRoute.ErrrMessage != null)
        //        {
        //            return driveRoute;
        //        }
        //        else
        //        {
        //            return driveRoute;
        //        }


        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        Console.WriteLine(responseBody);
        //        if (errorMessage == null)
        //        {
        //            errorMessage = e.Message;
        //        }

        //        return null;
        //    }
        //    finally
        //    {

        //    }

        //}

        /// <summary>
        /// MapApiサーバーがローカルかサーバーかを判断して返却
        /// True:サーバー；False：ローカル
        /// </summary>
        /// <returns></returns>
        private int GetMapsServerLocal() => _mapApiSettiong.Api.WebAPIHosts.Contains("localhost") ? 0 : 1;

        private string GetMapApiUrlPram() => GetMapsServerLocal() == 1 ? string.Format("?key={0}&type=special", _mapApiSettiong.AuthAid.AuthCode) : "";
    }
}
