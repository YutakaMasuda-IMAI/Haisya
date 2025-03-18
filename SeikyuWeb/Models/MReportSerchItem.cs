using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// レポート検索項目情報を表すクラス
    /// </summary>
    public partial class MReportSerchItem
    {
        /// <summary>
        /// レポート検索項目ID
        /// </summary>
        public int ReportSerchItemId { get; set; }
        
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
        /// 表示メッセージ
        /// </summary>
        public string DisplayMessage { get; set; }
        
        /// <summary>
        /// 選択ボタン
        /// </summary>
        public int SelectBtn { get; set; }
        
        /// <summary>
        /// 選択ボタンクリック時の動作
        /// </summary>
        public string SelectBtnOnClick { get; set; }
        
        /// <summary>
        /// 入力ボックス有効フラグ
        /// </summary>
        public int InputboxEnabled { get; set; }
        
        /// <summary>
        /// カレンダーフラグ
        /// </summary>
        public int CalenderFlg { get; set; }
        
        /// <summary>
        /// 入力ボックス幅
        /// </summary>
        public double InputboxWidth { get; set; }
        
        /// <summary>
        /// 最大長
        /// </summary>
        public int MaxLength { get; set; }
        
        /// <summary>
        /// 入力ボックスタイプ
        /// </summary>
        public string InputboxType { get; set; }
        
        /// <summary>
        /// 入力ボックスフォーマット
        /// </summary>
        public string InputboxFormart { get; set; }
        
        /// <summary>
        /// モデルプロパティ
        /// </summary>
        public string ModelProoerty { get; set; }
        
        /// <summary>
        /// 間表示
        /// </summary>
        public string BetweenDisplay { get; set; }
        
        /// <summary>
        /// 非NULLフラグ
        /// </summary>
        public int NotNullFlg { get; set; }
        
        /// <summary>
        /// デフォルト値
        /// </summary>
        public string DefaultVal { get; set; }
    }
}
