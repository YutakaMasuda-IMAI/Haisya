using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 売上月を表すクラス
    /// </summary>
    public partial class BUriageMonth
    {
        /// <summary>
        /// 売上月ID
        /// </summary>
        public int BakUriageMonthId { get; set; }
        
        /// <summary>
        /// 顧客支店ID
        /// </summary>
        public int CustomerBranchId { get; set; }
        
        /// <summary>
        /// 請求チェックID
        /// </summary>
        public int CheckSeikyuId { get; set; }
        
        /// <summary>
        /// 請求ID
        /// </summary>
        public int SeikyuId { get; set; }
        
        /// <summary>
        /// 削除日時
        /// </summary>
        public DateTime? DelDatetime { get; set; }
        
        /// <summary>
        /// 印刷パターン
        /// </summary>
        public int PrintPattern { get; set; }
        
        /// <summary>
        /// 請求月
        /// </summary>
        public DateTime SeikyuMonth { get; set; }
        
        /// <summary>
        /// 締め日
        /// </summary>
        public DateTime ShimeDate { get; set; }
        
        /// <summary>
        /// 税区分
        /// </summary>
        public int ZeiKubun { get; set; }
        
        /// <summary>
        /// 顧客名
        /// </summary>
        public string CustomerName { get; set; }
        
        /// <summary>
        /// 顧客名カナ
        /// </summary>
        public string CustomerNameKana { get; set; }
        
        /// <summary>
        /// 請求日
        /// </summary>
        public string SeikyuDate { get; set; }
        
        /// <summary>
        /// 前回請求額
        /// </summary>
        public decimal? SeikyuPrevious { get; set; }
        
        /// <summary>
        /// 今回受領額
        /// </summary>
        public decimal? ReceivedAmountThis { get; set; }
        
        /// <summary>
        /// 繰越額
        /// </summary>
        public decimal? BalanceForward { get; set; }
        
        /// <summary>
        /// 課税額
        /// </summary>
        public decimal? TaxableAmount { get; set; }
        
        /// <summary>
        /// 非課税額
        /// </summary>
        public decimal? NonTaxableAmount { get; set; }
        
        /// <summary>
        /// 税額
        /// </summary>
        public decimal? TaxAmout { get; set; }
        
        /// <summary>
        /// 税込額
        /// </summary>
        public decimal? TaxIncludedAmount { get; set; }
        
        /// <summary>
        /// 請求額
        /// </summary>
        public decimal? SeikyuAmount { get; set; }
        
        /// <summary>
        /// 現金受領額
        /// </summary>
        public decimal? CashReceivedAmount { get; set; }
        
        /// <summary>
        /// 小切手受領額
        /// </summary>
        public decimal? CheckReceivedAmount { get; set; }
        
        /// <summary>
        /// 振込受領額
        /// </summary>
        public decimal? TransferReceivedAmount { get; set; }
        
        /// <summary>
        /// 手形受領額
        /// </summary>
        public decimal? DraftReceivedAmount { get; set; }
        
        /// <summary>
        /// サービス料額
        /// </summary>
        public decimal? ServiceChargeAmount { get; set; }
        
        /// <summary>
        /// 運賃相殺
        /// </summary>
        public decimal? UnchinOffset { get; set; }
        
        /// <summary>
        /// 一般相殺
        /// </summary>
        public decimal? GeneralOffset { get; set; }
        
        /// <summary>
        /// 調整額
        /// </summary>
        public decimal? AdjustmentAmount { get; set; }
        
        /// <summary>
        /// 合計数量
        /// </summary>
        public double? QtyTotal { get; set; }
        
        /// <summary>
        /// 請求運賃
        /// </summary>
        public decimal? SeikyuUnchin { get; set; }
        
        /// <summary>
        /// 立替金
        /// </summary>
        public decimal? Tatekaekin { get; set; }
        
        /// <summary>
        /// 割増1
        /// </summary>
        public decimal? Warimashi1 { get; set; }
        
        /// <summary>
        /// 割増2
        /// </summary>
        public decimal? Warimashi2 { get; set; }
        
        /// <summary>
        /// 割増3
        /// </summary>
        public decimal? Warimashi3 { get; set; }
        
        /// <summary>
        /// 割増4
        /// </summary>
        public decimal? Warimashi4 { get; set; }
        
        /// <summary>
        /// 割増5
        /// </summary>
        public decimal? Warimashi5 { get; set; }
        
        /// <summary>
        /// 請求合計額
        /// </summary>
        public decimal? SeikyuTotalAmount { get; set; }
        
        /// <summary>
        /// 開始日
        /// </summary>
        public DateTime? FromDate { get; set; }
        
        /// <summary>
        /// 終了日
        /// </summary>
        public DateTime? ToDate { get; set; }
        
        /// <summary>
        /// 更新日
        /// </summary>
        public DateTime? UpDate { get; set; }
    }
}
