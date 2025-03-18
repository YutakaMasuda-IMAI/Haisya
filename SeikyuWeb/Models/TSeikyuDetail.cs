using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 請求詳細を表します。
    /// </summary>
    public partial class TSeikyuDetail
    {
        /// <summary>
        /// 請求ID
        /// </summary>
        public int SeikyuId { get; set; }
        /// <summary>
        /// 売上運賃ID
        /// </summary>
        public int UriageUnchinId { get; set; }
        /// <summary>
        /// 請求詳細番号
        /// </summary>
        public int SeikyuDetaiiNo { get; set; }
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
