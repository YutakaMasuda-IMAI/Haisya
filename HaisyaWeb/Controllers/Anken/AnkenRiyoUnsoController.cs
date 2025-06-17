using HaisyaWeb.Models;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using static HaisyaWeb.Models.AnkenModel;

namespace HaisyaWeb.Controllers.Anken
{
    [Authorize]
    public class AnkenRiyoUnsoController : BaseController
    {

        private readonly ILogger<AnkenRiyoUnsoController> _logger;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="viewRenderService"></param>
        /// <param name="mapApiSetting"></param>
        public AnkenRiyoUnsoController(ILogger<AnkenRiyoUnsoController> logger, IViewRenderService viewRenderService,
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
        /// 利用運送登録画面を開く
        /// </summary>
        /// <param name="param">SearchModelForAnkenList</param>
        /// <returns></returns>
        public async Task<IActionResult> RiyoUnsoIndex(AnkenBaseModel param)
        {
            try
            {
                RiyoUnsoAnkenRegisterModel model = await CreateModelRiyoUnso();

                if (param.SelectDay != null)
                {
                    model.SelectDay = param.SelectDay;
                    model.SelectTantou = param.SelectTantou;
                    model.SelectSyasyu = param.SelectSyasyu;
                    model.SelectKata = param.SelectKata;
                    model.SelectGroup = param.SelectGroup;
                    model.BackMenuAction = param.BackMenuAction;
                }
                else
                {
                    model.BackMenuAction = "Home";
                }

                return RedirectToAction("RegisterIndex", param);

            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        /// <summary>
        /// RiyoUnsoAnkenRegisterModelを作成して返却する
        /// </summary>
        /// <returns></returns>
        private async Task<RiyoUnsoAnkenRegisterModel> CreateModelRiyoUnso()
        {

            RiyoUnsoAnkenRegisterModel model = new()
            {
                Anken_ID = 0,
                Anken_Status = 0,
                Anken_Latest_Order = 0,
                AnkenRiyounsoData = new(),
                AnkenRiyounsoPointList = new(),
                RegKubun = 0,
            };

            // ログインユーザー取得
            Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
            model.UserID = loguinUser.User_ID;
            model.Company_ID = loguinUser.Company_ID;
            model.Branch_ID = loguinUser.Branch_ID;




            model.MapApiSettings = _mapApiSettiong;

            string url = _mapApiSettiong.WebUri.JavaScriptAPI + "/auth/jsapi/loader.htm";
            url += GetMapApiUrlPram();
            model.MapsApiForJSUrl = url;
            model.WebViewFlg = GetWebViewFlg();

            return model;

        }
    }
}
