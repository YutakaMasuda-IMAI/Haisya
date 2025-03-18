using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 会社ドライバーを表すクラス
    /// </summary>
    public partial class MCompanyDriver
    {
        /// <summary>
        /// ドライバーID
        /// </summary>
        public int DriverId { get; set; }
        
        /// <summary>
        /// 支店ID
        /// </summary>
        public int BranchId { get; set; }
        
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        
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
        /// 入社日
        /// </summary>
        public DateTime? NyusyaDate { get; set; }
        
        /// <summary>
        /// 業務開始日
        /// </summary>
        public DateTime? GyomuStartDate { get; set; }
        
        /// <summary>
        /// 退職日
        /// </summary>
        public DateTime? TaisyokuDate { get; set; }
        
        /// <summary>
        /// 更新日
        /// </summary>
        public DateTime UpDate { get; set; }
        
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DelFlg { get; set; }
        
        /// <summary>
        /// 電話番号1
        /// </summary>
        public string Phone1 { get; set; }
        
        /// <summary>
        /// 電話番号2
        /// </summary>
        public string Phone2 { get; set; }
        
        /// <summary>
        /// 住所1
        /// </summary>
        public string Address1 { get; set; }
        
        /// <summary>
        /// 住所2
        /// </summary>
        public string Address2 { get; set; }
        
        /// <summary>
        /// LINE ID
        /// </summary>
        public string LineId { get; set; }
    }
}
