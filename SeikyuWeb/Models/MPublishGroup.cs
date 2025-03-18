using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 公開グループクラス
    /// </summary>
    public partial class MPublishGroup
    {
        /// <summary>
        /// 公開グループID
        /// </summary>
        public int PublishGroupId { get; set; }
        
        /// <summary>
        /// 公開グループ名
        /// </summary>
        public string PublishGroupName { get; set; }
        
        /// <summary>
        /// ユーザーID
        /// </summary>
        public int? UserId { get; set; }
        
        /// <summary>
        /// 会社ID
        /// </summary>
        public int? CompanyId { get; set; }
        
        /// <summary>
        /// 支店ID
        /// </summary>
        public int? BranchId { get; set; }
        
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DelFlg { get; set; }
    }
}
