using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Model;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    /// <summary>
    /// PurchasePaymentRegistrationServiceクラスは購入支払い登録サービスを提供します
    /// </summary>
    public class PurchasePaymentRegistrationService: IPurchasePaymentRegistrationService
    {
        private readonly IPurchasePaymentRegistrationRepository purchasePaymentRegistrationRepository;

        public PurchasePaymentRegistrationService(IPurchasePaymentRegistrationRepository purchasePaymentRegistrationRepository)
        {
            this.purchasePaymentRegistrationRepository = purchasePaymentRegistrationRepository;
        }

        /// <summary>
        /// 指定された経費IDに基づいて経費データを取得し、関連するドライバー、車輌、事故、および経費項目や支払い情報を含むExpenseDataModelを返します。
        /// 経費区分に応じて、表示するドライバー名、車輌情報、事故情報を選択します。
        /// また、関連する経費項目とベンダー情報も取得します。
        /// </summary>
        /// <param name="expenseId">取得対象の経費の一意の識別子。</param>
        /// <returns>
        /// ExpenseDataModelオブジェクト。経費情報、ドライバー、車輌、事故、支払い情報、経費項目およびベンダー情報を含みます。
        /// </returns>
        public async Task<PurchasePaymentRegistrationModel.ExpenseDataModel> GetExpenseData(int expenseId) 
        {
            PurchasePaymentRegistrationModel.ExpenseDataModel expenseDataModel = new PurchasePaymentRegistrationModel.ExpenseDataModel();

            T_Expense expense = await purchasePaymentRegistrationRepository.GetTExpense(expenseId);
            expenseDataModel.TExpense = expense;

            // ・T_Expense．Expense_Kubun＝1：乗務員時：																			
	        // T_Expense．Driver_IDに該当するM_CompanyDriver．Display_Nameを表示																		
            if (expense.Expense_Kubun == 1)
            {
                M_CompanyDriver companyDriver = await purchasePaymentRegistrationRepository.GetDriverAsync(expense.Driver_ID);
                expenseDataModel.MCompanyDriver = companyDriver;
            }
            // ・T_Expense．Expense_Kubun＝2：車輌時：																			
	        // T_Expense．SyaryoManagement_IDに該当するM_SyaryoManagement．Syaryo_IDのSyasyuDisplayを表示	
            if (expense.Expense_Kubun == 2)
            {
                M_SyaryoManagement syaryoManagement = await purchasePaymentRegistrationRepository.GetMSyaryoManagement(expense.SyaryoManagement_ID);
                if (syaryoManagement.Syaryo_ID.HasValue)
                {
                    M_Syaryo syaryo = await purchasePaymentRegistrationRepository.GetMSyaryo(syaryoManagement.Syaryo_ID.Value);
                    expenseDataModel.MSyaryo = syaryo;
                }
            }
            // ・T_Expense．Expense_Kubun＝3：事故時：																					
	        // T_Expense．Jiko_IDに該当する　を表示																				
            if (expense.Expense_Kubun == 3)
            {
                expenseDataModel.AccidentListItem = await purchasePaymentRegistrationRepository.GetAccidentListItem(expense.Jiko_ID);
            }

            List<T_Expense_Payment> expensePayments = await purchasePaymentRegistrationRepository.GetTExpensePayments(expense.Expense_ID);
            expenseDataModel.TExpensePayments = expensePayments;

            // T_Expense_Payment．Expense_Item_IDに該当するT_Expense_Item．Item_Displayを表示
            List<T_Expense_Item> expenseItems = new List<T_Expense_Item>();

            // T_Expense_Payment．Vender_IDに該当するM_Vender．Vender_Nameを表示
            List<M_Vender> venders = new List<M_Vender>();

            foreach (var expensePayment in expensePayments)
            {
                // 各expensePaymentに関連するT_Expense_Itemを非同期で取得し、expenseItemsリストに追加
                T_Expense_Item expenseItem = await purchasePaymentRegistrationRepository.GetTExpenseItem(expensePayment.Expense_Item_ID);
                if (expenseItem == null)
                    expenseItem = new T_Expense_Item();
                expenseItems.Add(expenseItem);

                // 各expensePaymentに関連するM_Venderを非同期で取得し、vendersリストに追加
                M_Vender vender = await purchasePaymentRegistrationRepository.GetMVender(expensePayment.Vender_ID);
                if (vender == null)
                    vender = new M_Vender();
                venders.Add(vender);
            }
            expenseDataModel.TExpenseItems = expenseItems;

            List<T_Expense_Item> possibleExpenseItems = await purchasePaymentRegistrationRepository.GetTExpenseItemByKubun(expense.Expense_Kubun);
            expenseDataModel.PossibleExpenseItems = possibleExpenseItems;

            expenseDataModel.MVenders = venders;

            return expenseDataModel;
        }

        /// <summary>
        /// 指定された経費区分に基づいて経費項目のリストを取得します。
        /// </summary>
        /// <param name="expenseKubun">経費区分を指定する整数値。経費区分に応じた経費項目を取得します。</param>
        /// <returns>
        /// 指定された経費区分に対応する経費項目のリスト。該当する項目がない場合は空のリストを返します。
        /// </returns>
        public async Task<List<T_Expense_Item>> GetTExpenseItemByKubun(int expenseKubun)
        {
            return await purchasePaymentRegistrationRepository.GetTExpenseItemByKubun(expenseKubun);
        }

        /// <summary>
        /// 費用とその関連する支払いをリポジトリで更新します。
        /// </summary>
        /// <param name="expense">更新する費用オブジェクト。</param>
        /// <param name="expensePayments">費用に関連する支払いオブジェクトのリスト。</param>
        /// <returns>非同期操作を表すタスク。</returns>
        /// <remarks>
        /// 費用の更新操作が成功した場合（すなわち、expenseId が -1 でない場合）、
        /// メソッドは新しい費用IDを各支払いに割り当て、リポジトリで支払いを更新します。
        /// 費用の更新が失敗した場合でも、支払いの更新をリポジトリで試みます。
        /// </remarks>
        public async Task<bool> UpdateExpenses(T_Expense expense, List<T_Expense_Payment> expensePayments)
        {
            bool result = await purchasePaymentRegistrationRepository.UpdateExpenses(expense, expensePayments);
            return result;
        }

        /// <summary>
        /// 指定された車両管理IDに基づいて車両情報を取得します。車輛管理IDに関連付けられた車両IDが存在する場合、その車両の詳細を返します。
        /// 車両IDが存在しない場合は、nullを返します。
        /// </summary>
        /// <param name="syaryoManagementId">取得対象の車両管理の一意の識別子。</param>
        /// <returns>
        /// 車両管理IDに関連する車両情報を含むM_Syaryoオブジェクト。関連する車両が存在しない場合はnullを返します。
        /// </returns>        
        public async Task<M_Syaryo> GetSyaryoBySyaryoManagementId(int syaryoManagementId)
        {
            M_SyaryoManagement syaryoManagement = await purchasePaymentRegistrationRepository.GetMSyaryoManagement(syaryoManagementId);
            if (syaryoManagement.Syaryo_ID.HasValue)
            {
                M_Syaryo syaryo = await purchasePaymentRegistrationRepository.GetMSyaryo(syaryoManagement.Syaryo_ID.Value);
                return syaryo;

            }
            return null;
        }

        /// <summary>
        /// ドライバーID、車両管理ID、または事故IDを使用して、T_Expense オブジェクトを取得します。
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
        public async Task<T_Expense> GetTExpenseByDriverIdOrSyaryoManagementId(int driverId, int syaryoManagementId, int jikoId)
        {
            // 初期値としてnullを設定
            T_Expense t_Expense = null;
            // ドライバーIDが指定されている場合、対応するT_Expenseを取得
            if (driverId != 0)
            {
                t_Expense = await purchasePaymentRegistrationRepository.GetTExpenseByDriverId(driverId);
            }
            // 車両管理IDが指定されている場合、対応するT_Expenseを取得
            else if(syaryoManagementId != 0)
            {
                t_Expense = await purchasePaymentRegistrationRepository.GetTExpenseBySyaryoManagementId(syaryoManagementId);
            } 
            // 事故IDが指定されている場合、対応するT_Expenseを取得
            else if (jikoId != 0)
            {
                t_Expense = await purchasePaymentRegistrationRepository.GetTExpenseByJikoId(jikoId);
            }
            // T_Expenseがnullの場合、新しい空のT_Expenseオブジェクトを返す
            if (t_Expense == null)
            {
                return new T_Expense();
            } 
            // それ以外の場合、取得したT_Expenseオブジェクトを返す
            else { return t_Expense; }

        }
    }

    /// <summary>
    /// インターフェース・クラス PurchasePaymentRegistrationService は購入支払い登録サービスを提供します
    /// </summary>
    public interface IPurchasePaymentRegistrationService
    {
        /// <summary>
        /// 指定された経費IDに基づいて経費データを取得し、関連するドライバー、車輌、事故、および経費項目や支払い情報を含むExpenseDataModelを返します。
        /// </summary>
        /// <param name="expenseId">取得対象の経費の一意の識別子。</param>
        /// <returns>
        /// ExpenseDataModelオブジェクト。経費情報、ドライバー、車輌、事故、支払い情報、経費項目およびベンダー情報を含みます。
        /// </returns>
        Task<PurchasePaymentRegistrationModel.ExpenseDataModel> GetExpenseData(int expenseId);

        /// <summary>
        /// 指定された経費区分に基づいて経費項目のリストを取得します。
        /// </summary>
        /// <param name="expenseKubun">経費区分を指定する整数値。経費区分に応じた経費項目を取得します。</param>
        /// <returns>
        /// 指定された経費区分に対応する経費項目のリスト。該当する項目がない場合は空のリストを返します。
        /// </returns>
        Task<List<T_Expense_Item>> GetTExpenseItemByKubun(int expenseKubun);

        /// <summary>
        /// 費用とその関連する支払いをリポジトリで更新します。
        /// </summary>
        /// <param name="expense">更新する費用オブジェクト。</param>
        /// <param name="expensePayments">費用に関連する支払いオブジェクトのリスト。</param>
        /// <returns>非同期操作を表すタスク。</returns>
        Task<bool> UpdateExpenses(T_Expense expense, List<T_Expense_Payment> expensePayments);

        /// <summary>
        /// 指定された車両管理IDに基づいて車両情報を取得します。
        /// </summary>
        /// <param name="syaryoManagementId">取得対象の車両管理の一意の識別子。</param>
        /// <returns>
        /// 車両管理IDに関連する車両情報を含むM_Syaryoオブジェクト。関連する車両が存在しない場合はnullを返します。
        /// </returns>
        Task<M_Syaryo> GetSyaryoBySyaryoManagementId(int syaryoManagementId);

        /// <summary>
        /// ドライバーID、車両管理ID、または事故IDを使用して、T_Expense オブジェクトを取得します。
        /// </summary>
        /// <param name="driverId">ドライバーのID。ドライバーIDが指定されている場合に使用します。</param>
        /// <param name="syaryoManagementId">車両管理のID。車両管理IDが指定されている場合に使用します。</param>
        /// <param name="jikoId">事故のID。事故IDが指定されている場合に使用します。</param>
        /// <returns>指定されたIDに基づいて取得した T_Expense オブジェクト。指定されたIDに該当する T_Expense が見つからない場合は、新しい T_Expense オブジェクトを返します。</returns>
        Task<T_Expense> GetTExpenseByDriverIdOrSyaryoManagementId(int driverId, int syaryoManagementId, int jikoId);
    }
}