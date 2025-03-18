using HaisyaWeb.API.WebApp;
using HaisyaWeb.Models;
using HaisyaWeb.Models.DB;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace HaisyaWeb.Controllers
{
    [Authorize]
    public class TabletTransferController : BaseController
    {

        private readonly ILogger<TabletTransferController> _logger;

        private const string SessionCopyAnken = "_objCopyAnkenInfo";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="viewRenderService"></param>
        /// <param name="mapApiSetting"></param>
        public TabletTransferController(ILogger<TabletTransferController> logger, IViewRenderService viewRenderService,
            IOptions<MapApiSettings> mapApiSetting, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// タブレット送信画面（モーダル）を開く
        /// </summary>
        /// <param name="SyaryoID"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> TabletTransferModal(int syaryoManagementID, int driverID)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                TabletTransferModel model = new();

                model.MapApiSettings = _mapApiSettiong;
                model.WebViewFlg = GetWebViewFlg();

                MasterDataApi api = new(_mapApiSettiong);
                model.CompanyDriver = await api.GetCompanyDriverData(driverID);
                model.SyaryoManagement = await api.GetSyaryoManagementData(syaryoManagementID);
                model.Syaryo = await api.GetSyaryoData(loguinUser.Company_ID, (int)model.SyaryoManagement.Syaryo_ID);

                return await PartialViewAsJson("../TabletTransfer/Index", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, message = ex.Message });
            }
        }


        /// <summary>
        /// タブレット送信送信画面を開く
        /// </summary>
        /// <param name="data">AnkenRegisterModel</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SendTablet(TabletTransferModel data)
        {
            try
            {
                return Json(new { result = "", message = "" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }
    }
}
