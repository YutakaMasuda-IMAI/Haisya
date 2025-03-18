namespace SeikyuWeb.Dto.Shitabarai
{
    /// <summary>
    /// 印刷支払詳細DTOクラス
    /// </summary>
    public class PrintShitabaraiDetailDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// 表示日付
        /// </summary>
        public string displayDate { get; set; }

        /// <summary>
        /// 車番
        /// </summary>
        public string syaban { get; set; }

        /// <summary>
        /// 車種型名
        /// </summary>
        public string syasyuKataName { get; set; }

        /// <summary>
        /// 積み
        /// </summary>
        public string tsumi { get; set; }

        /// <summary>
        /// 卸し
        /// </summary>
        public string oroshi { get; set; }

        /// <summary>
        /// 作業名
        /// </summary>
        public string workName { get; set; }

        /// <summary>
        /// 荷物
        /// </summary>
        public string luggage { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public double qty { get; set; }

        /// <summary>
        /// 単位
        /// </summary>
        public string unit { get; set; }

        /// <summary>
        /// 単価
        /// </summary>
        public decimal unitPrice { get; set; }

        /// <summary>
        /// 支払運賃
        /// </summary>
        public decimal shiharaiUnchin { get; set; }

        /// <summary>
        /// 割増1
        /// </summary>
        public decimal warimashi1 { get; set; }

        /// <summary>
        /// 割増2
        /// </summary>
        public decimal warimashi2 { get; set; }

        /// <summary>
        /// 割増3
        /// </summary>
        public decimal warimashi3 { get; set; }

        /// <summary>
        /// 割増4
        /// </summary>
        public decimal warimashi4 { get; set; }

        /// <summary>
        /// 割増5
        /// </summary>
        public decimal warimashi5 { get; set; }

        /// <summary>
        /// 立替金
        /// </summary>
        public decimal tatekaekin { get; set; }

        /// <summary>
        /// 支払合計
        /// </summary>
        public decimal shiharaiTotal { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        public string remarks { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
