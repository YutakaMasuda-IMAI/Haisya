using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 支払変更を表すクラス
    /// </summary>
    public partial class TCheckShitabaraiChange
    {
        /// <summary>
        /// 支払ID
        /// </summary>
        public int CheckShitabaraiId { get; set; }

        /// <summary>
        /// 売上支払ID
        /// </summary>
        public int UriageShiharaiId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public double? Qty { get; set; }

        /// <summary>
        /// 単位
        /// </summary>
        public int? Unit { get; set; }

        /// <summary>
        /// 単価
        /// </summary>
        public decimal? UnitPrice { get; set; }

        /// <summary>
        /// 計算価格
        /// </summary>
        public decimal? CalcPrice { get; set; }

        /// <summary>
        /// 支払運賃
        /// </summary>
        public decimal? ShiharaiUnchin { get; set; }

        /// <summary>
        /// 立替金
        /// </summary>
        public decimal? Tatekaekin { get; set; }

        /// <summary>
        /// 割増1
        /// </summary>
        public decimal? Warimashi1 { get; set; }

        /// <summary>
        /// 割増2
        /// </summary>
        public decimal? Warimashi2 { get; set; }

        /// <summary>
        /// 割増3
        /// </summary>
        public decimal? Warimashi3 { get; set; }

        /// <summary>
        /// 割増4
        /// </summary>
        public decimal? Warimashi4 { get; set; }

        /// <summary>
        /// 割増5
        /// </summary>
        public decimal? Warimashi5 { get; set; }

        /// <summary>
        /// 支払合計
        /// </summary>
        public decimal? ShiharaiTotal { get; set; }

        /// <summary>
        /// 挿入日時
        /// </summary>
        public DateTime InsertDatetime { get; set; }

        /// <summary>
        /// 挿入ユーザー
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
