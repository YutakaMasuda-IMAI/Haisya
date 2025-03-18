using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication.Data.ReportCommons
{
    /// <summary>
    /// V_ReportTransportOrderSheetListクラスは帳票transport order sheet list pdfのモデル
    /// </summary>
    [Keyless]
    [Table("V_ReportTransportOrderSheetList")]
    public partial class V_ReportTransportOrderSheetList
    {
        /// <summary>
        /// 注文日
        /// </summary>
        [JsonPropertyName("OrderDate")]
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// 顧客名
        /// </summary>
        [JsonPropertyName("CustomerName")]
        public string CustomerName { get; set; }

        /// <summary>
        /// 顧客FAX
        /// </summary>
        [JsonPropertyName("CustomerFax")]
        public string CustomerFax { get; set; }

        /// <summary>
        /// 車種表示
        /// </summary>
        [JsonPropertyName("SyasyuDisplay")]
        public string SyasyuDisplay { get; set; }

        /// <summary>
        /// 台数
        /// </summary>
        [JsonPropertyName("Daisuu")]
        public string Daisuu { get; set; }

        /// <summary>
        /// 製品名
        /// </summary>
        [JsonPropertyName("ProductName")]
        public string ProductName { get; set; }

        /// <summary>
        /// 製品重量
        /// </summary>
        [JsonPropertyName("ProductWeight")]
        public string ProductWeight { get; set; }

        /// <summary>
        /// デバイス
        /// </summary>
        [JsonPropertyName("Device")]
        public string Device { get; set; }

        /// <summary>
        /// 開始日
        /// </summary>
        [JsonPropertyName("StartDate")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 開始時間
        /// </summary>
        [JsonPropertyName("StartTime")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 開始住所1
        /// </summary>
        [JsonPropertyName("StartAddress1")]
        public string StartAddress1 { get; set; }

        /// <summary>
        /// 開始住所2
        /// </summary>
        [JsonPropertyName("StartAddress2")]
        public string StartAddress2 { get; set; }

        /// <summary>
        /// 開始電話番号
        /// </summary>
        [JsonPropertyName("StartPhone")]
        public string StartPhone { get; set; }

        /// <summary>
        /// 終了日
        /// </summary>
        [JsonPropertyName("EndDate")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 終了時間
        /// </summary>
        [JsonPropertyName("EndTime")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 終了住所1
        /// </summary>
        [JsonPropertyName("EndAddress1")]
        public string EndAddress1 { get; set; }

        /// <summary>
        /// 終了住所2
        /// </summary>
        [JsonPropertyName("EndAddress2")]
        public string EndAddress2 { get; set; }

        /// <summary>
        /// メモ
        /// </summary>
        [JsonPropertyName("Note")]
        public string Note { get; set; }

        /// <summary>
        /// 会社名
        /// </summary>
        [JsonPropertyName("CompanyName")]
        public string CompanyName { get; set; }

        /// <summary>
        /// 車番番号
        /// </summary>
        [JsonPropertyName("SyabanNumber")]
        public string SyabanNumber { get; set; }

        /// <summary>
        /// 運転手名
        /// </summary>
        [JsonPropertyName("DriverName")]
        public string DriverName { get; set; }

        /// <summary>
        /// 運転手電話番号
        /// </summary>
        [JsonPropertyName("DriverPhone")]
        public string DriverPhone { get; set; }

        /// <summary>
        /// 配送合計時間
        /// </summary>
        [JsonPropertyName("TotalTimeDelivery")]
        public string TotalTimeDelivery { get; set; }
    }
}
