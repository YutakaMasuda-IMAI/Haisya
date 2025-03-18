using System.Text.Json.Serialization;

namespace SeikyuWeb.Dto.Contractor
{
    /// <summary>
    /// 請負業者DTO
    /// </summary>
    public class ContractorDto
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
        /// 会社ユーザー情報
        /// </summary>
        public CompanyUserDto companyUser { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// ロックフラグ
        /// </summary>
        [JsonIgnore]
        public bool LockFlg { get; set; }
    }
}