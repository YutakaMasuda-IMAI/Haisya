using HaisyaWeb.Dto;
using HaisyaWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HaisyaWeb.API.WebApp
{
    class PurchasePaymentRegistrationApi : BaseHttpClient
    {
        public PurchasePaymentRegistrationApi(MapApiSettings mapApiSetting) => _baseUrl = mapApiSetting.Api.WebAPIHosts;

        /// <summary>
        /// 指定された費用IDに基づいて費用データを取得します。
        /// </summary>
        /// <param name="expenseId">取得する費用のID。</param>
        /// <returns>指定された費用IDに基づいて取得した <see cref="PurchasePaymentRegistrationModel_Local.ExpenseDataModel_Local"/> オブジェクト。</returns>
        /// <remarks>
        /// このメソッドは、指定された費用IDに基づいて費用データを非同期に取得します。
        /// 指定された URL を使用して HTTP リクエストを送信し、取得したデータを返します。
        /// </remarks>
        public async Task<PurchasePaymentRegistrationModel_Local.ExpenseDataModel_Local> GetExpenseData(int expenseId)
        {
            string url = _baseUrl + string.Format("PurchasePaymentRegistration/GetExpenseData?expenseId={0}", expenseId);
            //データ取得
            return await GetHttpData<PurchasePaymentRegistrationModel_Local.ExpenseDataModel_Local>(url);
        }

        /// <summary>
        /// 指定された費用区分に基づいて費用アイテムのリストを取得します。
        /// </summary>
        /// <param name="expenseKubun">取得する費用アイテムの区分。</param>
        /// <returns>指定された費用区分に基づいて取得した <see cref="List{T_Expense_Item_Local}"/> オブジェクトのリスト。</returns>
        /// <remarks>
        /// このメソッドは、指定された費用区分に基づいて費用アイテムを非同期に取得します。
        /// 指定された URL を使用して HTTP リクエストを送信し、取得したデータを返します。
        /// </remarks>
        public async Task<List<T_Expense_Item_Local>> GetTExpenseItemByKubun(int expenseKubun)
        {
            string url = _baseUrl + string.Format("PurchasePaymentRegistration/GetTExpenseItemByKubun?expenseKubun={0}", expenseKubun);
            //データ取得
            return await GetHttpData<List<T_Expense_Item_Local>>(url);
        }

        /// <summary>
        /// 費用と関連する支払いを登録します。
        /// </summary>
        /// <param name="expense">登録する費用オブジェクト。</param>
        /// <param name="expenseItems">登録する費用支払いのリスト。</param>
        /// <returns>非同期操作を表すタスク。</returns>
        /// <remarks>
        /// このメソッドは、費用データと関連する支払いデータをモデルに設定し、指定された URL を使用して HTTP POST リクエストを送信して登録処理を行います。
        /// </remarks>
        public async Task<MsterDataCommonResultValDto_Local> RegisterExpenses(T_Expense_Local expense, List<T_Expense_Payment_Local> expenseItems)
        {
            PurchasePaymentRegistrationModel_Local.ExpenseDataModel_Local model = new PurchasePaymentRegistrationModel_Local.ExpenseDataModel_Local
            {
                TExpense = expense,
                TExpensePayments = expenseItems
            };

            string url = _baseUrl + string.Format("PurchasePaymentRegistration/UpdateExpenses?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData(model, url);
        }

        /// <summary>
        /// 指定された車両管理IDに基づいて車両データを取得します。
        /// </summary>
        /// <param name="syaryoManagementId">車両管理のID。</param>
        /// <returns>指定された車両管理IDに基づいて取得した <see cref="M_Syaryo_Local"/> オブジェクト。</returns>
        /// <remarks>
        /// このメソッドは、指定された車両管理IDに基づいて車両データを非同期に取得します.
        /// 指定された URL を使用して HTTP リクエストを送信し、取得したデータを返します.
        /// </remarks>
        public async Task<M_Syaryo_Local> GetSyaryoBySyaryoManagementId(int syaryoManagementId)
        {
            string url = _baseUrl + string.Format("PurchasePaymentRegistration/GetSyaryoBySyaryoManagementId?syaryoManagementId={0}", syaryoManagementId);
            //データ取得
            return await GetHttpData<M_Syaryo_Local>(url);
        }

        /// <summary>
        /// 指定されたドライバーID、車両管理ID、または事故IDに基づいて費用データを取得します.
        /// </summary>
        /// <param name="driverId">ドライバーのID。</param>
        /// <param name="syaryoManagementId">車両管理のID。</param>
        /// <param name="jikoId">事故のID。</param>
        /// <returns>指定されたIDに基づいて取得した <see cref="T_Expense_Local"/> オブジェクト。</returns>
        /// <remarks>
        /// このメソッドは、指定されたドライバーID、車両管理ID、または事故IDに基づいて費用データを非同期に取得します.
        /// 指定された URL を使用して HTTP リクエストを送信し、取得したデータを返します.
        /// </remarks>
        public async Task<T_Expense_Local> GetTExpenseByDriverIdOrSyaryoManagementId(int driverId, int syaryoManagementId, int jikoId)
        {
            string url = _baseUrl + string.Format("PurchasePaymentRegistration/GetTExpenseByDriverIdOrSyaryoManagementId?driverId={0}&syaryoManagementId={1}&jikoId={2}", driverId, syaryoManagementId, jikoId);
            //データ取得
            return await GetHttpData<T_Expense_Local>(url);
        }
    }
}
