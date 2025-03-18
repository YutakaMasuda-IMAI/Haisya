using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// レポート詳細パラメータクラス
    /// </summary>
    public partial class MReportDetailParam
    {
        /// <summary>
        /// レポート検索区分ID
        /// </summary>
        public int ReportSerchKubunId { get; set; }
        
        /// <summary>
        /// ソート順
        /// </summary>
        public int SortOrder { get; set; }
        
        /// <summary>
        /// パラメータ名
        /// </summary>
        public string ParamName { get; set; }
        
        /// <summary>
        /// パラメータタイプ
        /// </summary>
        public string ParamType { get; set; }
        
        /// <summary>
        /// パラメータ値
        /// </summary>
        public string ParamVal { get; set; }
        
        /// <summary>
        /// パラメータフォーマット
        /// </summary>
        public string ParamFormat { get; set; }
        
        /// <summary>
        /// パラメータデフォルト
        /// </summary>
        public string ParamDefault { get; set; }
        
        /// <summary>
        /// パラメータモデルプロパティ
        /// </summary>
        public string ParamModelProoerty { get; set; }
    }
}
