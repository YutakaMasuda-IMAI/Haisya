using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApplication.Data;
using WebApplication.Model;

namespace WebApplication.Controllers
{
    /// <summary>
    /// 配車に関する操作を提供するコントローラークラス。
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HaisyaController : ControllerBase
    {
        private readonly ILogger<HaisyaController> _logger;

        private readonly ApplicationDbContext _context;

        private readonly MapApiSettings _mapApiSettings;

        /// <summary>
        /// HaisyaControllerのコンストラクタ。
        /// </summary>
        /// <param name="logger">ロガーインスタンス。</param>
        /// <param name="context">データベースコンテキスト。</param>
        /// <param name="mapApiSettings">マップAPI設定。</param>
        public HaisyaController(ILogger<HaisyaController> logger, ApplicationDbContext context, IOptions<MapApiSettings> mapApiSettings)
        {
            _logger = logger;
            _context = context;
            _mapApiSettings = mapApiSettings.Value;
        }
    }
}
