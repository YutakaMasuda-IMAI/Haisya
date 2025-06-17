using HaisyaWeb.Models;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HaisyaWeb.Controllers
{
    /// <summary>
    /// トータルコントローラー
    /// </summary>
    [Authorize]
    public class TotalController : BaseController
    {
        private readonly ILogger<TotalController> _logger;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger">ロガー</param>
        /// <param name="viewRenderService">ビューのレンダリングサービス</param>
        /// <param name="mapApiSetting">マップAPI設定</param>
        /// <param name="signInManager">サインインマネージャー</param>
        public TotalController(ILogger<TotalController> logger, IViewRenderService viewRenderService,
            IOptions<MapApiSettings> mapApiSetting, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Index処理
        /// </summary>
        /// <param name="model">モデル</param>
        /// <returns>ビュー</returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}
