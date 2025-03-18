using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// エリア県を表すクラス
    /// </summary>
    public partial class MAreaKen
    {
        /// <summary>
        /// 県
        /// </summary>
        public string Ken { get; set; }
        
        /// <summary>
        /// エリアID
        /// </summary>
        public int AreaId { get; set; }
        
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
    }
}
