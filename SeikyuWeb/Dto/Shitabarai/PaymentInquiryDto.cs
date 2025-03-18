using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SeikyuWeb.Dto.Shitabarai
{
    /// <summary>
    /// 支払照会DTOクラス
    /// </summary>
    public class PaymentInquiryDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// 支払詳細リスト
        /// </summary>
        public List<ShitabaraiDetailDto> shitabaraiDetails { get; set; }

        /// <summary>
        /// 支払確認DTO
        /// </summary>
        public CheckShitabaraiDto checkShitabarai { get; set; }

        /// <summary>
        /// ポータル情報IDリスト
        /// </summary>
        [JsonIgnore]
        public List<int> portalInfoIds { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
