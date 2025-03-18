using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 日報滞在デジタコ情報を表します。
    /// </summary>
    public partial class TNippouStayDegitako
    {
        /// <summary>
        /// 日報滞在デジタコID
        /// </summary>
        public int NippouStayDegiId { get; set; }
        /// <summary>
        /// 日報ID
        /// </summary>
        public int NippouId { get; set; }
        /// <summary>
        /// 工事ID
        /// </summary>
        public int KudgivtId { get; set; }
        /// <summary>
        /// 運行番号
        /// </summary>
        public string 運行no { get; set; }
        /// <summary>
        /// 読取日
        /// </summary>
        public DateTime? 読取日 { get; set; }
        /// <summary>
        /// 事業所名
        /// </summary>
        public string 事業所名 { get; set; }
        /// <summary>
        /// 車輌コード
        /// </summary>
        public int? 車輌cd { get; set; }
        /// <summary>
        /// 車輌名
        /// </summary>
        public string 車輌名 { get; set; }
        /// <summary>
        /// 乗務員コード
        /// </summary>
        public int? 乗務員cd { get; set; }
        /// <summary>
        /// 乗務員名
        /// </summary>
        public string 乗務員名 { get; set; }
        /// <summary>
        /// 開始日時
        /// </summary>
        public DateTime? 開始日時 { get; set; }
        /// <summary>
        /// イベント名
        /// </summary>
        public string イベント名 { get; set; }
        /// <summary>
        /// 終了日時
        /// </summary>
        public DateTime? 終了日時 { get; set; }
        /// <summary>
        /// 開始走行距離
        /// </summary>
        public double? 開始走行距離 { get; set; }
        /// <summary>
        /// 終了走行距離
        /// </summary>
        public double? 終了走行距離 { get; set; }
        /// <summary>
        /// 区間時間
        /// </summary>
        public int? 区間時間 { get; set; }
        /// <summary>
        /// 区間距離
        /// </summary>
        public double? 区間距離 { get; set; }
        /// <summary>
        /// 開始市町村名
        /// </summary>
        public string 開始市町村名 { get; set; }
        /// <summary>
        /// 終了市町村名
        /// </summary>
        public string 終了市町村名 { get; set; }
        /// <summary>
        /// 開始場所名
        /// </summary>
        public string 開始場所名 { get; set; }
        /// <summary>
        /// 終了場所名
        /// </summary>
        public string 終了場所名 { get; set; }
        /// <summary>
        /// 早朝深夜休憩
        /// </summary>
        public DateTime? 早朝深夜休憩 { get; set; }
    }
}
