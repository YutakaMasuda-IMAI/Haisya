using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// エリアを表すクラス
    /// </summary>
    public partial class MArea
    {
        /// <summary>
        /// エリアID
        /// </summary>
        public int AreaId { get; set; }
        
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        
        /// <summary>
        /// エリア名
        /// </summary>
        public string Area { get; set; }
        
        /// <summary>
        /// ソート順
        /// </summary>
        public int SortOrder { get; set; }
    }
}
