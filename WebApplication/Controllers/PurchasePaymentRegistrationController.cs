using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Common;
using WebApplication.Model;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    /// <summary>
    /// 購入支払い登録に関する操作を提供するコントローラークラス。
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PurchasePaymentRegistrationController : ControllerBase
    {
        private readonly ILogger<PurchasePaymentRegistrationController> _logger;
        private readonly IPurchasePaymentRegistrationService purchasePaymentRegistrationService;

        /// <summary>
        /// PurchasePaymentRegistrationControllerのコンストラクタ。
        /// </summary>
        /// <param name="logger">ロガーインスタンス。</param>
        /// <param name="purchasePaymentRegistrationService">購入支払い登録サービス。</param>
        public PurchasePaymentRegistrationController(ILogger<PurchasePaymentRegistrationController> logger, IPurchasePaymentRegistrationService purchasePaymentRegistrationService)
        {
            _logger = logger;
            this.purchasePaymentRegistrationService = purchasePaymentRegistrationService;
        }

        /// <summary>
        /// 指定された費用IDに基づいて費用データを取得します。
        /// </summary>
        /// <param name="expenseId">取得する費用のID。</param>
        /// <returns>指定された費用IDに基づいて取得した <see cref="PurchasePaymentRegistrationModel.ExpenseDataModel"/> オブジェクト。</returns>
        /// <remarks>
        /// このメソッドは、指定された費用IDに関連する費用データを非同期に取得します。
        /// </remarks> 
        [HttpGet("GetExpenseData")]
        public async Task<IActionResult> GetExpenseData(int expenseId)
        {
            try
            {
                PurchasePaymentRegistrationModel.ExpenseDataModel ExpenseData = await purchasePaymentRegistrationService.GetExpenseData(expenseId);
                return new OkObjectResult(ExpenseData);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
        }

        /// <summary>
        /// 指定された費用区分に基づいて費用アイテムのリストを取得します。
        /// </summary>
        /// <param name="expenseKubun">取得する費用アイテムの区分。</param>
        /// <returns>指定された費用区分に基づいて取得した <see cref="List{T_Expense_Item}"/> オブジェクトのリスト。</returns>
        /// <remarks>
        /// このメソッドは、指定された費用区分に関連する費用アイテムを非同期に取得します。
        /// </remarks>
        [HttpGet("GetTExpenseItemByKubun")]
        public async Task<IActionResult> GetTExpenseItemByKubun(int expenseKubun)
        {
            try
            {
                List<Data.T_Expense_Item> TExpenseItemByKubun = await purchasePaymentRegistrationService.GetTExpenseItemByKubun(expenseKubun);
                return new OkObjectResult(TExpenseItemByKubun);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
        }

        /// <summary>
        /// 費用データを更新します。
        /// </summary>
        /// <param name="jsonString">JSON文字列。</param>
        /// <returns>Dto.MsterDataCommonResultValDto</returns>
        /// <remarks>
        /// このメソッドは、リクエストボディに含まれる費用データモデルを使用して、費用および関連する支払いを非同期に更新します。
        /// </remarks>
        [HttpPost("UpdateExpenses")]
        public async Task<Dto.MsterDataCommonResultValDto> UpdateExpenses(string jsonString)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = await Request.ReadFormAsync();
                if (!form.ContainsKey("name"))
                {
                    throw new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                PurchasePaymentRegistrationModel.ExpenseDataModel dataDto = System.Text.Json.JsonSerializer.Deserialize<PurchasePaymentRegistrationModel.ExpenseDataModel>(abc);

                await purchasePaymentRegistrationService.UpdateExpenses(dataDto.TExpense, dataDto.TExpensePayments);
                resultVal.RetrunFlg = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = ex is BadHttpRequestException ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError;
                resultVal.ErrrMessage = ex.Message;
            }
            finally
            {
            }
            return resultVal;
        }

        /// <summary>
        /// 車両管理IDに基づいて M_Syaryo オブジェクトを取得します。
        /// </summary>
        /// <param name="syaryoManagementId">車両管理のID。</param>
        /// <returns>指定された車両管理IDに基づいて取得した M_Syaryo オブジェクト。</returns>
        /// <remarks>
        /// 車両管理IDを使用して、関連する M_Syaryo オブジェクトを非同期に取得します。
        /// </remarks>
        [HttpGet("GetSyaryoBySyaryoManagementId")]
        public async Task<IActionResult> GetSyaryoBySyaryoManagementId(int syaryoManagementId)
        {
            try
            {
                Data.M_Syaryo SyaryoBySyaryoManagementI = await purchasePaymentRegistrationService.GetSyaryoBySyaryoManagementId(syaryoManagementId);
                return new OkObjectResult(SyaryoBySyaryoManagementI);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
        }

        /// <summary>
        /// ドライバーID、車両管理ID、または事故IDに基づいて T_Expense オブジェクトを取得します。
        /// </summary>
        /// <param name="driverId">ドライバーのID。ドライバーIDが指定された場合に使用します。</param>
        /// <param name="syaryoManagementId">車両管理のID。車両管理IDが指定された場合に使用します。</param>
        /// <param name="jikoId">事故のID。事故IDが指定された場合に使用します。</param>
        /// <returns>指定されたIDに基づいて取得した T_Expense オブジェクト。指定されたIDに該当する T_Expense が見つからない場合は、新しい T_Expense オブジェクトを返します。</returns>
        /// <remarks>
        /// メソッドは、以下の優先順位で ID を使用して T_Expense オブジェクトを取得します:
        /// 1. ドライバーIDが指定されている場合、ドライバーIDに基づいて T_Expense を取得します。
        /// 2. 車両管理IDが指定されている場合、車両管理IDに基づいて T_Expense を取得します。
        /// 3. 事故IDが指定されている場合、事故IDに基づいて T_Expense を取得します。
        /// 指定されたいずれのIDにも該当する T_Expense が見つからない場合は、新しい T_Expense オブジェクトを返します。
        /// </remarks>
        [HttpGet("GetTExpenseByDriverIdOrSyaryoManagementId")]
        public async Task<IActionResult> GetTExpenseByDriverIdOrSyaryoManagementId(int driverId, int syaryoManagementId, int jikoId)
        {
            try
            {
                Data.T_Expense TExpenseByDriverIdOrSyaryoManagementId = await purchasePaymentRegistrationService.GetTExpenseByDriverIdOrSyaryoManagementId(driverId, syaryoManagementId, jikoId);
                return new OkObjectResult(TExpenseByDriverIdOrSyaryoManagementId);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
        }
    }
}
