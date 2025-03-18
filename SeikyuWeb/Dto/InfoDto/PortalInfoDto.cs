using System;
using System.Text.Json.Serialization;

namespace SeikyuWeb.Dto.InfoDto
{
    /// <summary>
    /// ポータル情報DTO
    /// </summary>
    public class PortalInfoDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// タイトル
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 詳細
        /// </summary>
        public string detail { get; set; }

        /// <summary>
        /// 期限日
        /// </summary>
        public string limitDate => dateLimit?.ToString("yyyy年M月d日") ?? string.Empty;

        /// <summary>
        /// データID
        /// </summary>
        public int? dataId { get; set; }

        /// <summary>
        /// 印刷区分
        /// </summary>
        public int printKubun { get; set; }

        /// <summary>
        /// アクション
        /// </summary>
        public string action { get; set; }

        /// <summary>
        /// コントローラー
        /// </summary>
        public string controller { get; set; }

        /// <summary>
        /// 重要区分
        /// </summary>
        public int criticalKubun { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 期限日
        /// </summary>
        [JsonIgnore]
        public DateTime? dateLimit { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
