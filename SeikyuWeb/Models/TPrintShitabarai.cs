using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 支払印刷情報を表します。
    /// </summary>
    public partial class TPrintShitabarai
    {
        /// <summary>
        /// 支払印刷ID
        /// </summary>
        public int PrintShitabaraiId { get; set; }
        /// <summary>
        /// 傭車支店ID
        /// </summary>
        public int YosyaBranchId { get; set; }
        /// <summary>
        /// 締日
        /// </summary>
        public DateTime ShimeDate { get; set; }
        /// <summary>
        /// チェック支払ID
        /// </summary>
        public int CheckShitabaraiId { get; set; }
        /// <summary>
        /// 支払ID
        /// </summary>
        public int ShitabaraiId { get; set; }
        /// <summary>
        /// 削除日時
        /// </summary>
        public DateTime? DelDatetime { get; set; }
        /// <summary>
        /// 支払月
        /// </summary>
        public DateTime ShiharaiMonth { get; set; }
        /// <summary>
        /// 印刷パターン
        /// </summary>
        public int PrintPattern { get; set; }
        /// <summary>
        /// 税区分
        /// </summary>
        public int ZeiKubun { get; set; }
        /// <summary>
        /// メールタイトル
        /// </summary>
        public string MailTitle { get; set; }
        /// <summary>
        /// メール詳細
        /// </summary>
        public string MailDetail { get; set; }
        /// <summary>
        /// 傭車名
        /// </summary>
        public string YosyaName { get; set; }
        /// <summary>
        /// 傭車名（カナ）
        /// </summary>
        public string YosyaNameKana { get; set; }
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
        /// 支払日
        /// </summary>
        public string ShiharaiDate { get; set; }
        /// <summary>
        /// 前月残
        /// </summary>
        public decimal? 前月残 { get; set; }
        /// <summary>
        /// 当月支払額
        /// </summary>
        public decimal? 当月支払額 { get; set; }
        /// <summary>
        /// 繰越金額
        /// </summary>
        public decimal? 繰越金額 { get; set; }
        /// <summary>
        /// 課税支払金額
        /// </summary>
        public decimal? 課税支払金額 { get; set; }
        /// <summary>
        /// 非課税支払金額
        /// </summary>
        public decimal? 非課税支払金額 { get; set; }
        /// <summary>
        /// 今回支払金額
        /// </summary>
        public decimal? 今回支払金額 { get; set; }
        /// <summary>
        /// 消費税額
        /// </summary>
        public decimal? 消費税額 { get; set; }
        /// <summary>
        /// 税込支払金額
        /// </summary>
        public decimal? 税込支払金額 { get; set; }
        /// <summary>
        /// 今回支払額
        /// </summary>
        public decimal? 今回支払額 { get; set; }
        /// <summary>
        /// 現金支払額
        /// </summary>
        public decimal? 現金支払額 { get; set; }
        /// <summary>
        /// 小切手支払額
        /// </summary>
        public decimal? 小切手支払額 { get; set; }
        /// <summary>
        /// 振込支払額
        /// </summary>
        public decimal? 振込支払額 { get; set; }
        /// <summary>
        /// 手形支払額
        /// </summary>
        public decimal? 手形支払額 { get; set; }
        /// <summary>
        /// 手数料金額
        /// </summary>
        public decimal? 手数料金額 { get; set; }
        /// <summary>
        /// 運賃相殺
        /// </summary>
        public decimal? 運賃相殺 { get; set; }
        /// <summary>
        /// 一般相殺
        /// </summary>
        public decimal? 一般相殺 { get; set; }
        /// <summary>
        /// 調整金額
        /// </summary>
        public decimal? 調整金額 { get; set; }
        /// <summary>
        /// 数量計
        /// </summary>
        public decimal? 数量計 { get; set; }
        /// <summary>
        /// 基本運賃計
        /// </summary>
        public decimal? 基本運賃計 { get; set; }
        /// <summary>
        /// 割増1計
        /// </summary>
        public decimal? 割増１計 { get; set; }
        /// <summary>
        /// 割増2計
        /// </summary>
        public decimal? 割増２計 { get; set; }
        /// <summary>
        /// 割増3計
        /// </summary>
        public decimal? 割増３計 { get; set; }
        /// <summary>
        /// 割増4計
        /// </summary>
        public decimal? 割増４計 { get; set; }
        /// <summary>
        /// 割増5計
        /// </summary>
        public decimal? 割増５計 { get; set; }
        /// <summary>
        /// 割増6計
        /// </summary>
        public decimal? 割増６計 { get; set; }
        /// <summary>
        /// 運賃合計計
        /// </summary>
        public decimal? 運賃合計計 { get; set; }
        /// <summary>
        /// 明細件数
        /// </summary>
        public int? 明細件数 { get; set; }
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
