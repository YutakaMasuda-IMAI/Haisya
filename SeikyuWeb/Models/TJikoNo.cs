using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 事故番号を表します。
    /// </summary>
    public partial class TJikoNo
    {
        /// <summary>
        /// 年度
        /// </summary>
        public int Nendo { get; set; }

        /// <summary>
        /// 番号
        /// </summary>
        public int No { get; set; }
    }
}
