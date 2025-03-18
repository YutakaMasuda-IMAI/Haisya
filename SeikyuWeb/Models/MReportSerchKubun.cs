using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// レポート検索区分情報を表すクラス
    /// </summary>
    public partial class MReportSerchKubun
    {
        /// <summary>
        /// レポート検索区分ID
        /// </summary>
        public int ReportSerchKubunId { get; set; }
        
        /// <summary>
        /// レポート検索ID
        /// </summary>
        public int ReportSerchId { get; set; }
        
        /// <summary>
        /// ソート順
        /// </summary>
        public int SortOrder { get; set; }
        
        /// <summary>
        /// 表示タイトル
        /// </summary>
        public string DisplayTitle { get; set; }
        
        /// <summary>
        /// レポートHTML
        /// </summary>
        public string ReportHtml { get; set; }
        
        /// <summary>
        /// プロシージャ名
        /// </summary>
        public string ProcName { get; set; }
        
        /// <summary>
        /// クラス名
        /// </summary>
        public string ClassName { get; set; }
        
        /// <summary>
        /// データソート
        /// </summary>
        public string DataSort { get; set; }
    }
}
