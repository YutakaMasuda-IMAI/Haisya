using System.Text.Json.Serialization;

namespace SeikyuWeb.Dto.Customer
{
    /// <summary>
    /// 物流ユニットDTO
    /// </summary>
    public class LogisticsUnitDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// ログインID
        /// </summary>
        public string loginId { get; set; }

        /// <summary>
        /// 顧客担当者情報
        /// </summary>
        public CustomerTantouDto customerTantou { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// ロックフラグ
        /// </summary>
        [JsonIgnore]
        public bool LockFlg { get; set; }
    }
}