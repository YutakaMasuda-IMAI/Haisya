using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 支払明細印刷情報を表します。
    /// </summary>
    public partial class TPrintShitabaraiDetail
    {
        /// <summary>
        /// 支払印刷ID
        /// </summary>
        public int PrintShitabaraiId { get; set; }
        /// <summary>
        /// データ区分
        /// </summary>
        public int DataKubun { get; set; }
        /// <summary>
        /// データソート
        /// </summary>
        public int DataSort { get; set; }
        /// <summary>
        /// 売上支払ID
        /// </summary>
        public int UriageShiharaiId { get; set; }
        /// <summary>
        /// 案件ID
        /// </summary>
        public int AnkenId { get; set; }
        /// <summary>
        /// 案件詳細ID
        /// </summary>
        public int AnkenIdDetail { get; set; }
        /// <summary>
        /// 表示日
        /// </summary>
        public DateTime? DisplayDate { get; set; }
        /// <summary>
        /// 車番
        /// </summary>
        public string Syaban { get; set; }
        /// <summary>
        /// 車種型名
        /// </summary>
        public string SyasyuKataName { get; set; }
        /// <summary>
        /// 積み
        /// </summary>
        public string Tsumi { get; set; }
        /// <summary>
        /// 卸し
        /// </summary>
        public string Oroshi { get; set; }
        /// <summary>
        /// 荷物
        /// </summary>
        public string Luggage { get; set; }
        /// <summary>
        /// 作業名
        /// </summary>
        public string WorkName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public double Qty { get; set; }
        /// <summary>
        /// 単位
        /// </summary>
        public int Unit { get; set; }
        /// <summary>
        /// 単価
        /// </summary>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// 計算価格
        /// </summary>
        public decimal CalcPrice { get; set; }
        /// <summary>
        /// 支払運賃
        /// </summary>
        public decimal ShiharaiUnchin { get; set; }
        /// <summary>
        /// 立替金
        /// </summary>
        public decimal Tatekaekin { get; set; }
        /// <summary>
        /// 割増1
        /// </summary>
        public decimal Warimashi1 { get; set; }
        /// <summary>
        /// 割増2
        /// </summary>
        public decimal Warimashi2 { get; set; }
        /// <summary>
        /// 割増3
        /// </summary>
        public decimal Warimashi3 { get; set; }
        /// <summary>
        /// 割増4
        /// </summary>
        public decimal Warimashi4 { get; set; }
        /// <summary>
        /// 割増5
        /// </summary>
        public decimal Warimashi5 { get; set; }
        /// <summary>
        /// 支払総額
        /// </summary>
        public decimal ShiharaiTotal { get; set; }
        /// <summary>
        /// 税区分
        /// </summary>
        public int? ZeiKubun { get; set; }
        /// <summary>
        /// 傭車ドライバーID
        /// </summary>
        public int YosyaDriverId { get; set; }
        /// <summary>
        /// 傭車支店ID
        /// </summary>
        public int YosyaBranchId { get; set; }
        /// <summary>
        /// 傭車名
        /// </summary>
        public string YosyaName { get; set; }
        /// <summary>
        /// 傭車ドライバー名
        /// </summary>
        public string YosyaDriverName { get; set; }
        /// <summary>
        /// 備考ID
        /// </summary>
        public int? RemarksId { get; set; }
        /// <summary>
        /// 備考
        /// </summary>
        public string Remaks { get; set; }
        /// <summary>
        /// 開始日
        /// </summary>
        public DateTime? FromDate { get; set; }
        /// <summary>
        /// 終了日
        /// </summary>
        public DateTime? ToDate { get; set; }
    }
}
