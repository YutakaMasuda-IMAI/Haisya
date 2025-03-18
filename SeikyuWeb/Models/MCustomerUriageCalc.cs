using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 顧客売上計算を表します。
    /// </summary>
    public partial class MCustomerUriageCalc
    {
        /// <summary>
        /// 顧客支店ID
        /// </summary>
        public int CustomerBranchId { get; set; }

        /// <summary>
        /// 請求運賃名
        /// </summary>
        public string SeikyuUnchinName { get; set; }

        /// <summary>
        /// 立替金名
        /// </summary>
        public string TatekaekinName { get; set; }

        /// <summary>
        /// 割増1名
        /// </summary>
        public string Warimashi1Name { get; set; }

        /// <summary>
        /// 割増2名
        /// </summary>
        public string Warimashi2Name { get; set; }

        /// <summary>
        /// 割増3名
        /// </summary>
        public string Warimashi3Name { get; set; }

        /// <summary>
        /// 割増4名
        /// </summary>
        public string Warimashi4Name { get; set; }

        /// <summary>
        /// 割増5名
        /// </summary>
        public string Warimashi5Name { get; set; }

        /// <summary>
        /// 請求合計名
        /// </summary>
        public string SeikyuTotalName { get; set; }

        /// <summary>
        /// 割増1表示
        /// </summary>
        public bool? Warimashi1Visible { get; set; }

        /// <summary>
        /// 割増2表示
        /// </summary>
        public bool? Warimashi2Visible { get; set; }

        /// <summary>
        /// 割増3表示
        /// </summary>
        public bool? Warimashi3Visible { get; set; }

        /// <summary>
        /// 割増4表示
        /// </summary>
        public bool? Warimashi4Visible { get; set; }

        /// <summary>
        /// 割増5表示
        /// </summary>
        public bool? Warimashi5Visible { get; set; }

        /// <summary>
        /// 割増1計算
        /// </summary>
        public string Warimashi1Calc { get; set; }

        /// <summary>
        /// 割増2計算
        /// </summary>
        public string Warimashi2Calc { get; set; }

        /// <summary>
        /// 割増3計算
        /// </summary>
        public string Warimashi3Calc { get; set; }

        /// <summary>
        /// 割増4計算
        /// </summary>
        public string Warimashi4Calc { get; set; }

        /// <summary>
        /// 割増5計算
        /// </summary>
        public string Warimashi5Calc { get; set; }

        /// <summary>
        /// 請求合計計算
        /// </summary>
        public string SeikyuTotalCalc { get; set; }
    }
}
