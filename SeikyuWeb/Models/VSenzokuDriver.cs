using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 専属ドライバー情報を表すクラス
    /// </summary>
    public partial class VSenzokuDriver
    {
        /// <summary>
        /// 専属ドライバーID
        /// </summary>
        public int SenzokuDriverId { get; set; }
        /// <summary>
        /// 専属ID
        /// </summary>
        public int SenzokuId { get; set; }
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// ドライバーID
        /// </summary>
        public int DriverId { get; set; }
        /// <summary>
        /// 従業員番号
        /// </summary>
        public int? EmployeeNumber { get; set; }
        /// <summary>
        /// 表示名
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 車両管理ID
        /// </summary>
        public int? SyaryoManagementId { get; set; }
        /// <summary>
        /// 車種
        /// </summary>
        public string Syasyu { get; set; }
        /// <summary>
        /// 型
        /// </summary>
        public string Kata { get; set; }
        /// <summary>
        /// 車番番号
        /// </summary>
        public string SyabanNumber { get; set; }
        /// <summary>
        /// 車番
        /// </summary>
        public string Syaban { get; set; }
        /// <summary>
        /// 開始日
        /// </summary>
        public DateTime FromDate { get; set; }
        /// <summary>
        /// 終了日
        /// </summary>
        public DateTime? ToDate { get; set; }
        /// <summary>
        /// ドライバー車両ID
        /// </summary>
        public int DriverSyaryoId { get; set; }
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 顧客支店ID
        /// </summary>
        public int CustomerBranchId { get; set; }
        /// <summary>
        /// 顧客支店コード
        /// </summary>
        public string CustomerBranchCode { get; set; }
        /// <summary>
        /// 顧客支店名
        /// </summary>
        public string CustomerBranchName { get; set; }
        /// <summary>
        /// 顧客支店名略称
        /// </summary>
        public string CustomerBranchNameAbbr { get; set; }
        /// <summary>
        /// 顧客担当者ID
        /// </summary>
        public int KokyakuTantouId { get; set; }
        /// <summary>
        /// 担当者コード
        /// </summary>
        public string TantouCode { get; set; }
        /// <summary>
        /// 担当者名
        /// </summary>
        public string TantouName { get; set; }
        /// <summary>
        /// 担当者名略称
        /// </summary>
        public string TantouNameAbbr { get; set; }
        /// <summary>
        /// 担当者電話番号1
        /// </summary>
        public string TantouPhone1 { get; set; }
        /// <summary>
        /// 車両ID
        /// </summary>
        public int? SyaryoId { get; set; }
        /// <summary>
        /// 車種表示
        /// </summary>
        public string SyasyuDisplay { get; set; }
        /// <summary>
        /// 専属名
        /// </summary>
        public string SenzokuName { get; set; }
        /// <summary>
        /// 専属名略称
        /// </summary>
        public string SenzokuNameAbbr { get; set; }
    }
}
