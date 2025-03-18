using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 日報通行料情報を表します。
    /// </summary>
    public partial class TNippouToll
    {
        /// <summary>
        /// 日報通行料ID
        /// </summary>
        public int NippouTollId { get; set; }
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
        /// 傭車支店ID
        /// </summary>
        public int YosyaBranchId { get; set; }
        /// <summary>
        /// 運行no
        /// </summary>
        public string 運行no { get; set; }
        /// <summary>
        /// 読取日
        /// </summary>
        public DateTime? 読取日 { get; set; }
        /// <summary>
        /// 事業所コード
        /// </summary>
        public int? 事業所cd { get; set; }
        /// <summary>
        /// 運行日
        /// </summary>
        public DateTime 運行日 { get; set; }
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
        /// 対象乗務員区分
        /// </summary>
        public int? 対象乗務員区分 { get; set; }
        /// <summary>
        /// 開始日時
        /// </summary>
        public DateTime? 開始日時 { get; set; }
        /// <summary>
        /// 終了日時
        /// </summary>
        public DateTime? 終了日時 { get; set; }
        /// <summary>
        /// 開始道路番号
        /// </summary>
        public string 開始道路番号 { get; set; }
        /// <summary>
        /// 開始道路名
        /// </summary>
        public string 開始道路名 { get; set; }
        /// <summary>
        /// 開始ETC番号
        /// </summary>
        public string 開始etc番号 { get; set; }
        /// <summary>
        /// 開始IC名
        /// </summary>
        public string 開始ic名 { get; set; }
        /// <summary>
        /// 終了道路番号
        /// </summary>
        public string 終了道路番号 { get; set; }
        /// <summary>
        /// 終了道路名
        /// </summary>
        public string 終了道路名 { get; set; }
        /// <summary>
        /// 終了ETC番号
        /// </summary>
        public string 終了etc番号 { get; set; }
        /// <summary>
        /// 終了IC名
        /// </summary>
        public string 終了ic名 { get; set; }
        /// <summary>
        /// 精算区分
        /// </summary>
        public int? 精算区分 { get; set; }
        /// <summary>
        /// 精算区分名
        /// </summary>
        public string 精算区分名 { get; set; }
        /// <summary>
        /// 料金
        /// </summary>
        public decimal? 料金 { get; set; }
        /// <summary>
        /// 走行距離
        /// </summary>
        public double? 走行距離 { get; set; }
        /// <summary>
        /// 高速車種区分
        /// </summary>
        public int? 高速車種区分 { get; set; }
        /// <summary>
        /// 高速車種区分名
        /// </summary>
        public string 高速車種区分名 { get; set; }
        /// <summary>
        /// 標準料金
        /// </summary>
        public decimal? 標準料金 { get; set; }
        /// <summary>
        /// 料金区分
        /// </summary>
        public int? 料金区分 { get; set; }
        /// <summary>
        /// 料金区分名
        /// </summary>
        public string 料金区分名 { get; set; }
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
