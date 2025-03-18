using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace WebApplication.Data.ReportCommons
{
    /// <summary>
    /// クラスV_InvoiceListは共通レポートのモデルです
    /// </summary>
    [Keyless]
    [Table("V_InvoiceList")]
    public partial class V_InvoiceList
    {
        /// <summary>
        /// 顧客ID
        /// </summary>
        [JsonPropertyName("Customer_ID")]
        public int Customer_ID { get; set; }
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
        /// <summary>
        /// 繰越額
        /// </summary>
        [JsonPropertyName("Carried_Over")]
        public double Carried_Over { get; set; }
        /// <summary>
        /// 課税売上
        /// </summary>
        [JsonPropertyName("Taxable_Sales")]
        public double Taxable_Sales { get; set; }
        /// <summary>
        /// 非課税売上
        /// </summary>
        [JsonPropertyName("Non_Taxable_Sales")]
        public double Non_Taxable_Sales { get; set; }
        /// <summary>
        /// 消費税
        /// </summary>
        [JsonPropertyName("Consumption_Tax")]
        public double Consumption_Tax { get; set; }
        /// <summary>
        /// 総売上
        /// </summary>
        [JsonPropertyName("Total_Sales")]
        public double Total_Sales { get; set; }
        /// <summary>
        /// 挿入日時
        /// </summary>
        [JsonPropertyName("Insert_DateTime")]
        public DateTime Insert_DateTime { get; set; }
    }
}
