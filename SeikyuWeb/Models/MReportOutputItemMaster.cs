using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// レポート出力項目マスタークラス
    /// </summary>
    public partial class MReportOutputItemMaster
    {
        /// <summary>
        /// レポート出力項目ID
        /// </summary>
        public int ReportOutputItemId { get; set; }
        
        /// <summary>
        /// レポート検索区分ID
        /// </summary>
        public int ReportSerchKubunId { get; set; }
        
        /// <summary>
        /// ソート順
        /// </summary>
        public int SortOrder { get; set; }
        
        /// <summary>
        /// 行順
        /// </summary>
        public int RowOrder { get; set; }
        
        /// <summary>
        /// 表示タイトル
        /// </summary>
        public string DisplayTitle { get; set; }
        
        /// <summary>
        /// 表示タイプ
        /// </summary>
        public string DisplayType { get; set; }
        
        /// <summary>
        /// 表示フォーマット
        /// </summary>
        public string DisplayFormat { get; set; }
        
        /// <summary>
        /// モデルプロパティ
        /// </summary>
        public string ModelProoerty { get; set; }
        
        /// <summary>
        /// 表示幅
        /// </summary>
        public double? DisplayWidth { get; set; }
        
        /// <summary>
        /// レポート項目フラグ
        /// </summary>
        public int ReportItemFlg { get; set; }
        
        /// <summary>
        /// ページフッター
        /// </summary>
        public string PageFotter { get; set; }
        
        /// <summary>
        /// レポートフッター
        /// </summary>
        public string ReportFotter { get; set; }
    }
}
