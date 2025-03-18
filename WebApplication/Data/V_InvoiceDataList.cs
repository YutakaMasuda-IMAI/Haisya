using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    /// <summary>
    /// 請求書データリストを表します。
    /// </summary>
    [Keyless]
    public class V_InvoiceDataList : V_SeikyuDataList
    {
        /// <summary>
        /// 売上運賃IDリスト（カンマ区切り）
        /// </summary>
        public string Uriage_Unchin_ID_LIST { get; set; }
    }
}