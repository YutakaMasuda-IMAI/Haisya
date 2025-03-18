using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// レポート出力項目クラス
    /// </summary>
    public partial class MReportOutputItem
    {
        /// <summary>
        /// レポート検索区分ID
        /// </summary>
        public int ReportSerchKubunId { get; set; }
        
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        
        /// <summary>
        /// ユーザーID
        /// </summary>
        public int UserId { get; set; }
        
        /// <summary>
        /// レポート出力項目ID
        /// </summary>
        public int ReportOutputItemId { get; set; }
        
        /// <summary>
        /// ソート順
        /// </summary>
        public int SortOrder { get; set; }
        
        /// <summary>
        /// CSVタイトル
        /// </summary>
        public string CsvTitle { get; set; }
    }
}
