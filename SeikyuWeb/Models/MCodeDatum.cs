using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// コードデータを表すクラス
    /// </summary>
    public partial class MCodeDatum
    {
        /// <summary>
        /// コードID
        /// </summary>
        public int CodeId { get; set; }
        
        /// <summary>
        /// コードデータ
        /// </summary>
        public string CodeData { get; set; }
        
        /// <summary>
        /// ソート順
        /// </summary>
        public int SortOrder { get; set; }
        
        /// <summary>
        /// コード名
        /// </summary>
        public string CodeName { get; set; }
        
        /// <summary>
        /// コード名略称
        /// </summary>
        public string CodeNameAbbr { get; set; }
        
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
