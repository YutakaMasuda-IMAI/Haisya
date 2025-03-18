using System.Collections.Generic;
using WebApplication.Data;
using static WebApplication.Models.AccidentListModel;

#nullable enable

namespace WebApplication.Model
{
    /// <summary>
    /// 経費データ
    /// </summary>
    public class PurchasePaymentRegistrationModel
    {
        /// <summary>
        /// 経費データモデル
        /// </summary>
        public class ExpenseDataModel : ExpenseDataModel<T_Expense, T_Expense_Payment, M_CompanyDriver, M_Syaryo, T_Expense_Item, M_Vender, AccidentListItem> { }

        /// <summary>
        /// 仕入れ支払登録モデル
        /// </summary>
        public class ExpenseDataModel<ExpenseDataType, ExpensePaymentItemDataType, CompanyDriverDataType, SyaryoDataType, ExpenseItemDataType, VenderItemDataType, AccidentListDataType>
            where ExpenseDataType : T_Expense
            where ExpensePaymentItemDataType : T_Expense_Payment
            where CompanyDriverDataType : M_CompanyDriver
            where SyaryoDataType : M_Syaryo
            where ExpenseItemDataType : T_Expense_Item
            where VenderItemDataType : M_Vender
            where AccidentListDataType : AccidentListItem
        {
            /// <summary>
            /// 経費
            /// </summary>
            public ExpenseDataType? TExpense { get; set; }

            /// <summary>
            /// 経費・支払
            /// </summary>
            public List<ExpensePaymentItemDataType>? TExpensePayments { get; set; }

            /// <summary>
            /// 乗務員
            /// </summary>
            public CompanyDriverDataType? MCompanyDriver { get; set; }

            /// <summary>
            /// 車輌
            /// </summary>
            public SyaryoDataType? MSyaryo { get; set; }

            /// <summary>
            /// 経費アイテム
            /// </summary>
            public List<ExpenseItemDataType>? TExpenseItems { get; set; }

            /// <summary>
            /// 仕入業者
            /// </summary>
            public List<VenderItemDataType>? MVenders { get; set; }

            /// <summary>
            /// 可能な経費アイテム
            /// </summary>
            public List<ExpenseItemDataType>? PossibleExpenseItems { get; set; }

            /// <summary>
            /// 事故一覧アイテム
            /// </summary>
            public AccidentListDataType? AccidentListItem { get; set; }
        }
    }
}