using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("T_Print_Shitabarai")]
public partial class T_Print_Shitabarai
{
    [Key]
    public int Print_Shitabarai_ID { get; set; }

    public int Yosya_Branch_ID { get; set; }

    public DateOnly Shime_Date { get; set; }

    public int Check_Shitabarai_ID { get; set; }

    public int Shitabarai_ID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Del_Datetime { get; set; }

    public DateOnly Shiharai_Month { get; set; }

    public int Print_Pattern { get; set; }

    /// <summary>
    /// 0:課税、1:非課税
    /// </summary>
    public int Zei_Kubun { get; set; }

    [StringLength(50)]
    public string Mail_Title { get; set; }

    [StringLength(255)]
    public string Mail_Detail { get; set; }

    [StringLength(40)]
    public string Yosya_Name { get; set; }

    [StringLength(16)]
    public string Yosya_Name_Kana { get; set; }

    [StringLength(7)]
    public string PostCode { get; set; }

    [StringLength(50)]
    public string Mail_Address1 { get; set; }

    [StringLength(50)]
    public string Mail_Address2 { get; set; }

    [StringLength(40)]
    public string Address1 { get; set; }

    [StringLength(40)]
    public string Address2 { get; set; }

    [StringLength(40)]
    public string Address3 { get; set; }

    [StringLength(13)]
    public string Phone1 { get; set; }

    [StringLength(13)]
    public string Phone2 { get; set; }

    [StringLength(13)]
    public string Fax1 { get; set; }

    [StringLength(13)]
    public string Fax2 { get; set; }

    [StringLength(10)]
    public string Shiharai_Date { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? 前月残 { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? 当月支払額 { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? 繰越金額 { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? 課税支払金額 { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? 非課税支払金額 { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? 今回支払金額 { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? 消費税額 { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? 税込支払金額 { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? 今回支払額 { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? 現金支払額 { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? 小切手支払額 { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? 振込支払額 { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? 手形支払額 { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? 手数料金額 { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? 運賃相殺 { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? 一般相殺 { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? 調整金額 { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal? 数量計 { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal? 基本運賃計 { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal? 割増１計 { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal? 割増２計 { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal? 割増３計 { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal? 割増４計 { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal? 割増５計 { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal? 割増６計 { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? 運賃合計計 { get; set; }

    public int? 明細件数 { get; set; }

    public DateOnly? FROM_DATE { get; set; }

    public DateOnly? TO_DATE { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UP_DATE { get; set; }
}
