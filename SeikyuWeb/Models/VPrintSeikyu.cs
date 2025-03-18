using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 請求書印刷情報を表すクラス
    /// </summary>
    public partial class VPrintSeikyu
    {
        /// <summary>
        /// 結果
        /// </summary>
        public int Result { get; set; }
        /// <summary>
        /// 顧客支店ID
        /// </summary>
        public int? CustomerBranchId { get; set; }
        /// <summary>
        /// チェック請求ID
        /// </summary>
        public int? CheckSeikyuId { get; set; }
        /// <summary>
        /// 請求ID
        /// </summary>
        public int? SeikyuId { get; set; }
        /// <summary>
        /// 削除日時
        /// </summary>
        public DateTime? DelDatetime { get; set; }
        /// <summary>
        /// 印刷パターン
        /// </summary>
        public int? PrintPattern { get; set; }
        /// <summary>
        /// 請求月
        /// </summary>
        public DateTime? SeikyuMonth { get; set; }
        /// <summary>
        /// 締め日
        /// </summary>
        public int? ShimeDay { get; set; }
        /// <summary>
        /// 税区分
        /// </summary>
        public int? ZeiKubun { get; set; }
        /// <summary>
        /// メールタイトル
        /// </summary>
        public string MailTitle { get; set; }
        /// <summary>
        /// メール詳細
        /// </summary>
        public string MailDetail { get; set; }
        /// <summary>
        /// 顧客名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 顧客名カナ
        /// </summary>
        public string CustomerNameKana { get; set; }
        /// <summary>
        /// 郵便番号
        /// </summary>
        public string PostCode { get; set; }
        /// <summary>
        /// メールアドレス1
        /// </summary>
        public string MailAddress1 { get; set; }
        /// <summary>
        /// メールアドレス2
        /// </summary>
        public string MailAddress2 { get; set; }
        /// <summary>
        /// 住所1
        /// </summary>
        public string Address1 { get; set; }
        /// <summary>
        /// 住所2
        /// </summary>
        public string Address2 { get; set; }
        /// <summary>
        /// 住所3
        /// </summary>
        public string Address3 { get; set; }
        /// <summary>
        /// 電話番号1
        /// </summary>
        public string Phone1 { get; set; }
        /// <summary>
        /// 電話番号2
        /// </summary>
        public string Phone2 { get; set; }
        /// <summary>
        /// FAX番号1
        /// </summary>
        public string Fax1 { get; set; }
        /// <summary>
        /// FAX番号2
        /// </summary>
        public string Fax2 { get; set; }
        /// <summary>
        /// 請求日
        /// </summary>
        public string SeikyuDate { get; set; }
        /// <summary>
        /// 前回請求額
        /// </summary>
        public decimal? SeikyuPrevious { get; set; }
        /// <summary>
        /// 税端数区分
        /// </summary>
        public int? TaxFractionKubun { get; set; }
        /// <summary>
        /// 税端数位置
        /// </summary>
        public double? TaxFractionPosition { get; set; }
        /// <summary>
        /// 今回受領額
        /// </summary>
        public decimal? ReceivedAmountThis { get; set; }
        /// <summary>
        /// 繰越額
        /// </summary>
        public decimal? BalanceForward { get; set; }
        /// <summary>
        /// 課税対象額
        /// </summary>
        public decimal? TaxableAmount { get; set; }
        /// <summary>
        /// 非課税対象額
        /// </summary>
        public decimal? NonTaxableAmount { get; set; }
        /// <summary>
        /// 税額
        /// </summary>
        public decimal? TaxAmout { get; set; }
        /// <summary>
        /// 税込金額
        /// </summary>
        public decimal? TaxIncludedAmount { get; set; }
        /// <summary>
        /// 請求金額
        /// </summary>
        public decimal? SeikyuAmount { get; set; }
        /// <summary>
        /// 現金額
        /// </summary>
        public decimal? CashAmount { get; set; }
        /// <summary>
        /// 小切手金額
        /// </summary>
        public decimal? CheckAmount { get; set; }
        /// <summary>
        /// 振込金額
        /// </summary>
        public decimal? TransferAmount { get; set; }
        /// <summary>
        /// 手形金額
        /// </summary>
        public decimal? DraftAmount { get; set; }
        /// <summary>
        /// サービス料金額
        /// </summary>
        public decimal? ServiceChargeAmount { get; set; }
        /// <summary>
        /// 運賃相殺額
        /// </summary>
        public decimal? UnchinOffset { get; set; }
        /// <summary>
        /// 一般相殺額
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
        /// 請求運賃（税抜）
        /// </summary>
        public decimal? SeikyuUnchinNoTax { get; set; }
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
        /// 請求総額
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
        /// <summary>
        /// 請求日（終了）
        /// </summary>
        public DateTime? SeikyudateTo { get; set; }
        /// <summary>
        /// 請求ID詳細
        /// </summary>
        public int? SeikyuIdDetail { get; set; }
        /// <summary>
        /// データ区分
        /// </summary>
        public int? DataKubun { get; set; }
        /// <summary>
        /// データソート
        /// </summary>
        public int? DataSort { get; set; }
        /// <summary>
        /// 売上運賃ID
        /// </summary>
        public int? UriageUnchinId { get; set; }
        /// <summary>
        /// 入金ID
        /// </summary>
        public int? NyukinId { get; set; }
        /// <summary>
        /// 案件ID
        /// </summary>
        public int? AnkenId { get; set; }
        /// <summary>
        /// 案件ID詳細
        /// </summary>
        public int? AnkenIdDetail { get; set; }
        /// <summary>
        /// 表示日
        /// </summary>
        public DateTime? DisplayDate { get; set; }
        /// <summary>
        /// 車番
        /// </summary>
        public string Syaban { get; set; }
        /// <summary>
        /// 車種型名
        /// </summary>
        public string SyasyuKataName { get; set; }
        /// <summary>
        /// 運転手名
        /// </summary>
        public string DriverName { get; set; }
        /// <summary>
        /// 積み地
        /// </summary>
        public string Tsumi { get; set; }
        /// <summary>
        /// 降ろし地
        /// </summary>
        public string Oroshi { get; set; }
        /// <summary>
        /// 荷物
        /// </summary>
        public string Luggage { get; set; }
        /// <summary>
        /// 作業名
        /// </summary>
        public string WorkName { get; set; }
        /// <summary>
        /// 備考1
        /// </summary>
        public string Remarks1 { get; set; }
        /// <summary>
        /// 備考2
        /// </summary>
        public string Remarks2 { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public double? Qty { get; set; }
        /// <summary>
        /// 単位
        /// </summary>
        public int? Unit { get; set; }
        /// <summary>
        /// 単価
        /// </summary>
        public decimal? UnitPrice { get; set; }
        /// <summary>
        /// 計算価格
        /// </summary>
        public decimal? CalcPrice { get; set; }
        /// <summary>
        /// 請求運賃詳細
        /// </summary>
        public decimal? SeikyuUnchinD { get; set; }
        /// <summary>
        /// 立替金詳細
        /// </summary>
        public decimal? TatekaekinD { get; set; }
        /// <summary>
        /// 割増1詳細
        /// </summary>
        public decimal? Warimashi1D { get; set; }
        /// <summary>
        /// 割増2詳細
        /// </summary>
        public decimal? Warimashi2D { get; set; }
        /// <summary>
        /// 割増3詳細
        /// </summary>
        public decimal? Warimashi3D { get; set; }
        /// <summary>
        /// 割増4詳細
        /// </summary>
        public decimal? Warimashi4D { get; set; }
        /// <summary>
        /// 割増5詳細
        /// </summary>
        public decimal? Warimashi5D { get; set; }
        /// <summary>
        /// 請求総額詳細
        /// </summary>
        public decimal? SeikyuTotal { get; set; }
        /// <summary>
        /// 税区分詳細
        /// </summary>
        public int? ZeiKubunD { get; set; }
        /// <summary>
        /// 開始日詳細
        /// </summary>
        public DateTime? FromDateD { get; set; }
        /// <summary>
        /// 終了日詳細
        /// </summary>
        public DateTime? ToDateD { get; set; }
    }
}
