using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data
{
    /// <summary>
    /// 請求データリストを表します。
    /// </summary>
    [Keyless]
    public partial class V_SeikyuDataList
    {
        /// <summary>
        /// 会社ID
        /// </summary>
        public int Company_ID { get; set; }

        /// <summary>
        /// 顧客支店ID
        /// </summary>
        public int Customer_Branch_ID { get; set; }

        /// <summary>
        /// 顧客名
        /// </summary>
        [MaxLength(60)]
        public string Customer_Name { get; set; }

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
        /// メールアドレス1
        /// </summary>
        [MaxLength(50)]
        public string Mail_Address1 { get; set; }

        /// <summary>
        /// メールアドレス2
        /// </summary>
        [MaxLength(50)]
        public string Mail_Address2 { get; set; }

        /// <summary>
        /// 問い合わせステータス
        /// </summary>
        public int Inquiry_Status { get; set; }

        /// <summary>
        /// 登録ステータス
        /// </summary>
        public int Reg_Status { get; set; }

        /// <summary>
        /// 税区分
        /// </summary>
        public int Zei_Kubun { get; set; }

        /// <summary>
        /// 締め日
        /// </summary>
        public int Shime_Day { get; set; }

        /// <summary>
        /// 請求月
        /// </summary>
        public DateOnly Seikyu_Month { get; set; }

        /// <summary>
        /// 印刷日時
        /// </summary>
        public DateTime? Print_Datetime { get; set; }

        /// <summary>
        /// 印刷日
        /// </summary>
        public DateOnly? Print_Date { get; set; }

        /// <summary>
        /// 印刷終了日
        /// </summary>
        public DateOnly? Print_To_Date { get; set; }

        /// <summary>
        /// 明細数
        /// </summary>
        public int? Meisai_Count { get; set; }

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
        /// 請求運賃
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? SeikyuUnchin { get; set; }

        /// <summary>
        /// 立替金
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Tatekaekin { get; set; }

        /// <summary>
        /// 請求ID
        /// </summary>
        public int? Seikyu_ID { get; set; }

        /// <summary>
        /// 印刷請求ID
        /// </summary>
        public int? Print_Seikyu_ID { get; set; }

        /// <summary>
        /// 売上運賃ID
        /// </summary>
        public int Uriage_Unchin_ID { get; set; }

        /// <summary>
        /// 開始日
        /// </summary>
        [Column(TypeName = "date")]
        public DateOnly? FROM_DATE { get; set; }

        /// <summary>
        /// 終了日
        /// </summary>
        [Column(TypeName = "date")]
        public DateOnly? TO_DATE { get; set; }

        /// <summary>
        /// 請求日終了
        /// </summary>
        [Column(TypeName = "date")]
        public DateOnly? SEIKYUDATE_TO { get; set; }

        /// <summary>
        /// 年度末フラグ
        /// </summary>
        public int NENDOMATSU_FLG { get; set; }
    }
}
