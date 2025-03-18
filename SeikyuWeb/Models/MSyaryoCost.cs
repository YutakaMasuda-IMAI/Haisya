using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// MSyaryoCost クラスは、車両コスト情報を管理します。
    /// </summary>
    public partial class MSyaryoCost
    {
        /// <summary>
        /// 車両ID
        /// </summary>
        public int SyaryoId { get; set; }
        /// <summary>
        /// 開始距離
        /// </summary>
        public int FromDistance { get; set; }
        /// <summary>
        /// 終了距離
        /// </summary>
        public int ToDistance { get; set; }
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// 車種
        /// </summary>
        public string Syasyu { get; set; }
        /// <summary>
        /// 型
        /// </summary>
        public string Kata { get; set; }
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
