using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 会社ドライバー情報を表すクラス
    /// </summary>
    public partial class VCompanyDriver
    {
        /// <summary>
        /// 自社傭車区分
        /// </summary>
        public int JisyaYosyaKubun { get; set; }
        /// <summary>
        /// ドライバーID
        /// </summary>
        public int DriverId { get; set; }
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// 支店ID
        /// </summary>
        public int BranchId { get; set; }
        /// <summary>
        /// 支店名
        /// </summary>
        public string BranchName { get; set; }
        /// <summary>
        /// 支店名略称
        /// </summary>
        public string BranchNameAbbr { get; set; }
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
        /// <summary>
        /// ドライバー車両ID
        /// </summary>
        public int? DriverSyaryoId { get; set; }
        /// <summary>
        /// 車両管理ID
        /// </summary>
        public int? SyaryoManagementId { get; set; }
        /// <summary>
        /// 車両管理ID1
        /// </summary>
        public int? SyaryoManagementId1 { get; set; }
        /// <summary>
        /// 開始日
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// 終了日
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// グループID
        /// </summary>
        public int? GroupId { get; set; }
        /// <summary>
        /// 車種区分ID
        /// </summary>
        public int? SyasyuKubunId { get; set; }
        /// <summary>
        /// 車両ID
        /// </summary>
        public int? SyaryoId { get; set; }
        /// <summary>
        /// 車種
        /// </summary>
        public string Syasyu { get; set; }
        /// <summary>
        /// 型
        /// </summary>
        public string Kata { get; set; }
        /// <summary>
        /// 車番地域
        /// </summary>
        public string SyabanChiiki { get; set; }
        /// <summary>
        /// 車番分類
        /// </summary>
        public string SyabanBunrui { get; set; }
        /// <summary>
        /// 車番カナ
        /// </summary>
        public string SyabanKana { get; set; }
        /// <summary>
        /// 車番番号
        /// </summary>
        public string SyabanNumber { get; set; }
        /// <summary>
        /// 車種表示
        /// </summary>
        public string SyasyuDisplay { get; set; }
        /// <summary>
        /// サイズ
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 型ソート
        /// </summary>
        public int? KataSort { get; set; }
        /// <summary>
        /// 型表示
        /// </summary>
        public string KataDisplay { get; set; }
        /// <summary>
        /// 区分ソート
        /// </summary>
        public int? KubunSort { get; set; }
        /// <summary>
        /// 区分名
        /// </summary>
        public string KubunName { get; set; }
        /// <summary>
        /// 配車グループソート順
        /// </summary>
        public int? HaisyaGroupSortOrder { get; set; }
        /// <summary>
        /// 配車グループ名
        /// </summary>
        public string HaisyaGroupName { get; set; }
    }
}
