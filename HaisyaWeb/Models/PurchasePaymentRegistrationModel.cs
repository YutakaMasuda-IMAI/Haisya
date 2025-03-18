using HaisyaWeb.Dto;
using System.Collections.Generic;
using static HaisyaWeb.Models.ExpenseModel;

namespace HaisyaWeb.Models
{
    /// <summary>
    /// 仕入支払登録モデル（ローカル）
    /// </summary>
    public class PurchasePaymentRegistrationModel_Local : WebApplication.Model.PurchasePaymentRegistrationModel
    {
        /// <summary>
        /// 経費データモデル（ローカル）
        /// </summary>
        public class ExpenseDataModel_Local : ExpenseDataModel<
            T_Expense_Local,
            T_Expense_Payment_Local,
            M_CompanyDriver_Local,
            M_Syaryo_Local,
            T_Expense_Item_Local,
            M_Vender_Local,
            AccidentListItem_Local>
        { }

        /// <summary>
        /// データリストモデル
        /// </summary>
        public class DataListModel
        {
            /// <summary>
            /// 会社ID
            /// </summary>
            public int Company_ID { get; set; }

            /// <summary>
            /// 経費支払
            /// </summary>
            public List<T_Expense_Payment_Local> ExpensePayments { get; set; }

            /// <summary>
            /// 経費
            /// </summary>
            public T_Expense_Local Expense { get; set; }

            /// <summary>
            /// 乗務員
            /// </summary>
            public M_CompanyDriver_Local CompanyDriver { get; set; }

            /// <summary>
            /// 車輌
            /// </summary>
            public M_Syaryo_Local Syaryo { get; set; }

            /// <summary>
            /// 経費アイテム
            /// </summary>
            public List<T_Expense_Item_Local> ExpenseItems { get; set; }

            /// <summary>
            /// ベンダー
            /// </summary>
            public List<M_Vender_Local> Venders { get; set; }

            /// <summary>
            /// 可能な経費
            /// </summary>
            public List<T_Expense_Item_Local> PossibleExpenseItems { get; set; }

            /// <summary>
            /// 事故
            /// </summary>
            public AccidentListItem_Local Jiko { get; set; }

            /// <summary>
            /// 新しい経費であるか
            /// </summary>
            public bool IsNewExpense { get; set; }

            public SearchModelForExpenseList Search { get; set; }
        }

        /// <summary>
        /// 備考モデル
        /// </summary>
        public class BikouModel
        {
            /// <summary>
            /// 経費支払
            /// </summary>
            public T_Expense_Payment_Local ExpensePayment { get; set; }

            /// <summary>
            /// インデックス
            /// </summary>
            public int Index { get; set; }
        }

        /// <summary>
        /// インデックスモデル
        /// </summary>
        public class IndexModel
        {
            public int Expense_kubun { get; set; }
            public int Expense_id { get; set; }
            public string Vehicle_number { get; set; }
            public string Driver_code { get; set; }
            public string Driver_name { get; set; }
            public int Accident_year { get; set; }
        }
    }
}