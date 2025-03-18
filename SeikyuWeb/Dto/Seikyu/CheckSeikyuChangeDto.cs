namespace SeikyuWeb.Dto.Seikyu
{
    /// <summary>
    /// 請求変更のDTO
    /// </summary>
    public class CheckSeikyuChangeDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>請求チェックID</summary>
        public int checkSeikyuId { get; set; }
        /// <summary>売上運賃ID</summary>
        public int uriageUnchinId { get; set; }
        /// <summary>数量</summary>
        public double? qty { get; set; }
        /// <summary>単位</summary>
        public int? unit { get; set; }
        /// <summary>単価</summary>
        public decimal? unitPrice { get; set; }
        /// <summary>計算価格</summary>
        public decimal? calcPrice { get; set; }
        /// <summary>請求運賃</summary>
        public decimal? seikyuUnchin { get; set; }
        /// <summary>立替金</summary>
        public decimal? tatekaekin { get; set; }
        /// <summary>割増1</summary>
        public decimal? warimashi1 { get; set; }
        /// <summary>割増2</summary>
        public decimal? warimashi2 { get; set; }
        /// <summary>割増3</summary>
        public decimal? warimashi3 { get; set; }
        /// <summary>割増4</summary>
        public decimal? warimashi4 { get; set; }
        /// <summary>割増5</summary>
        public decimal? warimashi5 { get; set; }
        /// <summary>請求合計</summary>
        public decimal? seikyuTotal { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
