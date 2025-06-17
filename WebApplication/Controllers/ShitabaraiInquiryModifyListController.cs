using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApplication.Common;
using WebApplication.Data;
using WebApplication.Model;
using WebApplication.Services;


namespace WebApplication.Controllers
{
    /// <summary>
    /// 下払い問合せ変更リストコントローラー
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ShitabaraiInquiryModifyListController : ControllerBase
    {
        private readonly ILogger<ShitabaraiInquiryModifyListController> _logger;

        private readonly ApplicationDbContext _context;

        private readonly MapApiSettings _mapApiSettings;

        private readonly IShitabaraiInquiryModifyListService _shitabaraiService;

        public ShitabaraiInquiryModifyListController(ILogger<ShitabaraiInquiryModifyListController> logger, ApplicationDbContext context, IOptions<MapApiSettings> mapApiSettings, IShitabaraiInquiryModifyListService shitabaraiService)
        {
            _logger = logger;
            _context = context;
            _mapApiSettings = mapApiSettings.Value;
            _shitabaraiService = shitabaraiService;

        }

        /// <summary>
        /// データリストの取得
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="shiharaiNengetsu">支払年月</param>
        /// <param name="shimeDay">締め日</param>
        /// <param name="shiharaiTantou">支払担当</param>
        /// <param name="zeiKubun">税区分</param>
        /// <param name="inquiryStatus">問合せステータス</param>
        /// <param name="shiharaiChanged">支払変更</param>
        /// <param name="yosyasakiFrom">傭車先開始</param>
        /// <param name="yosyasakiTo">傭車先終了</param>
        /// <param name="checkShitabaraiId">チェック支払ID</param>
        /// <returns>データリスト</returns>
        [HttpGet("GetShitabaraiInquiryModifyList")]
        public async Task<IActionResult> GetShitabaraiCheckDataList(int CompanyID, DateTime? shiharaiNengetsu, int? shimeDay, string shiharaiTantou, int? zeiKubun, int? inquiryStatus, int? shiharaiChanged, string yosyasakiFrom, string yosyasakiTo, int? checkShitabaraiId)
        {
            try
            {
                System.Collections.Generic.List<V_ShitabaraiCheckDataList> shitabaraiCheckDataList = await _shitabaraiService.GetShitabaraiCheckDataList(CompanyID, shiharaiNengetsu, shimeDay, shiharaiTantou, zeiKubun, inquiryStatus, shiharaiChanged, yosyasakiFrom, yosyasakiTo, checkShitabaraiId);
                return new OkObjectResult(shitabaraiCheckDataList);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
        }
    }
}
