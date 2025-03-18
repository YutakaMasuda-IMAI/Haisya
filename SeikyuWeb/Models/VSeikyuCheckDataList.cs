using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 請求チェックデータリストを表すクラス
    /// </summary>
    [Keyless]
    [Table("V_SeikyuCheckDataList")]
    public partial class VSeikyuCheckDataList
    {
        /// <summary>
        /// 請求月
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime Seikyu_Month { get; set; }
        /// <summary>
        /// 請求日
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime Seikyu_Date { get; set; }
        /// <summary>
        /// 締め日
        /// </summary>
        public int? Shime_Day { get; set; }
        /// <summary>
        /// 顧客支店ID
        /// </summary>
        public int? Customer_Branch_ID { get; set; }
        /// <summary>
        /// 顧客名
        /// </summary>
        [StringLength(40)]
        public string Customer_Name { get; set; }
        /// <summary>
        /// 請求担当ID
        /// </summary>
        public int? Seikyu_TantouID { get; set; }
        /// <summary>
        /// 請求担当
        /// </summary>
        [StringLength(50)]
        public string Seikyu_Tantou { get; set; }
        /// <summary>
        /// メールタイトル
        /// </summary>
        [StringLength(50)]
        public string Mail_Title { get; set; }
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
        /// 税区分
        /// </summary>
        public int? Zei_Kubun { get; set; }
        /// <summary>
        /// 開始日
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime FROM_DATE { get; set; }
        /// <summary>
        /// 終了日
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime TO_DATE { get; set; }
        /// <summary>
        /// 問い合わせステータス
        /// </summary>
        [StringLength(20)]
        public int? Inquiry_Status { get; set; }
        /// <summary>
        /// チェックステータス
        /// </summary>
        public int? Check_Status { get; set; }
        /// <summary>
        /// 請求変更
        /// </summary>
        public int? Seikyu_Changed { get; set; }
        /// <summary>
        /// 明細数
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Meisai_Count { get; set; }
        /// <summary>
        /// 請求運賃
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? SeikyuUnchin { get; set; }
        /// <summary>
        /// 立替金
        /// </summary>
        public decimal? Tatekaekin { get; set; }
        /// <summary>
        /// 案件数
        /// </summary>
        public int? Anken_Count { get; set; }
        /// <summary>
        /// 確定数
        /// </summary>
        public int? Kakutei_Count { get; set; }
        /// <summary>
        /// 暫定数
        /// </summary>
        public int? Zantei_Count { get; set; }
        /// <summary>
        /// 仮数
        /// </summary>
        public int? Kari_Count { get; set; }
        /// <summary>
        /// チェック請求ID
        /// </summary>
        public int? Check_Seikyu_ID { get; set; }
    }
}
