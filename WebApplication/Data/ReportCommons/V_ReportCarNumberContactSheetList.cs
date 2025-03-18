using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using System.Text.Json.Serialization;

namespace WebApplication.Data.ReportCommons
{
    /// <summary>
    /// V_ReportCarNumberContactSheetListクラスは車両番号連絡シートリストpdfのモデル
    /// </summary>
    [Keyless]
    [Table("V_ReportCarNumberContactSheetList")]
    public partial class V_ReportCarNumberContactSheetList
    {
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
        /// 日付
        /// </summary>
        [JsonPropertyName("Day")]
        public DateTime Day { get; set; }
        
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
        /// 会社FAX
        /// </summary>
        [JsonPropertyName("CompanyFax")]
        public string CompanyFax { get; set; }
        
        /// <summary>
        /// 会社担当者名
        /// </summary>
        [JsonPropertyName("CompanyTantouName")]
        public string CompanyTantouName { get; set; }
        
        /// <summary>
        /// 担当者電話番号
        /// </summary>
        [JsonPropertyName("TantouPhone")]
        public string TantouPhone { get; set; }
        
        /// <summary>
        /// 会社メールアドレス
        /// </summary>
        [JsonPropertyName("CompanyEmail")]
        public string CompanyEmail { get; set; }
        
        /// <summary>
        /// 会社URL
        /// </summary>
        [JsonPropertyName("CompanyUrl")]
        public string CompanyUrl { get; set; }
        
        /// <summary>
        /// 開始日
        /// </summary>
        [JsonPropertyName("StartDate")]
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// 開始住所
        /// </summary>
        [JsonPropertyName("StartAddress")]
        public string StartAddress { get; set; }
        
        /// <summary>
        /// 終了日
        /// </summary>
        [JsonPropertyName("EndDate")]
        public DateTime EndDate { get; set; }
        
        /// <summary>
        /// 終了住所
        /// </summary>
        [JsonPropertyName("EndAddress")]
        public string EndAddress { get; set; }
        
        /// <summary>
        /// 会社名略称
        /// </summary>
        [JsonPropertyName("CompanyNameAbbr")]
        public string CompanyNameAbbr { get; set; }
        
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
        /// ドライバー名
        /// </summary>
        [JsonPropertyName("DriverName")]
        public string DriverName { get; set; }
        
        /// <summary>
        /// ドライバー電話番号
        /// </summary>
        [JsonPropertyName("DriverPhone")]
        public string DriverPhone { get; set; }
        
        /// <summary>
        /// 備考
        /// </summary>
        [JsonPropertyName("Note")]
        public string Note { get; set; }
    }
}
