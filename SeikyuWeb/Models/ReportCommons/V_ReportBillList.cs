using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SeikyuWeb.Models.ReportCommons
{
    /// <summary>
    /// クラスV_ReportBillListはbill pdf用のモデル
    /// </summary>
    [Keyless]
    [Table("V_ReportBillList")]
    public partial class V_ReportBillList
    {
        [JsonPropertyName("CustomerCode")]
        public string CustomerCode { get; set; }
        [JsonPropertyName("CustomerPost")]
        public string CustomerPost { get; set; }
        [JsonPropertyName("CustomerAddress1")]
        public string CustomerAddress1 { get; set; }
        [JsonPropertyName("CustomerAddress2")]
        public string CustomerAddress2 { get; set; }
        [JsonPropertyName("CustomerName")]
        public string CustomerName { get; set; }
        [JsonPropertyName("CompanyName")]
        public string CompanyName { get; set; }
        [JsonPropertyName("CompanyPost")]
        public string CompanyPost { get; set; }
        [JsonPropertyName("CompanyAddress")]
        public string CompanyAddress { get; set; }
        [JsonPropertyName("CompanyPhone")]
        public string CompanyPhone { get; set; }
        [JsonPropertyName("CompanyFax")]
        public string CompanyFax { get; set; }
        [JsonPropertyName("CompanyInfoBank")]
        public string CompanyInfoBank { get; set; }
        [JsonPropertyName("Day")]
        public DateTime Day { get; set; }
        [JsonPropertyName("SyabanNumber")]
        public string SyabanNumber { get; set; }
        [JsonPropertyName("SyasyuDisplay")]
        public string SyasyuDisplay { get; set; }
        [JsonPropertyName("StartAddress")]
        public string StartAddress { get; set; }
        [JsonPropertyName("EndAddress")]
        public string EndAddress { get; set; }
        [JsonPropertyName("WorkTime")]
        public string WorkTime { get; set; }
        [JsonPropertyName("ProductName")]
        public string ProductName { get; set; }
        [JsonPropertyName("Quantity")]
        public double Quantity { get; set; }
        [JsonPropertyName("Unit")]
        public string Unit { get; set; }
        [JsonPropertyName("Price")]
        public decimal Price { get; set; }
        [JsonPropertyName("FeeShip")]
        public decimal FeeShip { get; set; }
        [JsonPropertyName("FeeCar")]
        public decimal FeeCar { get; set; }
        [JsonPropertyName("TotalPrice")]
        public decimal TotalPrice { get; set; }
        [JsonPropertyName("Remark")]
        public string Remark { get; set; }
        [JsonPropertyName("SeikyuMonth")]
        public DateTime SeikyuMonth { get; set; }
        [JsonPropertyName("Seikyu_Previous")]
        public decimal Seikyu_Previous { get; set; }
        [JsonPropertyName("Received_Amount_This")]
        public decimal Received_Amount_This { get; set; }
        [JsonPropertyName("Discount")]
        public decimal Discount { get; set; }
        [JsonPropertyName("Balance_Forward")]
        public decimal Balance_Forward { get; set; }
        [JsonPropertyName("Taxable_Amount")]
        public decimal Taxable_Amount { get; set; }
        [JsonPropertyName("Tax_Amout")]
        public decimal Tax_Amout { get; set; }
        [JsonPropertyName("Seikyu_Total_Amount")]
        public decimal Seikyu_Total_Amount { get; set; }
    }
}