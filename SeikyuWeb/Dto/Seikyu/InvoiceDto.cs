using System.Collections.Generic;

namespace SeikyuWeb.Dto.Seikyu
{
    /// <summary>
    /// 請求書のDTOクラス
    /// </summary>
    public class InvoiceDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// 請求書ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 請求書詳細リスト
        /// </summary>
        public List<InvoiceDetailDto> details { get; set; }
        /// <summary>
        /// 印刷パターン
        /// </summary>
        public int printPattern { get; set; }
        /// <summary>
        /// 請求月
        /// </summary>
        public string seikyuMonth { get; set; }
        /// <summary>
        /// 締め日
        /// </summary>
        public string shimeDay { get; set; }
        /// <summary>
        /// 税区分
        /// </summary>
        public int zeiKubun { get; set; }
        /// <summary>
        /// ステータス
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 印刷日時
        /// </summary>
        public string printDatetime { get; set; }
        /// <summary>
        /// 印刷ユーザー
        /// </summary>
        public string printUser { get; set; }
        /// <summary>
        /// 案件数
        /// </summary>
        public int ankenCount { get; set; }
        /// <summary>
        /// 詳細数
        /// </summary>
        public int detailCount { get; set; }
        /// <summary>
        /// 請求金額
        /// </summary>
        public decimal? seikuyuAmount { get; set; }
        /// <summary>
        /// 請求合計金額
        /// </summary>
        public decimal? seikuyuTotalAmount { get; set; }
        /// <summary>
        /// 請求ID
        /// </summary>
        public int seikyuId { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
