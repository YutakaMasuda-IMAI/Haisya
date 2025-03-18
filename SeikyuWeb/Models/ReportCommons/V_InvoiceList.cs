using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SeikyuWeb.Models.ReportCommons
{
    /// <summary>
    /// クラスV_InvoiceListは共通レポート用のモデル
    /// </summary>
    [Keyless]
    [Table("V_InvoiceList")]
    public partial class V_InvoiceList
    {
        [JsonPropertyName("Customer_ID")]
        public int Customer_ID { get; set; }
        [JsonPropertyName("Customer_Name")]
        public string Customer_Name { get; set; }
        [JsonPropertyName("Current_Billing")]
        public double Current_Billing { get; set; }
        [JsonPropertyName("Previous_Balance")]
        public double Previous_Balance { get; set; }
        [JsonPropertyName("Current_Payment")]
        public double Current_Payment { get; set; }
        [JsonPropertyName("Carried_Over")]
        public double Carried_Over { get; set; }
        [JsonPropertyName("Taxable_Sales")]
        public double Taxable_Sales { get; set; }
        [JsonPropertyName("Non_Taxable_Sales")]
        public double Non_Taxable_Sales { get; set; }
        [JsonPropertyName("Consumption_Tax")]
        public double Consumption_Tax { get; set; }
        [JsonPropertyName("Total_Sales")]
        public double Total_Sales { get; set; }
        [JsonPropertyName("Insert_DateTime")]
        public DateTime Insert_DateTime { get; set; }
    }
}
