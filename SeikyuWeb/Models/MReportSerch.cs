using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// レポート検索情報を表すクラス
    /// </summary>
    public partial class MReportSerch
    {
        /// <summary>
        /// レポート検索ID
        /// </summary>
        public int ReportSerchId { get; set; }
        
        /// <summary>
        /// レポート番号
        /// </summary>
        public int ReportNumber { get; set; }
        
        /// <summary>
        /// レポート名
        /// </summary>
        public string ReportName { get; set; }
        
        /// <summary>
        /// 表示タイトル
        /// </summary>
        public string DisplayTitle { get; set; }
        
        /// <summary>
        /// CSVフラグ
        /// </summary>
        public int CsvFlg { get; set; }
        
        /// <summary>
        /// PDFフラグ
        /// </summary>
        public int PdfFlg { get; set; }
        
        /// <summary>
        /// CSVファイル名
        /// </summary>
        public string CsvFileName { get; set; }
    }
}
