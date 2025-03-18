namespace SeikyuWeb.Dto.Seikyu
{
    /// <summary>
    /// 請求詳細DTOクラス
    /// </summary>
    public class SeikyuDetailDto
    {
#pragma warning disable IDE1006 // Naming Styles
        public CheckSeikyuDetailResDto checkSeikyuDetail { get; set; }
        public CheckSeikyuChangeDto checkSeikyuChange { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
