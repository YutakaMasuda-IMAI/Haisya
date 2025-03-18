using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 支払詳細情報を表します。
    /// </summary>
    public partial class TShitabaraiDetail
    {
        /// <summary>
        /// 支払ID
        /// </summary>
        public int ShitabaraiId { get; set; }
        /// <summary>
        /// 売上運賃ID
        /// </summary>
        public int UriageUnchinId { get; set; }
        /// <summary>
        /// 案件ID
        /// </summary>
        public int AnkenId { get; set; }
        /// <summary>
        /// 売上ID
        /// </summary>
        public int UriageId { get; set; }
        /// <summary>
        /// 日報ID
        /// </summary>
        public int NippouId { get; set; }
    }
}
