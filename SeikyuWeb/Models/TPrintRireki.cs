using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 印刷履歴情報を表します。
    /// </summary>
    public partial class TPrintRireki
    {
        /// <summary>
        /// 印刷履歴ID
        /// </summary>
        public int PrintRirekiId { get; set; }
        /// <summary>
        /// 印刷日時
        /// </summary>
        public DateTime PrintDatetime { get; set; }
        /// <summary>
        /// 印刷ユーザーID
        /// </summary>
        public int PrintUserId { get; set; }
        /// <summary>
        /// 印刷区分
        /// </summary>
        public int PrintKubun { get; set; }
        /// <summary>
        /// データID
        /// </summary>
        public int DataId { get; set; }
        /// <summary>
        /// 登録日時
        /// </summary>
        public DateTime InsertDatetime { get; set; }
        /// <summary>
        /// 登録ユーザー
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
