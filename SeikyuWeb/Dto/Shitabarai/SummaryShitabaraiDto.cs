namespace SeikyuWeb.Dto.Shitabarai
{
    /// <summary>
    /// 支払概要DTOクラス
    /// </summary>
    public class SummaryShitabaraiDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// 前回支払額
        /// </summary>
        public long shitabaraiPrevious { get; set; }

        /// <summary>
        /// 今回受領額
        /// </summary>
        public long receivedAmountThis { get; set; }

        /// <summary>
        /// 割引
        /// </summary>
        public long discount { get; set; }

        /// <summary>
        /// 繰越残高
        /// </summary>
        public long balanceForward { get; set; }

        /// <summary>
        /// 支払運賃
        /// </summary>
        public long shitabaraiUnchin { get; set; }

        /// <summary>
        /// 割増
        /// </summary>
        public long warimashi { get; set; }

        /// <summary>
        /// 税金
        /// </summary>
        public long tax { get; set; }

        /// <summary>
        /// 立替金
        /// </summary>
        public long tatekaekin { get; set; }

        /// <summary>
        /// 支払合計
        /// </summary>
        public long shitabaraiTotal { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
