using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// MSyaryoSize クラスは、車両サイズ情報を管理します。
    /// </summary>
    public partial class MSyaryoSize
    {
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// サイズ
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// ソート順
        /// </summary>
        public int SortOrder { get; set; }
        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime? UpDate { get; set; }
        /// <summary>
        /// 非表示フラグ
        /// </summary>
        public bool HiddenFlg { get; set; }
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DelFlg { get; set; }
    }
}
