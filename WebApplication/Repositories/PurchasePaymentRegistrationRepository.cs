using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Data.Kintai;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    /// <summary>
    /// 購入支払い登録リポジトリ
    /// </summary>
    public class PurchasePaymentRegistrationRepository: IPurchasePaymentRegistrationRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContextKintai _contextKintai;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context">アプリケーションデータベースコンテキスト</param>
        /// <param name="contextKintai">勤怠データベースコンテキスト</param>
        public PurchasePaymentRegistrationRepository(ApplicationDbContext context, ApplicationDbContextKintai contextKintai)
        {
            _context = context;
            _contextKintai = contextKintai;
        }

        /// <summary>
        /// 指定された経費IDに基づいてT_Expenseを取得します。
        /// </summary>
        /// <param name="expenseId">取得対象の経費の一意の識別子。</param>
        /// <returns>指定されたIDに対応するT_Expenseオブジェクト、存在しない場合はnullを返します。</returns>
        public async Task<T_Expense> GetTExpense(int expenseId)
        {
            T_Expense tExpense = await _context.T_Expenses
                .Where(expense => expense.Expense_ID == expenseId)
                .FirstOrDefaultAsync();

            return tExpense;
        }

        /// <summary>
        /// 指定されたドライバーIDに基づいてT_Expenseを取得します。
        /// </summary>
        /// <param name="driverId">取得対象のドライバーの一意の識別子。</param>
        /// <returns>指定されたドライバーIDに対応するT_Expenseオブジェクト、存在しない場合はnullを返します。</returns>
        public async Task<T_Expense> GetTExpenseByDriverId(int driverId)
        {
            T_Expense tExpense = await _context.T_Expenses
                .Where(expense => expense.Driver_ID == driverId)
                .FirstOrDefaultAsync();

            return tExpense;
        }

        /// <summary>
        /// 指定された車輌管理IDに基づいてT_Expenseを取得します。
        /// </summary>
        /// <param name="syaryoManagementId">取得対象の車輌管理の一意の識別子。</param>
        /// <returns>指定された車輌管理IDに対応するT_Expenseオブジェクト、存在しない場合はnullを返します。</returns>
        public async Task<T_Expense> GetTExpenseBySyaryoManagementId(int syaryoManagementId)
        {
            T_Expense tExpense = await _context.T_Expenses
            .Where(expense => expense.SyaryoManagement_ID == syaryoManagementId)
            .FirstOrDefaultAsync();

            return tExpense;       
        }

        /// <summary>
        /// 指定された事故IDに基づいてT_Expenseを取得します。
        /// </summary>
        /// <param name="jikoId">取得対象の事故の一意の識別子。</param>
        /// <returns>指定された事故IDに対応するT_Expenseオブジェクト、存在しない場合はnullを返します。</returns>
        public async Task<T_Expense> GetTExpenseByJikoId(int jikoId)
        {
            T_Expense tExpense = await _context.T_Expenses
                .Where(expense => expense.Jiko_ID == jikoId)
                .FirstOrDefaultAsync();

            return tExpense;
        }

        /// <summary>
        /// 指定されたドライバーIDに基づいてM_CompanyDriverを取得します。
        /// </summary>
        /// <param name="driverId">取得対象のドライバーの一意の識別子。</param>
        /// <returns>指定されたドライバーIDに対応するM_CompanyDriverオブジェクト、存在しない場合はnullを返します。</returns>
        public async Task<M_CompanyDriver> GetDriverAsync(int driverId)
        {
            M_CompanyDriver driver = await _context.M_CompanyDrivers
                .Where(driver => driver.Driver_ID == driverId)
                .FirstOrDefaultAsync();

            return driver;
        }

        /// <summary>
        /// 指定された経費IDに基づいてT_Expense_Paymentのリストを取得します。
        /// </summary>
        /// <param name="expenseId">取得対象の経費の一意の識別子。</param>
        /// <returns>指定された経費IDに対応するT_Expense_Paymentのリストを返します。</returns>
        public async Task<List<T_Expense_Payment>> GetTExpensePayments(int expenseId)
        {
            List<T_Expense_Payment> tExpensePayments = await _context.T_Expense_Payments
                .Where(expense => expense.Expense_ID == expenseId)
                .ToListAsync();

            return tExpensePayments;
        }

        /// <summary>
        /// 指定された経費アイテムIDに基づいてT_Expense_Itemを取得します。
        /// </summary>
        /// <param name="expenseItemId">取得対象の経費アイテムの一意の識別子。</param>
        /// <returns>指定された経費アイテムIDに対応するT_Expense_Itemオブジェクト、存在しない場合はnullを返します。</returns>
        public async Task<T_Expense_Item> GetTExpenseItem(int expenseItemId)
        {
            T_Expense_Item tExpensePayment = await _context.T_Expense_Items
                .Where(expense => expense.Expense_Item_ID == expenseItemId)
                .FirstOrDefaultAsync();

            return tExpensePayment;
        }

        /// <summary>
        /// 指定された車両管理IDに基づいてM_SyaryoManagementを取得します。
        /// </summary>
        /// <param name="syaryoManagementId">取得対象の車両管理の一意の識別子。</param>
        /// <returns>指定された車両管理IDに対応するM_SyaryoManagementオブジェクト、存在しない場合はnullを返します。</returns>
        public async Task<M_SyaryoManagement> GetMSyaryoManagement(int syaryoManagementId)
        {
            M_SyaryoManagement syaryoManagement = await _context.M_SyaryoManagements
                .Where(syaryoManagement => syaryoManagement.SyaryoManagement_ID == syaryoManagementId)
                .FirstOrDefaultAsync();

            return syaryoManagement;
        }

        /// <summary>
        /// 指定された車輌IDに基づいてM_Syaryoを取得します。
        /// </summary>
        /// <param name="syaryoId">取得対象の車輌の一意の識別子。</param>
        /// <returns>指定された車輌IDに対応するM_Syaryoオブジェクト、存在しない場合はnullを返します。</returns>
        public async Task<M_Syaryo> GetMSyaryo(int syaryoId)
        {
            M_Syaryo syaryo = await _context.M_Syaryos
                .Where(syaryo => syaryo.Syaryo_ID == syaryoId)
                .FirstOrDefaultAsync();

            return syaryo;
        }

        /// <summary>
        /// 指定されたベンダーIDに基づいてM_Venderを取得します。
        /// </summary>
        /// <param name="venderId">取得対象のベンダーの一意の識別子。</param>
        /// <returns>指定されたベンダーIDに対応するM_Venderオブジェクト、存在しない場合はnullを返します。</returns>
        public async Task<M_Vender> GetMVender(int venderId)
        {
            M_Vender vender = await _context.M_Venders
                .Where(v => v.Vender_ID == venderId)
                .FirstOrDefaultAsync();

            return vender;
        }

        /// <summary>
        /// 指定された経費区分に基づいてT_Expense_Itemのリストを取得します。
        /// 削除フラグが0の経費項目のみ取得します。
        /// </summary>
        /// <param name="expenseKubun">取得対象の経費区分。</param>
        /// <returns>指定された経費区分に対応するT_Expense_Itemのリスト、存在しない場合は空のリストを返します。</returns>
        public async Task<List<T_Expense_Item>> GetTExpenseItemByKubun(int expenseKubun)
        {
            List<T_Expense_Item> tExpensePayment = await _context.T_Expense_Items
                .Where(expense => expense.Expense_Kubun == expenseKubun && expense.Del_Flg == 0)
                .OrderBy(expense => expense.Sort_Order) 
                .ToListAsync();

            if (tExpensePayment == null)
            {
                return new List<T_Expense_Item>();
            }

            return tExpensePayment;
        }

        /// <summary>
        /// 指定されたT_Expenseオブジェクトを更新します。
        /// 既存のデータが存在しない場合は新規に登録します。
        /// </summary>
        /// <param name="expense">更新対象のT_Expenseオブジェクト。</param>
        /// <returns>新規に登録された場合は新しいT_ExpenseのIDを返します。既存データの更新の場合は-1を返します。</returns>
        public async Task<int> UpdateTExpense(T_Expense expense)
        {
            T_Expense existingExpense = await _context.T_Expenses
                .FirstOrDefaultAsync(tExpense => tExpense.Expense_ID == expense.Expense_ID);

            // 新規登録
            if (existingExpense == null)
            {
                T_Expense newExpense = new T_Expense();

                newExpense.Company_ID = expense.Company_ID;

                if (expense.Expense_Kubun == 1)
                {
                    // ・Driver_ID＝Expense_Kubunが1：乗務員時、ヘッダー部で設定したDriver_ID
                    newExpense.Driver_ID = expense.Driver_ID;
                }

                // ・SyaryoManagement_ID＝Expense_Kubunが2：車輌時、ヘッダー部で設定したSyaryoManagement_ID
                if (expense.Expense_Kubun == 2)
                {
                    // Expense_Kubunが2：車輌時、ヘッダー部で設定したSyaryouManagement_IDを持つ
                    newExpense.SyaryoManagement_ID = expense.SyaryoManagement_ID;
                    newExpense.Driver_ID = expense.Driver_ID;
                }

                // ・Jiko_ID＝Expense_Kubunが3：事故時、ヘッダー部で設定したJiko_ID
                if (expense.Expense_Kubun == 3)
                {
                    newExpense.Jiko_ID = expense.Jiko_ID;
                }
                newExpense.Expense_Kubun = expense.Expense_Kubun;
                newExpense.Insert_Datetime = DateTime.Now;
                newExpense.Update_Datetime = DateTime.Now;
                newExpense.Update_User = expense.Update_User; 
                newExpense.Insert_User = expense.Update_User; 

                _context.T_Expenses.Add(newExpense);        
                
                await _context.SaveChangesAsync();

                return newExpense.Expense_ID;

            }

            await _context.SaveChangesAsync();

            return -1; 

        }

        /// <summary>
        /// 指定されたT_Expense_Paymentのリストを更新または新規登録します。
        /// リスト内の各項目が既存のデータであれば更新し、存在しない場合は新規に登録します。
        /// </summary>
        /// <param name="expensePayments">更新または登録対象のT_Expense_Paymentのリスト。</param>
        public async Task UpdateTExpensePayments(List<T_Expense_Payment> expensePayments)
        {
            
            foreach (var expensePayment in expensePayments)
            {
                T_Expense_Payment existingExpensePayment = await _context.T_Expense_Payments
                .FirstOrDefaultAsync(tExpensePayment => tExpensePayment.Expense_Payment_ID== expensePayment.Expense_Payment_ID);

                // 更新処理
                if (existingExpensePayment != null)
                {
                    existingExpensePayment.Expense_Item_ID = expensePayment.Expense_Item_ID;
                    existingExpensePayment.Vender_ID = expensePayment.Vender_ID;
                    existingExpensePayment.Expense_Day = expensePayment.Expense_Day;
                    existingExpensePayment.Expense_Money = expensePayment.Expense_Money;
                    existingExpensePayment.Payment_Money = expensePayment.Payment_Money;
                    existingExpensePayment.Payment_Month = expensePayment.Payment_Month;
                    existingExpensePayment.Remarks = expensePayment.Remarks;
                    existingExpensePayment.Shime_Kubun = expensePayment.Shime_Kubun;
                    existingExpensePayment.Update_Datetime = DateTime.Now; 
                    existingExpensePayment.Update_User = expensePayment.Update_User; 
                }
                // 新規登録
                else
                {
                    T_Expense_Payment newExpensePayment = new T_Expense_Payment();


                    newExpensePayment.Expense_ID = expensePayment.Expense_ID;
                    newExpensePayment.Expense_Item_ID = expensePayment.Expense_Item_ID;
                    newExpensePayment.Vender_ID = expensePayment.Vender_ID;
                    newExpensePayment.Expense_Day = expensePayment.Expense_Day;
                    newExpensePayment.Expense_Money = expensePayment.Expense_Money;
                    newExpensePayment.Payment_Money = expensePayment.Payment_Money;
                    newExpensePayment.Payment_Month = expensePayment.Payment_Month;
                    newExpensePayment.Remarks = expensePayment.Remarks;
                    newExpensePayment.Shime_Kubun = 0;
                    newExpensePayment.Insert_Datetime = DateTime.Now;
                    newExpensePayment.Update_Datetime = DateTime.Now;
                    newExpensePayment.Update_User = expensePayment.Update_User; 
                    newExpensePayment.Insert_User = expensePayment.Update_User;

                    _context.T_Expense_Payments.Add(newExpensePayment);
                }

                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 指定された事故IDに基づいてAccidentListItemを取得し、事故に関する詳細情報を表示します。
        /// 各フィールドは関連するテーブル(M_Code_Data, M_CompanyDriver, M_SyaryoManagement)から取得されます。
        /// </summary>
        /// <param name="jikoId">取得対象の事故の一意の識別子。</param>
        /// <returns>
        /// AccidentListItemオブジェクト。事故の詳細（事故番号、表示名、日付、区分名、ドライバー名、車両番号）を含みます。
        /// 指定された事故IDが存在しない場合、空のAccidentListItemを返します。
        /// </returns> 																	
        public async Task<AccidentListModel.AccidentListItem> GetAccidentListItem(int jikoId)
        {
            //         T_Expense．Jiko_IDに該当する　を表示																				
            // Jiko_No																				
            // Jiko_Display																				2024/8/23　追加
            // Jiko_Date																				
            // Jiko_Kubun　→M_Code：18で、M_Code_Data．Data_ID＝Jiko_KubunのCode_Nameを表示																				
            // Driver_ID　→M_CompanyDriver．Display_Nameを表示																				
            // SyaryouManagement_ID　→M_SyaryoManagement．Syaban_Numberを表示																				
            // SyaryouManagement_ID1　→M_SyaryoManagement．Syaban_Numberを表示			
            AccidentListModel.AccidentListItem accidentListItem = new AccidentListModel.AccidentListItem();

            T_Jiko jiko = await _context.T_Jikos
                .Where(j => j.Jiko_ID == jikoId)
                .FirstOrDefaultAsync();

            if (jiko != null)
            {
                accidentListItem.Jiko_ID = jiko.Jiko_ID;
                accidentListItem.Jiko_No = jiko.Jiko_No;
                accidentListItem.Jiko_Date = jiko.Jiko_Date;
                accidentListItem.Jiko_Display = jiko.Jiko_Display;
                M_Code_Datum codeData = await _context.M_Code_Data
                    .Where(j => j.Code_ID == 18)
                    .FirstOrDefaultAsync();
                // Jiko_Kubun　→M_Code：18で、M_Code_Data．Data_ID＝Jiko_KubunのCode_Nameを表示																			
                accidentListItem.Code_Name = codeData.Code_Name;
            }

            if (jiko != null && jiko.Driver_ID != 0)
            {
                M_CompanyDriver companyDriver = await _context.M_CompanyDrivers
                    .Where(j => j.Driver_ID == jiko.Driver_ID)
                    .FirstOrDefaultAsync();
                // Driver_ID　→M_CompanyDriver．Display_Nameを表示																				
                if(companyDriver != null) accidentListItem.Display_Name = companyDriver.Display_Name;
            }
            if (jiko != null && jiko.SyaryoManagement_ID != 0)
            {
                M_SyaryoManagement syaryoManagement = await _context.M_SyaryoManagements
                    .Where(j => j.SyaryoManagement_ID == jiko.SyaryoManagement_ID)
                    .FirstOrDefaultAsync();
                // SyaryouManagement_ID　→M_SyaryoManagement．Syaban_Numberを表示
                if(syaryoManagement != null) accidentListItem.Syaban_Number = syaryoManagement.Syaban_Number;
            }

            return accidentListItem;
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
            // データベーストランザクションの開始
            using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. `T_Expense`を非同期で更新し、更新されたExpenseのIDを取得
                int expenseId = await UpdateTExpense(expense);
                // 2. ExpenseのIDが正常に取得できた場合
                if (expenseId != -1)
                {
                    // 2-1. 各Expense Paymentに新しいExpense IDを設定
                    List<T_Expense_Payment> newExpensesPayments = new List<T_Expense_Payment>();
                    foreach (var expensepayment in expensePayments)
                    {
                        expensepayment.Expense_ID = expenseId;
                        newExpensesPayments.Add(expensepayment);
                    }
                    // 2-2. 更新されたExpense IDを持つExpense Paymentsを非同期で更新
                    await UpdateTExpensePayments(expensePayments);
                }
                else
                {
                    // 3. Expense IDが取得できなかった場合（エラー等）
                    // 元のExpense Paymentsをそのまま更新
                    await UpdateTExpensePayments(expensePayments);
                }

                // すべての変更をデータベースに保存
                await _context.SaveChangesAsync();

                // トランザクションのコミット
                await transaction.CommitAsync();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("UpdateExpenses:" + ex.Message);
                // エラーが発生した場合はトランザクションをロールバック
                await transaction.RollbackAsync();
                return false;
            }
            return true;
        }
    }

    /// <summary>
    /// 購入支払い登録リポジトリのインターフェース
    /// </summary>
    public interface IPurchasePaymentRegistrationRepository
    {
        Task<T_Expense> GetTExpense(int expenseId);
        Task<List<T_Expense_Payment>> GetTExpensePayments(int expenseId);
        Task<M_CompanyDriver> GetDriverAsync(int driverId);
        Task<M_SyaryoManagement> GetMSyaryoManagement(int syaryoManagementId);
        Task<M_Syaryo> GetMSyaryo(int syaryoId);
        Task<T_Expense_Item> GetTExpenseItem(int expenseItemId);
        Task<M_Vender> GetMVender(int venderId);
        Task<List<T_Expense_Item>> GetTExpenseItemByKubun(int expenseKubun);
        Task<int> UpdateTExpense(T_Expense expense);
        Task UpdateTExpensePayments(List<T_Expense_Payment> expensePayments);
        Task<T_Expense> GetTExpenseByDriverId(int driverId);
        Task<T_Expense> GetTExpenseByJikoId(int jikoId);
        Task<T_Expense> GetTExpenseBySyaryoManagementId(int syaryoManagementId);

        Task<AccidentListModel.AccidentListItem> GetAccidentListItem(int jikoId);
        Task<bool> UpdateExpenses(T_Expense expense, List<T_Expense_Payment> expensePayments);
    }
}