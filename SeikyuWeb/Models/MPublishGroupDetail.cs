using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 公開グループ詳細クラス
    /// </summary>
    public partial class MPublishGroupDetail
    {
        /// <summary>
        /// 公開グループ詳細ID
        /// </summary>
        public int PublishGroupDetailId { get; set; }
        
        /// <summary>
        /// 公開グループID
        /// </summary>
        public int PublishGroupId { get; set; }
        
        /// <summary>
        /// 会社ID
        /// </summary>
        public int? ComanyId { get; set; }
        
        /// <summary>
        /// 支店ID
        /// </summary>
        public int? BranchId { get; set; }
    }
}
