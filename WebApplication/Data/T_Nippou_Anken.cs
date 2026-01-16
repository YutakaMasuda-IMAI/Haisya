using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("T_Nippou_Anken")]
public partial class T_Nippou_Anken
{
    [Key]
    public int Nippou_ID { get; set; }

    public DateOnly Day { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Start_Datetime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? End_Datetime { get; set; }

    public int? Start_Degitako_Id { get; set; }

    public int? End_Degitako_Id { get; set; }

    /// <summary>
    /// 区間距離
    /// </summary>
    public double? Distance { get; set; }

    [Column(TypeName = "money")]
    public decimal Dllowance { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? BreakTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? WorkTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ActualWorkTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Insert_Datetime { get; set; }

    public int? Insert_User { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Update_Datetime { get; set; }

    public int? Update_User { get; set; }
}
