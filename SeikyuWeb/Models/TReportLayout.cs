using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// レポートレイアウトを表します。
    /// </summary>
    public partial class TReportLayout
    {
        /// <summary>
        /// 印刷区分
        /// </summary>
        public int PrintKubun { get; set; }
        /// <summary>
        /// 税区分
        /// </summary>
        public int ZeiKubun { get; set; }
        /// <summary>
        /// レポートソート
        /// </summary>
        public int ReportSort { get; set; }
        /// <summary>
        /// レポート名
        /// </summary>
        public string ReportName { get; set; }
        /// <summary>
        /// レポート説明
        /// </summary>
        public string ReportExplan { get; set; }
        /// <summary>
        /// レポート備考
        /// </summary>
        public string ReportRemarks { get; set; }
        /// <summary>
        /// レポートHTML
        /// </summary>
        public string ReportHtml { get; set; }
        /// <summary>
        /// CSV出力フラグ
        /// </summary>
        public int CsvOutputFlg { get; set; }
        /// <summary>
        /// レポート検索ID
        /// </summary>
        public int ReportSerchId { get; set; }
        /// <summary>
        /// レポート検索区分ID
        /// </summary>
        public int ReportSerchKubunId { get; set; }
    }
}
