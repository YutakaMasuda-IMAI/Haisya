using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("T_Seikyu")]
[Index("Customer_Branch_ID", "Zei_Kubun", "Seikyu_Month", "Shime_Day", "Del_Datetime", Name = "IX_T_Seikyu", IsUnique = true)]
public partial class T_Seikyu
{
    [Key]
    public int Seikyu_ID { get; set; }

    public int Company_ID { get; set; }

    /// <summary>
    /// 1：WEB、2：帳票
    /// </summary>
    public int Seikyu_Kubun { get; set; }

    /// <summary>
    /// 請求方法、InquiryTypes
    /// </summary>
    public int Print_Kubun { get; set; }

    /// <summary>
    /// 帳票印刷の場合、印刷パターン
    /// </summary>
    public int Print_Pattern { get; set; }

    public int Customer_Branch_ID { get; set; }

    /// <summary>
    /// 0:課税のみ、1:非課税と混在
    /// </summary>
    public int Zei_Kubun { get; set; }

    public DateOnly Seikyu_Month { get; set; }

    public int Shime_Day { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Del_Datetime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Print_Datetime { get; set; }

    public DateOnly Print_Date { get; set; }

    public DateOnly? Print_To_Date { get; set; }

    [StringLength(50)]
    public string Mail_Address1 { get; set; }

    [StringLength(50)]
    public string Mail_Address2 { get; set; }

    /// <summary>
    /// 0:課税、1:非課税
    /// </summary>
    public int NENDOMATSU_FLG { get; set; }

    public DateOnly? FROM_DATE { get; set; }

    public DateOnly? TO_DATE { get; set; }

    public DateOnly? SEIKYUDATE_TO { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Insert_Datetime { get; set; }

    public int Insert_User { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Update_Datetime { get; set; }

    public int Update_User { get; set; }
}
