using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// コードを表すクラス
    /// </summary>
    public partial class MCode
    {
        /// <summary>
        /// コードID
        /// </summary>
        public int CodeId { get; set; }
        
        /// <summary>
        /// コード名
        /// </summary>
        public string CodeName { get; set; }
        
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
