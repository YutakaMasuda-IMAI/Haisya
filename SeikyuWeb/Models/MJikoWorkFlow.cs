using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 事故ワークフローを表します。
    /// </summary>
    public partial class MJikoWorkFlow
    {
        /// <summary>
        /// 事故ワークフローベースID
        /// </summary>
        public int JikoWorkFlowBaseId { get; set; }

        /// <summary>
        /// 事故ワークフロー名
        /// </summary>
        public string JikoWorkFlowName { get; set; }

        /// <summary>
        /// 削除フラグ
        /// </summary>
        public int DelFlg { get; set; }
    }
}
