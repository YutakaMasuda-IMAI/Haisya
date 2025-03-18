using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 日報その他通行料情報を表します。
    /// </summary>
    public partial class TNippouTollOther
    {
        /// <summary>
        /// 日報その他通行料ID
        /// </summary>
        public int NippouTollOtherId { get; set; }
        /// <summary>
        /// 日報ID
        /// </summary>
        public int NippouId { get; set; }
        /// <summary>
        /// ソート順
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// ドライバー車両ID
        /// </summary>
        public int DriverSyaryoId { get; set; }
        /// <summary>
        /// ドライバーID
        /// </summary>
        public int DriverId { get; set; }
        /// <summary>
        /// 傭車ドライバー車両ID
        /// </summary>
        public int YosyaDriverSyaryoId { get; set; }
        /// <summary>
        /// 傭車ドライバーID
        /// </summary>
        public int YosyaDriverId { get; set; }
        /// <summary>
        /// 傭車ID
        /// </summary>
        public int YosyaBranchId { get; set; }
        /// <summary>
        /// 日付
        /// </summary>
        public DateTime Day { get; set; }
        /// <summary>
        /// 通行料区分
        /// </summary>
        public int TollKubun { get; set; }
        /// <summary>
        /// 開始地点名
        /// </summary>
        public string StartName { get; set; }
        /// <summary>
        /// 終了地点名
        /// </summary>
        public string EndName { get; set; }
        /// <summary>
        /// 通行料
        /// </summary>
        public decimal TollFee { get; set; }
        /// <summary>
        /// 距離
        /// </summary>
        public double? Distance { get; set; }
        /// <summary>
        /// 負担区分
        /// </summary>
        public int FutanKubun { get; set; }
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
