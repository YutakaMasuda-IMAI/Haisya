using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    /// <summary>
    /// 顧客請求情報を表します。
    /// </summary>
    [Table("M_Customer_Seikyu")]
    public partial class M_Customer_Seikyu
    {
        /// <summary>
        /// 顧客ID
        /// </summary>
        [Key]
        public string Customer_ID { get; set; }

        /// <summary>
        /// 請求運賃名
        /// </summary>
        public string SeikyuUnchin_Name { get; set; }

        /// <summary>
        /// 立替金名
        /// </summary>
        public string Tatekaekin_Name { get; set; }

        /// <summary>
        /// 割増1名
        /// </summary>
        public string Warimashi1_Name { get; set; }

        /// <summary>
        /// 割増2名
        /// </summary>
        public string Warimashi2_Name { get; set; }

        /// <summary>
        /// 割増3名
        /// </summary>
        public string Warimashi3_Name { get; set; }

        /// <summary>
        /// 割増4名
        /// </summary>
        public string Warimashi4_Name { get; set; }

        /// <summary>
        /// 割増5名
        /// </summary>
        public string Warimashi5_Name { get; set; }

        /// <summary>
        /// 請求合計名
        /// </summary>
        public string SeikyuTotal_Name { get; set; }

        /// <summary>
        /// 割増1表示
        /// </summary>
        public string Warimashi1_Visible { get; set; }

        /// <summary>
        /// 割増2表示
        /// </summary>
        public string Warimashi2_Visible { get; set; }

        /// <summary>
        /// 割増3表示
        /// </summary>
        public string Warimashi3_Visible { get; set; }

        /// <summary>
        /// 割増4表示
        /// </summary>
        public string Warimashi4_Visible { get; set; }

        /// <summary>
        /// 割増5表示
        /// </summary>
        public string Warimashi5_Visible { get; set; }

        /// <summary>
        /// 割増1計算
        /// </summary>
        public string Warimashi1_Calc { get; set; }

        /// <summary>
        /// 割増2計算
        /// </summary>
        public string Warimashi2_Calc { get; set; }

        /// <summary>
        /// 割増3計算
        /// </summary>
        public string Warimashi3_Calc { get; set; }

        /// <summary>
        /// 割増4計算
        /// </summary>
        public string Warimashi4_Calc { get; set; }

        /// <summary>
        /// 割増5計算
        /// </summary>
        public string Warimashi5_Calc { get; set; }

        /// <summary>
        /// 請求合計計算
        /// </summary>
        public string SeikyuTotal_Calc { get; set; }
    }
}
