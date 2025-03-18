namespace SeikyuWeb.Dto.Seikyu
{
    /// <summary>
    /// 請求詳細のレスポンスDTO
    /// </summary>
    public class CheckSeikyuDetailResDto
    {
        /// <summary>表示日付</summary>
        public string displayDate { get; set; }
        /// <summary>車番</summary>
        public string syaban { get; set; }
        /// <summary>車種型名</summary>
        public string syasyuKataName { get; set; }
        /// <summary>積み地</summary>
        public string tsumi { get; set; }
        /// <summary>卸し地</summary>
        public string oroshi { get; set; }
        /// <summary>作業名</summary>
        public string workName { get; set; }
        /// <summary>荷物</summary>
        public string luggage { get; set; }
        /// <summary>数量</summary>
        public double qty { get; set; }
        /// <summary>単位</summary>
        public string unit { get; set; }
        /// <summary>単価</summary>
        public decimal unitPrice { get; set; }
        /// <summary>請求運賃</summary>
        public decimal seikyuUnchin { get; set; }
        /// <summary>割増1</summary>
        public decimal warimashi1 { get; set; }
        /// <summary>割増2</summary>
        public decimal warimashi2 { get; set; }
        /// <summary>割増3</summary>
        public decimal warimashi3 { get; set; }
        /// <summary>割増4</summary>
        public decimal warimashi4 { get; set; }
        /// <summary>割増5</summary>
        public decimal warimashi5 { get; set; }
        /// <summary>立替金</summary>
        public decimal tatekaekin { get; set; }
        /// <summary>請求合計</summary>
        public decimal seikyuTotal { get; set; }
        /// <summary>備考</summary>
        public string remarks { get; set; }
    }
}
