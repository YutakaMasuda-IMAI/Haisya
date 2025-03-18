using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// MUnit クラスは、単位情報を管理します。
    /// </summary>
    public partial class MUnit
    {
        /// <summary>
        /// 単位ID
        /// </summary>
        public int UnitId { get; set; }
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// ソート順
        /// </summary>
        public int? SortOrder { get; set; }
        /// <summary>
        /// 単位表示
        /// </summary>
        public string UnitDisplay { get; set; }
        /// <summary>
        /// 単位フォーマット
        /// </summary>
        public string UnitFormat { get; set; }
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public int? DelFlg { get; set; }
    }
}
