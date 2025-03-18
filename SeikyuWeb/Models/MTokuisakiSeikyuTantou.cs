using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// MTokuisakiSeikyuTantou クラスは、得意先の請求担当者情報を管理します。
    /// </summary>
    public partial class MTokuisakiSeikyuTantou
    {
        /// <summary>
        /// コード
        /// </summary>
        public int コード { get; set; }
        /// <summary>
        /// 担当者1
        /// </summary>
        public int? Tantou1 { get; set; }
        /// <summary>
        /// 担当者2
        /// </summary>
        public int? Tantou2 { get; set; }
        /// <summary>
        /// 担当者3
        /// </summary>
        public int? Tantou3 { get; set; }
        /// <summary>
        /// 担当者4
        /// </summary>
        public int? Tantou4 { get; set; }
        /// <summary>
        /// 担当者5
        /// </summary>
        public int? Tantou5 { get; set; }
    }
}
