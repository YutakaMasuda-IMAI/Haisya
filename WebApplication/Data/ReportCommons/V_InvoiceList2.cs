using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace WebApplication.Data.ReportCommons
{
    /// <summary>
    /// クラスV_InvoiceList2は共通レポートのモデルです
    /// </summary>
    [Keyless]
    [Table("V_InvoiceList2")]
    public partial class V_InvoiceList2
    {
        /// <summary>
        /// 顧客ID1
        /// </summary>
        [JsonPropertyName("Customer_ID1")]
        public int Customer_ID1 { get; set; }
        /// <summary>
        /// 顧客名
        /// </summary>
        [JsonPropertyName("Customer_Name")]
        public string Customer_Name { get; set; }
        /// <summary>
        /// 今回請求額
        /// </summary>
        [JsonPropertyName("Current_Billing")]
        public double Current_Billing { get; set; }
        /// <summary>
        /// 前回残高
        /// </summary>
        [JsonPropertyName("Previous_Balance")]
        public double Previous_Balance { get; set; }
        /// <summary>
        /// 今回支払額
        /// </summary>
        [JsonPropertyName("Current_Payment")]
        public double Current_Payment { get; set; }
    }
}
