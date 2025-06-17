using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Services;
using System;
using WebApplication.Common;
using WebApplication.Model;
using Microsoft.AspNetCore.Http;

namespace WebApplication.Controllers
{
    /// <summary>
    /// 事故リストを管理するコントローラー
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AccidentListController : ControllerBase
    {
        private readonly IAccidentListService accidentListService;

        public AccidentListController(IAccidentListService accidentListService)
        {
            this.accidentListService = accidentListService;
        }

        /// <summary>
        /// 指定された条件に基づいて事故リストを取得します。
        /// </summary>
        /// <param name="companyId">会社のID。</param>
        /// <param name="userId">ユーザーのID。</param>
        /// <param name="jikoKubun">事故区分。</param>
        /// <param name="loginUser">ログインユーザーかどうかを示すブール値。</param>
        /// <param name="jikoDisplay">事故の表示名（オプション）。</param>
        /// <param name="fromDate">検索開始日（オプション）。</param>
        /// <param name="toDate">検索終了日（オプション）。</param>
        /// <param name="displayName">表示名（オプション）。</param>
        /// <param name="syabanNumber">車輛番号（オプション）。</param>
        /// <returns>指定された条件に基づいて取得した事故リスト。</returns>
        [HttpGet("GetAccidentList")]
        public async Task<IActionResult> GetAccidentList(
            int companyId,
            int userId,
            int jikoKubun,
            bool loginUser,
            string jikoDisplay = "",
            DateTime fromDate = default,
            DateTime toDate = default,
            string displayName = "",
            string syabanNumber = "")
        {
            try
            {
                List<AccidentListModel.AccidentListItem> list = await accidentListService.GetAccidentList(companyId, userId, jikoKubun, loginUser, jikoDisplay, fromDate, toDate, displayName, syabanNumber);
                return new OkObjectResult(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
        }

        /// <summary>
        /// 指定されたコードIDに基づいてM_Codeリストを取得します。
        /// </summary>
        /// <param name="codeId">取得するコードのID。</param>
        /// <returns>指定されたコードIDに基づいて取得したM_Codeリスト。</returns>
        [HttpGet("GetMCodeList")]
        public async Task<IActionResult> GetMCodeList(int codeId)
        {
            try
            {
                List<M_Code_Datum> code = await accidentListService.GetMCodeList(codeId);
                return new OkObjectResult(code);
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