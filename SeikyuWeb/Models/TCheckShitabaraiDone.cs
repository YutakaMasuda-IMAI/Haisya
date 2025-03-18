using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// TCheckShitabaraiDone クラス
    /// </summary>
    public partial class TCheckShitabaraiDone
    {
        /// <summary>
        /// 支払チェックID
        /// </summary>
        public int CheckShitabaraiId { get; set; }

        /// <summary>
        /// チェック日時
        /// </summary>
        public DateTime CheckDatetime { get; set; }

        /// <summary>
        /// チェックユーザー
        /// </summary>
        public int CheckUser { get; set; }

        /// <summary>
        /// チェック結果
        /// </summary>
        public int CheckReault { get; set; }

        /// <summary>
        /// 変更フラグ
        /// </summary>
        public int? ChangeFlg { get; set; }

        /// <summary>
        /// 挿入日時
        /// </summary>
        public DateTime InsertDatetime { get; set; }

        /// <summary>
        /// 挿入ユーザー
        /// </summary>
        public int InsertUser { get; set; }

        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime UpdateDatetime { get; set; }

        /// <summary>
        /// 更新ユーザー
        /// </summary>
        public int UpdateUser { get; set; }
    }
}
