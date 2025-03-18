using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 案件追加料金を表すクラス
    /// </summary>
    public partial class MAnkenExcharge
    {
        /// <summary>
        /// 項目ID
        /// </summary>
        public int KomokuId { get; set; }
        
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        
        /// <summary>
        /// サイズ
        /// </summary>
        public string Size { get; set; }
        
        /// <summary>
        /// 項目キー
        /// </summary>
        public string KomokuKey { get; set; }
        
        /// <summary>
        /// ソート順
        /// </summary>
        public int SortOrder { get; set; }
        
        /// <summary>
        /// 項目名
        /// </summary>
        public string KomokuName { get; set; }
        
        /// <summary>
        /// 項目名略称
        /// </summary>
        public string KomokuNameAbbr { get; set; }
        
        /// <summary>
        /// 標準追加料金
        /// </summary>
        public decimal StdExcharge { get; set; }
        
        /// <summary>
        /// 総追加料金
        /// </summary>
        public decimal GrossExcharge { get; set; }
        
        /// <summary>
        /// 更新日
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
