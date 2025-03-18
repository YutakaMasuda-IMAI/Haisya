using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// MYosyaDriverSyaryo クラスは、顧客のドライバーと車両の情報を管理します。
    /// </summary>
    public partial class MYosyaDriverSyaryo
    {
        /// <summary>
        /// ドライバー車両ID
        /// </summary>
        public int YosyaDriverSyaryoId { get; set; }
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
        /// 開始日
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 終了日
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// サイズ
        /// </summary>
        public string Size { get; set; }
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
        /// 備考
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DelFlg { get; set; }
    }
}
