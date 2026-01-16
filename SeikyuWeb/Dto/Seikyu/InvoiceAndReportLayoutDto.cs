using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SeikyuWeb.Dto.Seikyu
{
    /// <summary>
    /// 請求書とレポートレイアウトのDTOクラス
    /// </summary>
    public class InvoiceAndReportLayoutDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// 請求書情報
        /// </summary>
        public InvoiceDto printSeikyu { get; set; }
        /// <summary>
        /// レポートレイアウトリスト
        /// </summary>
        public List<ReportLayoutDto.ReportLayoutDto> reportLayout { get; set; }
        /// <summary>
        /// ポータル情報IDリスト（JSONに含めない）
        /// </summary>
        [JsonIgnore]
        public List<int> portalInfoIds { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
