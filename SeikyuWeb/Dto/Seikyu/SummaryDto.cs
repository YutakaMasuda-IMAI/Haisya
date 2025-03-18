namespace SeikyuWeb.Dto.Seikyu
{
    /// <summary>
    /// 請求サマリーDTOクラス
    /// </summary>
    public class SummaryDto
    {
#pragma warning disable IDE1006 // Naming Styles
        public long seikyuPrevious { get; set; }
        public long receivedAmountThis { get; set; }
        public long discount { get; set; }
        public long balanceForward { get; set; }
        public long seikyuUnchin { get; set; }
        public long warimashi { get; set; }
        public long tax { get; set; }
        public long tatekaekin { get; set; }
        public long seikyuTotal { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
