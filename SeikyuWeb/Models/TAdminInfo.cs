using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 管理情報を表します。
    /// </summary>
    public partial class TAdminInfo
    {
        /// <summary>
        /// 管理情報ID
        /// </summary>
        public int AdminInfoId { get; set; }
        /// <summary>
        /// 情報日時
        /// </summary>
        public DateTime InfoDatetime { get; set; }
        /// <summary>
        /// 情報終了日時
        /// </summary>
        public DateTime InfoEndDatetime { get; set; }
        /// <summary>
        /// 情報区分
        /// </summary>
        public int InfoKubun { get; set; }
        /// <summary>
        /// 情報タイトル
        /// </summary>
        public string InfoTitle { get; set; }
        /// <summary>
        /// 情報詳細
        /// </summary>
        public string InfoDetail { get; set; }
    }
}
