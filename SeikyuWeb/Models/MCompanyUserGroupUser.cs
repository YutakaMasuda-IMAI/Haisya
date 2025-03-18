using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 会社ユーザーグループユーザーを表すクラス
    /// </summary>
    public partial class MCompanyUserGroupUser
    {
        /// <summary>
        /// グループID
        /// </summary>
        public int GroupId { get; set; }
        
        /// <summary>
        /// ユーザーID
        /// </summary>
        public int UserId { get; set; }
        
        /// <summary>
        /// デフォルトフラグ
        /// </summary>
        public bool DefaultFlg { get; set; }
        
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DelFlg { get; set; }
    }
}
