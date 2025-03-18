using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// ロール情報を表すクラス
    /// </summary>
    public partial class MRole
    {
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        
        /// <summary>
        /// ロール
        /// </summary>
        public int Role { get; set; }
        
        /// <summary>
        /// コントローラー
        /// </summary>
        public string Controller { get; set; }
        
        /// <summary>
        /// アクション
        /// </summary>
        public string Action { get; set; }
        
        /// <summary>
        /// メソッド
        /// </summary>
        public string Method { get; set; }
        
        /// <summary>
        /// 有効フラグ
        /// </summary>
        public bool Enabled { get; set; }
    }
}
