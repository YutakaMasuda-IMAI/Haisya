using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// ログインユーザー情報を表すクラス
    /// </summary>
    public partial class MLoginUser
    {
        /// <summary>
        /// ログインユーザーID
        /// </summary>
        public int LoginUserId { get; set; }
        
        /// <summary>
        /// ユーザーID
        /// </summary>
        public int UserId { get; set; }
        
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
        
        /// <summary>
        /// 配車Webライセンス
        /// </summary>
        public bool HaisyaWebLicense { get; set; }
        
        /// <summary>
        /// 請求Webライセンス
        /// </summary>
        public bool SeikyuWebLicense { get; set; }
        
        /// <summary>
        /// 連携Webライセンス
        /// </summary>
        public bool RenkeiWebLicense { get; set; }
    }
}
