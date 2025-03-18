using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 事故ワークフローのステータス情報を表します。
    /// </summary>
    public partial class TJikoWorkFlowStatus
    {
        /// <summary>
        /// 事故ワークフローステータスID
        /// </summary>
        public int JikoWorkFlowStatusId { get; set; }
        /// <summary>
        /// 事故ワークフローID
        /// </summary>
        public int JikoWorkFlowId { get; set; }
        /// <summary>
        /// 承認区分
        /// </summary>
        public int ApprovalKubun { get; set; }
        /// <summary>
        /// 承認日時
        /// </summary>
        public DateTime? ApprovalDatetime { get; set; }
        /// <summary>
        /// 承認ユーザーID
        /// </summary>
        public int ApprovalUserId { get; set; }
        /// <summary>
        /// 否認理由
        /// </summary>
        public string RejectReason { get; set; }
    }
}
