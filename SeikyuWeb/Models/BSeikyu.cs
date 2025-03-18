using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 請求クラス
    /// </summary>
    public partial class BSeikyu
    {
        public int BakSeikyuId { get; set; }
        public int CustomerBranchId { get; set; }
        public int CheckSeikyuId { get; set; }
        public int SeikyuId { get; set; }
        public DateTime? DelDatetime { get; set; }
        public int PrintPattern { get; set; }
        public DateTime SeikyuMonth { get; set; }
        public DateTime ShimeDate { get; set; }
        public int ZeiKubun { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNameKana { get; set; }
        public string SeikyuDate { get; set; }
        public decimal? SeikyuPrevious { get; set; }
        public decimal? ReceivedAmountThis { get; set; }
        public decimal? BalanceForward { get; set; }
        public decimal? TaxableAmount { get; set; }
        public decimal? NonTaxableAmount { get; set; }
        public decimal? TaxAmout { get; set; }
        public decimal? TaxIncludedAmount { get; set; }
        public decimal? SeikyuAmount { get; set; }
        public decimal? CashReceivedAmount { get; set; }
        public decimal? CheckReceivedAmount { get; set; }
        public decimal? TransferReceivedAmount { get; set; }
        public decimal? DraftReceivedAmount { get; set; }
        public decimal? ServiceChargeAmount { get; set; }
        public decimal? UnchinOffset { get; set; }
        public decimal? GeneralOffset { get; set; }
        public decimal? AdjustmentAmount { get; set; }
        public double? QtyTotal { get; set; }
        public decimal? SeikyuUnchin { get; set; }
        public decimal? Tatekaekin { get; set; }
        public decimal? Warimashi1 { get; set; }
        public decimal? Warimashi2 { get; set; }
        public decimal? Warimashi3 { get; set; }
        public decimal? Warimashi4 { get; set; }
        public decimal? Warimashi5 { get; set; }
        public decimal? SeikyuTotalAmount { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? UpDate { get; set; }
    }
}
