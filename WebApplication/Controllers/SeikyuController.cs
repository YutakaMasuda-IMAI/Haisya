using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApplication.Data;
using WebApplication.Model;

namespace WebApplication.Controllers
{
    /// <summary>
    /// 請求に関する操作を提供するコントローラークラス。
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SeikyuController : ControllerBase
    {
        private readonly ILogger<SeikyuController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly MapApiSettings _mapApiSettings;

        /// <summary>
        /// SeikyuControllerのコンストラクタ。
        /// </summary>
        /// <param name="logger">ロガーインスタンス。</param>
        /// <param name="context">データベースコンテキスト。</param>
        /// <param name="mapApiSettings">マップAPI設定。</param>
        public SeikyuController(ILogger<SeikyuController> logger, ApplicationDbContext context, IOptions<MapApiSettings> mapApiSettings)
        {
            _logger = logger;
            _context = context;
            _mapApiSettings = mapApiSettings.Value;
        }

        /// <summary>
        /// データベースプロバイダー名を取得します。
        /// </summary>
        /// <returns>データベースプロバイダー名。</returns>
        [HttpGet]
        public string Get()
        {
            return _context.Database.ProviderName;
        }
    }
}
