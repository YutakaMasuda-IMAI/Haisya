using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 日報滞在情報を表します。
    /// </summary>
    public partial class TNippouStay
    {
        /// <summary>
        /// 日報ID
        /// </summary>
        public int NippouId { get; set; }
        /// <summary>
        /// 日付
        /// </summary>
        public DateTime Day { get; set; }
        /// <summary>
        /// 開始日時
        /// </summary>
        public string StartDatetime { get; set; }
        /// <summary>
        /// 終了日時
        /// </summary>
        public string EndDatetime { get; set; }
        /// <summary>
        /// 開始市区名
        /// </summary>
        public string StartShikuName { get; set; }
        /// <summary>
        /// 終了市区名
        /// </summary>
        public string EndShikuName { get; set; }
        /// <summary>
        /// 開始地点名
        /// </summary>
        public string StartPointName { get; set; }
        /// <summary>
        /// 終了地点名
        /// </summary>
        public string EndPointName { get; set; }
        /// <summary>
        /// 区間時間
        /// </summary>
        public int? IntervalTime { get; set; }
        /// <summary>
        /// 手当
        /// </summary>
        public decimal Dllowance { get; set; }
        /// <summary>
        /// 登録日時
        /// </summary>
        public DateTime? InsertDatetime { get; set; }
        /// <summary>
        /// 登録ユーザー
        /// </summary>
        public int? InsertUser { get; set; }
        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime? UpdateDatetime { get; set; }
        /// <summary>
        /// 更新ユーザー
        /// </summary>
        public int? UpdateUser { get; set; }
    }
}
