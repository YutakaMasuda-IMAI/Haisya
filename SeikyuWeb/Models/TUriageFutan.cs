using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 売上負担情報を表します。
    /// </summary>
    public partial class TUriageFutan
    {
        /// <summary>
        /// 売上負担ID
        /// </summary>
        public int UriageFutanId { get; set; }
        /// <summary>
        /// 売上ID
        /// </summary>
        public int UriageId { get; set; }
        /// <summary>
        /// ソート順
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// デフォルト区分
        /// </summary>
        public int DefaultKubun { get; set; }
        /// <summary>
        /// 負担区分
        /// </summary>
        public int FutanKubun { get; set; }
        /// <summary>
        /// 負担価格
        /// </summary>
        public decimal FutanPrice { get; set; }
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DelFlg { get; set; }
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
