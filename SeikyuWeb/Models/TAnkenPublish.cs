using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 案件の公開に関する情報を表します。
    /// </summary>
    public partial class TAnkenPublish
    {
        /// <summary>
        /// 案件ID
        /// </summary>
        public int AnkenId { get; set; }
        /// <summary>
        /// 公開グループID
        /// </summary>
        public int PublishGroupId { get; set; }
        /// <summary>
        /// 公開フラグ
        /// </summary>
        public bool PublishFlg { get; set; }
        /// <summary>
        /// 公開開始日時
        /// </summary>
        public DateTime? PublishFromDatetime { get; set; }
        /// <summary>
        /// 公開終了日時
        /// </summary>
        public DateTime? PublishToDatetime { get; set; }
    }
}
