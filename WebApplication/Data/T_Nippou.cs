using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("T_Nippou")]
[Index("AnkenDisplay_ID", Name = "IX_T_Nippou", IsUnique = true)]
public partial class T_Nippou
{
    [Key]
    public int Nippou_ID { get; set; }

    public int AnkenDisplay_ID { get; set; }

    public int Anken_ID { get; set; }

    public int Receipt { get; set; }

    public DateOnly Receipt_Date { get; set; }

    [StringLength(255)]
    public string Commnet { get; set; }

    public int ApprovalStatus { get; set; }

    public int RenkeiStatus { get; set; }

    /// <summary>
    /// 区間距離
    /// </summary>
    public double? Distance { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Insert_Datetime { get; set; }

    public int? Insert_User { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Update_Datetime { get; set; }

    public int? Update_User { get; set; }

    /// <summary>
    /// デジタコ連動結果：０：未連携、１：正常連携、２：一部連携
    /// </summary>
    public int DegitakoLink_Result { get; set; }
}
