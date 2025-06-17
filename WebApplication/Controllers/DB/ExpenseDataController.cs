using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Common;
using WebApplication.Data;
using WebApplication.Model;

namespace WebApplication.Controllers.DB
{
    /// <summary>
    /// 経費データを管理するコントローラー
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ExpenseDataController : ControllerBase
    {
        private readonly ILogger<MasterDataController> _logger;
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// ExpenseDataControllerのコンストラクタ
        /// </summary>
        /// <param name="logger">ロガー</param>
        /// <param name="context">データベースコンテキスト</param>
        public ExpenseDataController(ILogger<MasterDataController> logger, ApplicationDbContext context)
            => (_logger, _context) = (logger, context);

        /// <summary>
        /// 経費データリストを取得します。
        /// </summary>
        /// <param name="company_id">会社ID</param>
        /// <param name="expense_category">経費カテゴリ</param>
        /// <param name="vehicle_number">車両番号</param>
        /// <param name="driver_code">ドライバーコード</param>
        /// <param name="driver_name">ドライバー名</param>
        /// <param name="accident_year">事故年</param>
        /// <returns>経費データリスト</returns>
        [HttpGet("GetExpenseDataList")]
        public async Task<IActionResult> GetExpenseDataList(
            int company_id,
            int expense_category,
            int? vehicle_number,
            string driver_code,
            string driver_name,
            int? accident_year)
        {
            IEnumerable<V_ExpenseDataList> r;
            using ExpenseDataModel m = new(_context);
            try
            {
                r = await m.GetExpenseDataList(company_id, expense_category, vehicle_number, driver_code, driver_name, accident_year);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {
            }
            return new OkObjectResult(r);
        }
    }
}
