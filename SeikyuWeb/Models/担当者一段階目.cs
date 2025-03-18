using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 担当者の一段階目情報を表すクラス
    /// </summary>
    public partial class 担当者一段階目
    {
        /// <summary>
        /// コード
        /// </summary>
        public double? コード { get; set; }
        /// <summary>
        /// 略称
        /// </summary>
        public string 略称 { get; set; }
        /// <summary>
        /// 担当者
        /// </summary>
        public string 担当者 { get; set; }
        /// <summary>
        /// 検索2
        /// </summary>
        public double? 検索２ { get; set; }
        /// <summary>
        /// 検索3
        /// </summary>
        public double? 検索３ { get; set; }
    }
}
