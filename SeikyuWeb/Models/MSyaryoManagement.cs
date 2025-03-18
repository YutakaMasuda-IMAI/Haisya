using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// MSyaryoManagement クラスは、車両管理情報を管理します。
    /// </summary>
    public partial class MSyaryoManagement
    {
        /// <summary>
        /// 車両管理ID
        /// </summary>
        public int SyaryoManagementId { get; set; }
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// 支店ID
        /// </summary>
        public int BranchId { get; set; }
        /// <summary>
        /// 担当者ID
        /// </summary>
        public int TntouId { get; set; }
        /// <summary>
        /// グループID
        /// </summary>
        public int GroupId { get; set; }
        /// <summary>
        /// 運用開始日
        /// </summary>
        public DateTime? OpeDate { get; set; }
        /// <summary>
        /// 廃車日
        /// </summary>
        public DateTime? ScrapDate { get; set; }
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
        /// 登録日
        /// </summary>
        public DateTime? TourokuDate { get; set; }
        /// <summary>
        /// 初年度
        /// </summary>
        public string FirstYear { get; set; }
        /// <summary>
        /// 車名
        /// </summary>
        public string Syamei { get; set; }
        /// <summary>
        /// 車体番号
        /// </summary>
        public string SyataiNumber { get; set; }
        /// <summary>
        /// 車体モデル
        /// </summary>
        public string SyataiModel { get; set; }
        /// <summary>
        /// エンジンモデル
        /// </summary>
        public string EnginModel { get; set; }
        /// <summary>
        /// 車体形状
        /// </summary>
        public string SyataiShape { get; set; }
        /// <summary>
        /// 最大積載量
        /// </summary>
        public string MaxLoadCapa { get; set; }
        /// <summary>
        /// 基本装備品
        /// </summary>
        public string BaseEaseItem { get; set; }
        /// <summary>
        /// 車両重量
        /// </summary>
        public string SyaryoWeight { get; set; }
        /// <summary>
        /// 車両総重量
        /// </summary>
        public string SyaryoTotalWeight { get; set; }
        /// <summary>
        /// その他
        /// </summary>
        public string Etc { get; set; }
        /// <summary>
        /// 車両価格
        /// </summary>
        public decimal? SyaryoPrice { get; set; }
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks { get; set; }
    }
}
