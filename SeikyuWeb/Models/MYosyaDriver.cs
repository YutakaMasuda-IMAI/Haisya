using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// MYosyaDriver クラスは、顧客のドライバー情報を管理します。
    /// </summary>
    public partial class MYosyaDriver
    {
        /// <summary>
        /// ドライバーID
        /// </summary>
        public int YosyaDriverId { get; set; }
        
        /// <summary>
        /// 支店ID
        /// </summary>
        public int YosyaBranchId { get; set; }
        
        /// <summary>
        /// 顧客ID
        /// </summary>
        public int YosyaId { get; set; }
        
        /// <summary>
        /// 区分
        /// </summary>
        public int YosyaKubun { get; set; }
        
        /// <summary>
        /// グループID
        /// </summary>
        public int GroupId { get; set; }
        
        /// <summary>
        /// 従業員番号
        /// </summary>
        public int? EmployeeNumber { get; set; }
        
        /// <summary>
        /// 姓
        /// </summary>
        public string LastName { get; set; }
        
        /// <summary>
        /// 名
        /// </summary>
        public string FirstName { get; set; }
        
        /// <summary>
        /// 表示名
        /// </summary>
        public string DisplayName { get; set; }
        
        /// <summary>
        /// 開始日
        /// </summary>
        public DateTime FromDate { get; set; }
        
        /// <summary>
        /// 終了日
        /// </summary>
        public DateTime? ToDate { get; set; }
        
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DelFlg { get; set; }
        
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
