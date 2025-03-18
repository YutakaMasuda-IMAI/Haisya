using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// バッチ結果を表すクラス
    /// </summary>
    public partial class TBatchResult
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// バッチ名
        /// </summary>
        public string BatchName { get; set; }

        /// <summary>
        /// 終了時間
        /// </summary>
        public DateTime? ExitTime { get; set; }

        /// <summary>
        /// 結果
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// メッセージ
        /// </summary>
        public string Message { get; set; }
    }
}
