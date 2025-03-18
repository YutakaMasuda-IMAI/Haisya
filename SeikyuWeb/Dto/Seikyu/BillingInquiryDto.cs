using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SeikyuWeb.Dto.Seikyu
{
    /// <summary>
    /// 請求照会のDTO
    /// </summary>
    public class BillingInquiryDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>請求詳細リスト</summary>
        public List<SeikyuDetailDto> seikyuDetails { get; set; }
        /// <summary>請求チェックDTO</summary>
        public CheckSeikyuDto checkSeikyu { get; set; }
        /// <summary>ポータル情報IDリスト</summary>
        [JsonIgnore]
        public List<int> portalInfoIds { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
