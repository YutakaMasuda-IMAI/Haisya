using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 事故ワークフローのルートを表します。
    /// </summary>
    public partial class MJikoWorkFlowRoute
    {
        /// <summary>
        /// 事故ワークフローベースID
        /// </summary>
        public int JikoWorkFlowBaseId { get; set; }

        /// <summary>
        /// 事故ワークフローソート順
        /// </summary>
        public int JikoWorkFlowSort { get; set; }

        /// <summary>
        /// 事故ワークフロー表示
        /// </summary>
        public string JikoWorkFlowDisplay { get; set; }

        /// <summary>
        /// 事故ワークフローグループID
        /// </summary>
        public int JikoWorkFlowGroupId { get; set; }
    }
}
