using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication.Data.ReportCommons
{
    /// <summary>
    /// クラスV_ReportBillListはbill pdf用のモデル
    /// </summary>
    [Keyless]
    [Table("V_ReportBillList")]
    public partial class V_ReportBillList
    {
        /// <summary>
        /// 顧客コード
        /// </summary>
        [JsonPropertyName("CustomerCode")]
        public string CustomerCode { get; set; }
        /// <summary>
        /// 顧客郵便番号
        /// </summary>
        [JsonPropertyName("CustomerPost")]
        public string CustomerPost { get; set; }
        /// <summary>
        /// 顧客住所1
        /// </summary>
        [JsonPropertyName("CustomerAddress1")]
        public string CustomerAddress1 { get; set; }
        /// <summary>
        /// 顧客住所2
        /// </summary>
        [JsonPropertyName("CustomerAddress2")]
        public string CustomerAddress2 { get; set; }
        /// <summary>
        /// 顧客名
        /// </summary>
        [JsonPropertyName("CustomerName")]
        public string CustomerName { get; set; }
        /// <summary>
        /// 会社名
        /// </summary>
        [JsonPropertyName("CompanyName")]
        public string CompanyName { get; set; }
        /// <summary>
        /// 会社郵便番号
        /// </summary>
        [JsonPropertyName("CompanyPost")]
        public string CompanyPost { get; set; }
        /// <summary>
        /// 会社住所
        /// </summary>
        [JsonPropertyName("CompanyAddress")]
        public string CompanyAddress { get; set; }
        /// <summary>
        /// 会社電話番号
        /// </summary>
        [JsonPropertyName("CompanyPhone")]
        public string CompanyPhone { get; set; }
        /// <summary>
        /// 会社FAX番号
        /// </summary>
        [JsonPropertyName("CompanyFax")]
        public string CompanyFax { get; set; }
        /// <summary>
        /// 会社銀行情報
        /// </summary>
        [JsonPropertyName("CompanyInfoBank")]
        public string CompanyInfoBank { get; set; }
        /// <summary>
        /// 日付
        /// </summary>
        [JsonPropertyName("Day")]
        public DateTime Day { get; set; }
        /// <summary>
        /// 車番番号
        /// </summary>
        [JsonPropertyName("SyabanNumber")]
        public string SyabanNumber { get; set; }
        /// <summary>
        /// 車種表示
        /// </summary>
        [JsonPropertyName("SyasyuDisplay")]
        public string SyasyuDisplay { get; set; }
        /// <summary>
        /// 出発地
        /// </summary>
        [JsonPropertyName("StartAddress")]
        public string StartAddress { get; set; }
        /// <summary>
        /// 到着地
        /// </summary>
        [JsonPropertyName("EndAddress")]
        public string EndAddress { get; set; }
        /// <summary>
        /// 作業時間
        /// </summary>
        [JsonPropertyName("WorkTime")]
        public string WorkTime { get; set; }
        /// <summary>
        /// 商品名
        /// </summary>
        [JsonPropertyName("ProductName")]
        public string ProductName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [JsonPropertyName("Quantity")]
        public double Quantity { get; set; }
        /// <summary>
        /// 単位
        /// </summary>
        [JsonPropertyName("Unit")]
        public string Unit { get; set; }
        /// <summary>
        /// 価格
        /// </summary>
        [JsonPropertyName("Price")]
        public decimal Price { get; set; }
        /// <summary>
        /// 配送料
        /// </summary>
        [JsonPropertyName("FeeShip")]
        public decimal FeeShip { get; set; }
        /// <summary>
        /// 車両料金
        /// </summary>
        [JsonPropertyName("FeeCar")]
        public decimal FeeCar { get; set; }
        /// <summary>
        /// 合計金額
        /// </summary>
        [JsonPropertyName("TotalPrice")]
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 備考
        /// </summary>
        [JsonPropertyName("Remark")]
        public string Remark { get; set; }
        /// <summary>
        /// 請求月
        /// </summary>
        [JsonPropertyName("SeikyuMonth")]
        public DateTime SeikyuMonth { get; set; }
        /// <summary>
        /// 前回請求額
        /// </summary>
        [JsonPropertyName("Seikyu_Previous")]
        public decimal Seikyu_Previous { get; set; }
        /// <summary>
        /// 今回受領額
        /// </summary>
        [JsonPropertyName("Received_Amount_This")]
        public decimal Received_Amount_This { get; set; }
        /// <summary>
        /// 割引
        /// </summary>
        [JsonPropertyName("Discount")]
        public decimal Discount { get; set; }
        /// <summary>
        /// 繰越残高
        /// </summary>
        [JsonPropertyName("Balance_Forward")]
        public decimal Balance_Forward { get; set; }
        /// <summary>
        /// 課税対象額
        /// </summary>
        [JsonPropertyName("Taxable_Amount")]
        public decimal Taxable_Amount { get; set; }
        /// <summary>
        /// 税額
        /// </summary>
        [JsonPropertyName("Tax_Amout")]
        public decimal Tax_Amout { get; set; }
        /// <summary>
        /// 請求総額
        /// </summary>
        [JsonPropertyName("Seikyu_Total_Amount")]
        public decimal Seikyu_Total_Amount { get; set; }
    }
}