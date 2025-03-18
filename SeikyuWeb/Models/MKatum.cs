using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 型を表します。
    /// </summary>
    public partial class MKatum
    {
        /// <summary>
        /// 型ID
        /// </summary>
        public string KataId { get; set; }

        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// ソート順
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 型表示
        /// </summary>
        public string KataDisplay { get; set; }

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
