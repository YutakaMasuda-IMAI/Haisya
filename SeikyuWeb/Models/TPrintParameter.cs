using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 印刷パラメータ情報を表します。
    /// </summary>
    public partial class TPrintParameter
    {
        /// <summary>
        /// 印刷ID
        /// </summary>
        public int PrintId { get; set; }
        /// <summary>
        /// 特記事項
        /// </summary>
        public string Tokun { get; set; }
        /// <summary>
        /// 期限日
        /// </summary>
        public DateTime? LimitDate { get; set; }
        /// <summary>
        /// 印刷区分
        /// </summary>
        public int PrintKubun { get; set; }
        /// <summary>
        /// データID
        /// </summary>
        public int DataId { get; set; }
        /// <summary>
        /// 削除日時
        /// </summary>
        public DateTime? DelDatetime { get; set; }
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
