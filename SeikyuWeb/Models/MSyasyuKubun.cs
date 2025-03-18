using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// MSyasyuKubun クラスは、車種区分情報を管理します。
    /// </summary>
    public partial class MSyasyuKubun
    {
        /// <summary>
        /// 車種区分ID
        /// </summary>
        public int SyasyuKubunId { get; set; }
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// サイズ
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 型ID
        /// </summary>
        public string KataId { get; set; }
        /// <summary>
        /// 区分
        /// </summary>
        public string Kubun { get; set; }
        /// <summary>
        /// ソート順
        /// </summary>
        public int SortOrder { get; set; }
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
