using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 事故の種類を表します。
    /// </summary>
    public partial class TJikoType
    {
        /// <summary>
        /// 事故ID
        /// </summary>
        public int JikoId { get; set; }

        /// <summary>
        /// 事故タイプID
        /// </summary>
        public int JikoTypeId { get; set; }

        /// <summary>
        /// 事故タイプの備考
        /// </summary>
        public string JikoTypeRemarks { get; set; }
    }
}
