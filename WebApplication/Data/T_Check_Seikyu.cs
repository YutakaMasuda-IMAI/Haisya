using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("T_Check_Seikyu")]
[Index("Customer_Branch_ID", "Seikyu_Month", "Shime_Day", "Del_Datetime", "Zei_Kubun", Name = "IX_T_Check_Seikyu", IsUnique = true)]
public partial class T_Check_Seikyu
{
    [Key]
    public int Check_Seikyu_ID { get; set; }

    public int Company_ID { get; set; }

    public int Print_Pattern { get; set; }

    /// <summary>
    /// 1：WEB、2：帳票
    /// </summary>
    public int Check_Kubun { get; set; }

    /// <summary>
    /// 0：発行済み、1：確認中、2：確認済、3：未定、4：承認済
    /// </summary>
    public int Check_Status { get; set; }

    public int Customer_Branch_ID { get; set; }

    public DateOnly Seikyu_Month { get; set; }

    public int Shime_Day { get; set; }

    /// <summary>
    /// 0:課税、1:非課税
    /// </summary>
    public int Zei_Kubun { get; set; }

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

    [Column(TypeName = "datetime")]
    public DateTime Insert_Datetime { get; set; }

    public int Insert_User { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Update_Datetime { get; set; }

    public int Update_User { get; set; }

    public DateOnly? FROM_DATE { get; set; }

    public DateOnly? TO_DATE { get; set; }
}
