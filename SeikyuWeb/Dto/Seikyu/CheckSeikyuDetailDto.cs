namespace SeikyuWeb.Dto.Seikyu
{
    /// <summary>
    /// 請求詳細のDTO
    /// </summary>
    public class CheckSeikyuDetailDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>請求チェックID</summary>
        public int checkSeikyuId { get; set; }
        /// <summary>売上運賃ID</summary>
        public int uriageUnchinId { get; set; }
        /// <summary>請求変更DTO</summary>
        public CheckSeikyuChangeDto change { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
