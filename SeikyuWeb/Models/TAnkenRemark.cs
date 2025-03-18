using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 案件の備考に関する情報を表します。
    /// </summary>
    public partial class TAnkenRemark
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
        /// 備考
        /// </summary>
        public string Remarks { get; set; }
    }
}
