using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 顧客ドライバーを表すクラス
    /// </summary>
    public partial class MCustomerDriver
    {
        /// <summary>
        /// 顧客ドライバーID
        /// </summary>
        public int CustomerDriverId { get; set; }
        
        /// <summary>
        /// 顧客ID
        /// </summary>
        public int CustomerId { get; set; }
        
        /// <summary>
        /// 顧客支店ID
        /// </summary>
        public int CustomerBranchId { get; set; }
        
        /// <summary>
        /// 他社区分
        /// </summary>
        public int YosyaKubun { get; set; }
        
        /// <summary>
        /// グループID
        /// </summary>
        public int GroupId { get; set; }
        
        /// <summary>
        /// 社員番号
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
        /// 電話番号1
        /// </summary>
        public string Phone1 { get; set; }
        
        /// <summary>
        /// 電話番号2
        /// </summary>
        public string Phone2 { get; set; }
        
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
        /// 挿入日時
        /// </summary>
        public DateTime InsertDatetime { get; set; }
        
        /// <summary>
        /// 挿入ユーザー
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
