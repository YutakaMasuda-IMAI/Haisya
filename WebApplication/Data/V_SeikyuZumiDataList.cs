using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data
{
    /// <summary>
    /// 請求済みデータリストを表すクラス
    /// </summary>
    [Keyless]
    public partial class V_SeikyuZumiDataList 
    {
        /// <summary>
        /// 会社ID
        /// </summary>
        public int Company_ID { get; set; }

        /// <summary>
        /// 請求担当ID
        /// </summary>
        public int? SeikyuTantouID { get; set; }

        /// <summary>
        /// 請求担当名
        /// </summary>
        [MaxLength(60)]
        public string Seikyu_Tantou_Name { get; set; }

        /// <summary>
        /// 問い合わせステータス
        /// </summary>
        [DisplayName("問い合わせステータス")]
        public int Inquiry_Status { get; set; }

        /// <summary>
        /// 登録ステータス
        /// </summary>
        [DisplayName("登録ステータス")]
        public int Reg_Status { get; set; }

        /// <summary>
        /// 税区分名
        /// </summary>
        [MaxLength(50)]
        public string Zei_Kubun_Name { get; set; }

        /// <summary>
        /// 明細数
        /// </summary>
        public int Meisai_Count { get; set; }

        /// <summary>
        /// 印刷日時
        /// </summary>
        public DateTime? Print_Datetime { get; set; }

        /// <summary>
        /// 印刷日
        /// </summary>
        public DateTime? Print_Date { get; set; }

        /// <summary>
        /// 印刷終了日
        /// </summary>
        public DateTime? Print_To_Date { get; set; }

        /// <summary>
        /// 受領金額
        /// </summary>
        [DisplayName("受領金額")]
        [Column(TypeName = "money")]
        public decimal? Received_Money { get; set; }

        /// <summary>
        /// 返金金額
        /// </summary>
        [DisplayName("返金金額")]
        [Column(TypeName = "money")]
        public decimal? Refund_Money { get; set; }

        /// <summary>
        /// 印刷請求ID
        /// </summary>
        public int Print_Seikyu_ID { get; set; }

        /// <summary>
        /// 顧客支店ID
        /// </summary>
        public int Customer_Branch_ID { get; set; }

        /// <summary>
        /// チェック請求ID
        /// </summary>
        public int Check_Seikyu_ID { get; set; }

        /// <summary>
        /// 請求ID
        /// </summary>
        public int Seikyu_ID { get; set; }

        /// <summary>
        /// 削除日時
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? Del_Datetime { get; set; }

        /// <summary>
        /// 印刷パターン
        /// </summary>
        public int Print_Pattern { get; set; }

        /// <summary>
        /// 請求月
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime Seikyu_Month { get; set; }

        /// <summary>
        /// 締日
        /// </summary>
        public int Shime_Day { get; set; }

        /// <summary>
        /// 税区分
        /// </summary>
        public int Zei_Kubun { get; set; }

        /// <summary>
        /// メールタイトル
        /// </summary>
        [StringLength(50)]
        public string Mail_Title { get; set; }

        /// <summary>
        /// メール詳細
        /// </summary>
        [StringLength(255)]
        public string Mail_Detail { get; set; }

        /// <summary>
        /// 顧客名
        /// </summary>
        [StringLength(40)]
        public string Customer_Name { get; set; }

        /// <summary>
        /// 顧客名カナ
        /// </summary>
        [StringLength(16)]
        public string Customer_Name_Kana { get; set; }

        /// <summary>
        /// メールアドレス1
        /// </summary>
        [StringLength(50)]
        public string Mail_Address1 { get; set; }

        /// <summary>
        /// メールアドレス2
        /// </summary>
        [StringLength(50)]
        public string Mail_Address2 { get; set; }

        /// <summary>
        /// 郵便番号
        /// </summary>
        [StringLength(8)]
        public string PostCode { get; set; }

        /// <summary>
        /// 住所1
        /// </summary>
        [StringLength(80)]
        public string Address1 { get; set; }

        /// <summary>
        /// 住所2
        /// </summary>
        [StringLength(40)]
        public string Address2 { get; set; }

        /// <summary>
        /// 住所3
        /// </summary>
        [StringLength(40)]
        public string Address3 { get; set; }

        /// <summary>
        /// 電話番号1
        /// </summary>
        [StringLength(13)]
        public string Phone1 { get; set; }

        /// <summary>
        /// 電話番号2
        /// </summary>
        [StringLength(13)]
        public string Phone2 { get; set; }

        /// <summary>
        /// FAX番号1
        /// </summary>
        [StringLength(13)]
        public string Fax1 { get; set; }

        /// <summary>
        /// FAX番号2
        /// </summary>
        [StringLength(13)]
        public string Fax2 { get; set; }

        /// <summary>
        /// 請求日
        /// </summary>
        [StringLength(10)]
        public string Seikyu_Date { get; set; }

        /// <summary>
        /// 前回請求額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Seikyu_Previous { get; set; }

        /// <summary>
        /// 今回受領額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Received_Amount_This { get; set; }

        /// <summary>
        /// 繰越額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Balance_Forward { get; set; }

        /// <summary>
        /// 課税額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Taxable_Amount { get; set; }

        /// <summary>
        /// 非課税額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Non_Taxable_Amount { get; set; }

        /// <summary>
        /// 税額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Tax_Amout { get; set; }

        /// <summary>
        /// 税込額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Tax_Included_Amount { get; set; }

        /// <summary>
        /// 請求額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Seikyu_Amount { get; set; }

        /// <summary>
        /// 現金額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Cash_Amount { get; set; }

        /// <summary>
        /// 小切手額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Check_Amount { get; set; }

        /// <summary>
        /// 振込額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Transfer_Amount { get; set; }

        /// <summary>
        /// 手形額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Draft_Amount { get; set; }

        /// <summary>
        /// サービス料額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Service_Charge_Amount { get; set; }

        /// <summary>
        /// 運賃相殺額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Unchin_Offset { get; set; }

        /// <summary>
        /// 一般相殺額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? General_Offset { get; set; }

        /// <summary>
        /// 調整額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Adjustment_Amount { get; set; }

        /// <summary>
        /// 数量合計
        /// </summary>
        public double? Qty_Total { get; set; }

        /// <summary>
        /// 請求運賃
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? SeikyuUnchin { get; set; }

        /// <summary>
        /// 請求運賃（税抜）
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? SeikyuUnchin_NoTax { get; set; }

        /// <summary>
        /// 立替金
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Tatekaekin { get; set; }

        /// <summary>
        /// 割増1
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Warimashi1 { get; set; }

        /// <summary>
        /// 割増2
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Warimashi2 { get; set; }

        /// <summary>
        /// 割増3
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Warimashi3 { get; set; }

        /// <summary>
        /// 割増4
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Warimashi4 { get; set; }

        /// <summary>
        /// 割増5
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Warimashi5 { get; set; }

        /// <summary>
        /// 請求総額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Seikyu_Total_Amount { get; set; }

        /// <summary>
        /// 開始日
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime? FROM_DATE { get; set; }

        /// <summary>
        /// 終了日
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime? TO_DATE { get; set; }

        /// <summary>
        /// 更新日
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UP_DATE { get; set; }

        /// <summary>
        /// 請求日終了
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime? SEIKYUDATE_TO { get; set; }

        /// <summary>
        /// 税端数区分
        /// </summary>
        public int Tax_Fraction_Kubun { get; set; }

        /// <summary>
        /// 税端数位置
        /// </summary>
        public double Tax_Fraction_Position { get; set; }
    }
}
