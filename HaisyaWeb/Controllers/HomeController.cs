using HaisyaWeb.Models;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HaisyaWeb.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(ILogger<HomeController> logger,
            SignInManager<ApplicationUser> signInManager, IViewRenderService viewRenderService, IOptions<MapApiSettings> mapApiSetting)
        {
            _signInManager = signInManager;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
        }

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                DateTime dateTime = DateTime.Now;

                HomeModel.HomeMainModel model = new()
                {
                    AdminInfos = new(),
                };

                using API.WebApp.HomeDataApi apiH = new(_mapApiSettiong);
                model.AdminInfos = await apiH.GetAdminInfos(dateTime);

                await SetAddressList();

                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                ErrorViewModel errorModel = new();
                errorModel.RequestId = "1";
                errorModel.Title = "Index";
                errorModel.Message = ex.Message;
                return View("Error", errorModel);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //private async Task GetGetAddressList()
        //{

        //    //if 

        //    Context.AddressList list = new();

        //    API.WebApp.MasterDataApi api = new(_mapApiSettiong);

        //    list.HokkaidoAddressItem = await api.GetGetAddressList(1);
        //    list.TohokuAddressItem = await api.GetGetAddressList(2);
        //    list.ChubuAddressItem = await api.GetGetAddressList(3);
        //    list.KantoAddressItem = await api.GetGetAddressList(4);
        //    list.KinkiAddressItem = await api.GetGetAddressList(5);
        //    list.ChugokuAddressItem = await api.GetGetAddressList(6);
        //    list.ShikokuAddressItem = await api.GetGetAddressList(7);
        //    list.KyusyuAddressItem = await api.GetGetAddressList(8);
        //    list.OkinawaAddressItem = await api.GetGetAddressList(9);

        //}

        [HttpPost]
        public async Task<IActionResult> GetLoginUserInfo()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                return Json(new { result = loguinUser });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// お知らせの一覧の表示
        /// </summary>
        /// <param name="selectMonth"></param>
        /// <param name="selectJogyosyo"></param>
        /// <param name="alertDays"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> JsonDisplaySystemInfo()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                DateTime dateTime = DateTime.Now;

                HomeModel.HomeSystemInfoModel model = new();

                using API.WebApp.HomeDataApi apiH = new(_mapApiSettiong);
                model.PortalInfos = await apiH.GetPortalInfos(dateTime, loguinUser.Company_ID, loguinUser.User_ID);

                return await PartialViewAsJson("SystemInfo", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// お知らせ一覧のクリック処理
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SubmitSystemInfo(int id)
        {
            try
            {
                if (id == 0) { throw new Exception("パラメーターエラー：" + id.ToString()); }

                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                using API.WebApp.HomeDataApi apiH = new(_mapApiSettiong);
                Dto.T_Portal_Info_Local data = await apiH.GetPortalInfo(id);
                
                if (data != null)
                {
                    return RedirectToAction(data.Controller, data.Action, data);
                }
                return RedirectToAction("","");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                ErrorViewModel errorModel = new();
                errorModel.RequestId = "1";
                errorModel.Title = "SubmitSystemInfo";
                errorModel.Message = ex.Message;
                return View("Error", errorModel);
            }
        }
    }
}
