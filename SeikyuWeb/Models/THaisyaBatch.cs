using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 配車バッチ情報を表すクラス
    /// </summary>
    public partial class THaisyaBatch
    {
        /// <summary>
        /// 配車バッチID
        /// </summary>
        public int HaisyaBatchId { get; set; }
        /// <summary>
        /// 配車ID
        /// </summary>
        public int HaisyaId { get; set; }
        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime UpdateDateTime { get; set; }
        /// <summary>
        /// バッチ終了時間
        /// </summary>
        public DateTime? BatchEndTime { get; set; }
        /// <summary>
        /// バッチ結果
        /// </summary>
        public string BatchResult { get; set; }
        /// <summary>
        /// バッチエラーメッセージ
        /// </summary>
        public string BatchErrorMsg { get; set; }
        /// <summary>
        /// 報告日時
        /// </summary>
        public DateTime? Reported { get; set; }
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks { get; set; }
    }
}
