using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// ログインユーザー顧客情報を表すクラス
    /// </summary>
    public partial class MLoginUserCustomer
    {
        /// <summary>
        /// ログインユーザー顧客ID
        /// </summary>
        public int LoginUserCustomerId { get; set; }
        
        /// <summary>
        /// 担当者ID
        /// </summary>
        public int TantouId { get; set; }
        
        /// <summary>
        /// ログインID
        /// </summary>
        public string LoginId { get; set; }
        
        /// <summary>
        /// パスワード
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// ロックフラグ
        /// </summary>
        public bool LockFlg { get; set; }
        
        /// <summary>
        /// ロール
        /// </summary>
        public int Role { get; set; }
        
        /// <summary>
        /// デフォルトエリア
        /// </summary>
        public string DefaultArea { get; set; }
        
        /// <summary>
        /// デフォルト車種
        /// </summary>
        public string DefaultSyasyu { get; set; }
        
        /// <summary>
        /// デフォルト型
        /// </summary>
        public string DefaultKata { get; set; }
        
        /// <summary>
        /// デフォルトグループ
        /// </summary>
        public int DefaultGroup { get; set; }
        
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
