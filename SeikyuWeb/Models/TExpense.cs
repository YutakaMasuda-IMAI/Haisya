using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 経費情報を表すクラス
    /// </summary>
    public partial class TExpense
    {
        /// <summary>
        /// 経費ID
        /// </summary>
        public int ExpenseId { get; set; }
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// 経費区分
        /// </summary>
        public int ExpenseKubun { get; set; }
        /// <summary>
        /// 事故ID
        /// </summary>
        public int JikoId { get; set; }
        /// <summary>
        /// 車両管理ID
        /// </summary>
        public int SyaryoManagementId { get; set; }
        /// <summary>
        /// ドライバーID
        /// </summary>
        public int DriverId { get; set; }
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
