using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 事故ワークフローのルートを表します。
    /// </summary>
    public partial class TJikoWorkFlowRoute
    {
        /// <summary>
        /// 事故ワークフローID
        /// </summary>
        public int JikoWorkFlowId { get; set; }

        /// <summary>
        /// 事故ID
        /// </summary>
        public int JikoId { get; set; }

        /// <summary>
        /// 事故ワークフローの順序
        /// </summary>
        public int JikoWorkFlowSort { get; set; }

        /// <summary>
        /// 事故ワークフローの表示名
        /// </summary>
        public string JikoWorkFlowDisplay { get; set; }

        /// <summary>
        /// 事故ワークフローグループID
        /// </summary>
        public int JikoWorkFlowGroupId { get; set; }
    }
}
