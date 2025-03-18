namespace SeikyuWeb.Dto.Shitabarai
{
    /// <summary>
    /// 支払詳細DTOクラス
    /// </summary>
    public class ShitabaraiDetailDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// 支払詳細確認DTO
        /// </summary>
        public PrintShitabaraiDetailDto checkShitabaraiDetail { get; set; }

        /// <summary>
        /// 支払変更確認DTO
        /// </summary>
        public CheckShitabaraiChangeDto checkShitabaraiChange { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
