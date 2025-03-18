#nullable disable

using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Data
{
    /// <summary>
    /// 請求書チェックデータリストを表します。
    /// </summary>
    public class V_InvoiceCheckDataList : V_InvoiceDataList
    {
        /// <summary>
        /// チェック請求ID
        /// </summary>
        public int Check_Seikyu_ID { get; set; }
    }
}
