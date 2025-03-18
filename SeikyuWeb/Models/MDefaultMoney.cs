using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// デフォルト料金を表します。
    /// </summary>
    public partial class MDefaultMoney
    {
        /// <summary>
        /// エリア
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 車種サイズ
        /// </summary>
        public string SyasyuSize { get; set; }

        /// <summary>
        /// 開始距離
        /// </summary>
        public int FromDistance { get; set; }

        /// <summary>
        /// 終了距離
        /// </summary>
        public int ToDistance { get; set; }

        /// <summary>
        /// 間隔
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        /// 追加金額
        /// </summary>
        public decimal? AdditionAmount { get; set; }
    }
}
