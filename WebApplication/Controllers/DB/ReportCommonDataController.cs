using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebApplication.Common;
using WebApplication.Data;
using WebApplication.Services;

namespace WebApplication.Controllers.DB
{
    /// <summary>
    /// ReportCommonControllerクラスは、共通レポートのコントローラーです。
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ReportCommonController : MyBaseController
    {
        private readonly ILogger<ReportCommonController> _logger;
        private readonly IReportCommonService _reportCommonService;

        public ReportCommonController(ILogger<ReportCommonController> logger, ApplicationDbContext context, IReportCommonService reportCommonService)
        {
            _logger = logger;
            _context = context;
            _reportCommonService = reportCommonService;
        }

        #region ReportCommonData

        /// <summary>
        /// Report_Serch_Kubun_Idに基づいて共通レポートデータを取得します。
        /// </summary>
        /// <param name="reportSearchKubunId">レポート検索区分ID</param>
        /// <param name="jsonData">レポートプロシージャのパラメータ用JSONデータ</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="userId">ユーザーID</param>
        /// <returns>
        /// 200: 共通レポートデータ
        /// 400: パラメータが不正です。
        /// 500: リクエスト処理中にエラーが発生しました。
        /// </returns>
        [HttpGet("{reportSearchKubunId}")]
        public async Task<IActionResult> GetDataReport(int reportSearchKubunId, string jsonData, int userId = 0, int companyId = 0)
        {
            try
            {
                Dto.ReportCommonDto data = await _reportCommonService.GetReportCommonAsync(reportSearchKubunId, jsonData, userId, companyId);
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
        }

        #endregion ReportCommonData
    }
}
