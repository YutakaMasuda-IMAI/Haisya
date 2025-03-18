using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// ログインユーザーロール情報を表すクラス
    /// </summary>
    public partial class MLoginUserRole
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
        /// ロール名
        /// </summary>
        public string RoleName { get; set; }
        
        /// <summary>
        /// 更新日
        /// </summary>
        public DateTime UpDate { get; set; }
        
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DelFlg { get; set; }
    }
}
