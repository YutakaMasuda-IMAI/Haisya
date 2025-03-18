using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Data
{
    /// <summary>
    /// 支払チェックデータリストを表すクラス
    /// </summary>
    [Keyless]
    [Table("V_ShitabaraiCheckDataList")]
    public class V_ShitabaraiCheckDataList
    {
        // [Key]
        // public int? Id { get; set; }

        /// <summary>
        /// 支払チェックID
        /// </summary>
        [Key]
        public int? Check_Shitabarai_ID { get; set; }

        /// <summary>
        /// 支払月
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime Shiharai_Month { get; set; }

        /// <summary>
        /// 支払日
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime Shiharai_Date { get; set; }

        /// <summary>
        /// 締日
        /// </summary>
        public int? Shime_Day { get; set; }

        /// <summary>
        /// 予社支店ID
        /// </summary>
        public int? Yosya_Branch_ID { get; set; }

        /// <summary>
        /// 予社名
        /// </summary>
        [Column(TypeName = "nvarchar(40)")]
        public string Yosya_Name { get; set; }

        /// <summary>
        /// 支払担当ID
        /// </summary>
        public int? SHIHARAI_TANTOUID { get; set; }

        /// <summary>
        /// 支払担当
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string SHIHARAI_TANTOU { get; set; }

        /// <summary>
        /// メールタイトル
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string Mail_Title { get; set; }

        /// <summary>
        /// メールアドレス1
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string Mail_Address1 { get; set; }

        /// <summary>
        /// メールアドレス2
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string Mail_Address2 { get; set; }

        /// <summary>
        /// 電話番号1
        /// </summary>
        [Column(TypeName = "nvarchar(13)")]
        public string Phone1 { get; set; }

        /// <summary>
        /// 電話番号2
        /// </summary>
        [Column(TypeName = "nvarchar(13)")]
        public string Phone2 { get; set; }

        /// <summary>
        /// FAX番号1
        /// </summary>
        [Column(TypeName = "nvarchar(13)")]
        public string Fax1 { get; set; }

        /// <summary>
        /// FAX番号2
        /// </summary>
        [Column(TypeName = "nvarchar(13)")]
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
        public int? Inquiry_Status { get; set; }

        /// <summary>
        /// 問い合わせステータスの文字列
        /// </summary>
        public string Inquiry_Status_String
        {
            get
            {
                switch (Inquiry_Status)
                {
                    case 0:
                        return "未";
                    case 1:
                        return "WEB済";
                    case 2:
                        return "発行済";
                    default:
                        return "";
                };
            }
        }

        /// <summary>
        /// チェックステータス
        /// </summary>
        public int? Check_Status { get; set; }

        /// <summary>
        /// 支払変更
        /// </summary>
        public int? Shiharai_Changed { get; set; }

        /// <summary>
        /// 明細数
        /// </summary>
        public decimal? Meisai_Count { get; set; }

        /// <summary>
        /// 支払運賃
        /// </summary>
        public decimal? ShiharaiUnchin { get; set; }

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
        /// 売上支払ID
        /// </summary>
        public int Uriage_Shiharai_ID { get; set; }
    }
}
