using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 事故の項目を表します。
    /// </summary>
    public partial class TJikoItem
    {
        /// <summary>
        /// 事故ID
        /// </summary>
        public int JikoId { get; set; }

        /// <summary>
        /// 事故項目ID
        /// </summary>
        public int JikoItemsId { get; set; }

        /// <summary>
        /// 事故項目の文字列値
        /// </summary>
        public string JikoItemsValString { get; set; }

        /// <summary>
        /// 事故項目の整数値
        /// </summary>
        public int? JikoItemsValInt { get; set; }

        /// <summary>
        /// 事故項目の浮動小数点値
        /// </summary>
        public double? JikoItemsValDouble { get; set; }

        /// <summary>
        /// 事故項目の金額値
        /// </summary>
        public decimal? JikoItemsValMoney { get; set; }

        /// <summary>
        /// 事故項目の日付値
        /// </summary>
        public DateTime? JikoItemsValDatetime { get; set; }
    }
}
