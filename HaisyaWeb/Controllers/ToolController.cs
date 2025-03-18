using HaisyaWeb.Models;
using HaisyaWeb.Models.DB;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace HaisyaWeb.Controllers
{
    public class ToolController : BaseController
    {
        private readonly ILogger<ToolController> _logger;

        public ToolController(ILogger<ToolController> logger, IViewRenderService viewRenderService,
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
        /// 
        /// </summary>
        /// <param name="codeID"></param>
        /// <returns></returns>
        public async Task<IActionResult> SelectCodeIndex(int codeID = 0)
        {
            try
            {
                Models.SelectCodeDto dto = new() { m_CodeDataList = new(), };
                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                dto.m_Code = await api.M_CodeData(codeID);
                dto.m_CodeDataList = await api.M_Code_DataList(codeID);

                if (dto.m_Code == null) { throw new Exception("パラメーターエラー：" + codeID); }
                return await PartialViewAsJson("SelectCodeModal", dto, true);
            }
            catch (Exception ex)
            {
                return Json(new { errorMessage = ex.Message });
            }
        }
    }
}
