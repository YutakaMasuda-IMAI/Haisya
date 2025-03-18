using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 経費支払い情報を表すクラス
    /// </summary>
    public partial class TExpensePayment
    {
        /// <summary>
        /// 経費支払いID
        /// </summary>
        public int ExpensePaymentId { get; set; }
        /// <summary>
        /// 経費ID
        /// </summary>
        public int ExpenseId { get; set; }
        /// <summary>
        /// ベンダーID
        /// </summary>
        public int VenderId { get; set; }
        /// <summary>
        /// 経費項目ID
        /// </summary>
        public int ExpenseItemId { get; set; }
        /// <summary>
        /// 経費日
        /// </summary>
        public DateTime ExpenseDay { get; set; }
        /// <summary>
        /// 経費金額
        /// </summary>
        public decimal ExpenseMoney { get; set; }
        /// <summary>
        /// 支払い金額
        /// </summary>
        public decimal PaymentMoney { get; set; }
        /// <summary>
        /// 支払い月
        /// </summary>
        public DateTime PaymentMonth { get; set; }
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 締め区分
        /// </summary>
        public int ShimeKubun { get; set; }
        /// <summary>
        /// 登録日時
        /// </summary>
        public DateTime InsertDatetime { get; set; }
        /// <summary>
        /// 登録ユーザー
        /// </summary>
        public int InsertUser { get; set; }
        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime UpdateDatetime { get; set; }
        /// <summary>
        /// 更新ユーザー
        /// </summary>
        public int UpdateUser { get; set; }
    }
}
