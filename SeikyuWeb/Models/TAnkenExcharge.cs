using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 案件の追加料金に関する情報を表します。
    /// </summary>
    public partial class TAnkenExcharge
    {
        /// <summary>
        /// 案件ID
        /// </summary>
        public int AnkenId { get; set; }
        /// <summary>
        /// 案件の順序
        /// </summary>
        public int AnkenOrder { get; set; }
        /// <summary>
        /// 項目ID
        /// </summary>
        public int KomokuId { get; set; }
        /// <summary>
        /// 標準追加料金
        /// </summary>
        public decimal StdExcharge { get; set; }
        /// <summary>
        /// 総追加料金
        /// </summary>
        public decimal GrossExcharge { get; set; }
        /// <summary>
        /// 追加料金
        /// </summary>
        public decimal Excharge { get; set; }
    }
}
