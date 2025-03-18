namespace SeikyuWeb.Dto.Shitabarai
{
    /// <summary>
    /// 支払確認詳細DTOクラス
    /// </summary>
    public class CheckShitabaraiDetailDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// 支払確認ID
        /// </summary>
        public int checkShitabaraiId { get; set; }

        /// <summary>
        /// 売上支払ID
        /// </summary>
        public int uriageShiharaiId { get; set; }

        /// <summary>
        /// 支払確認変更DTO
        /// </summary>
        public CheckShitabaraiChangeDto change { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
