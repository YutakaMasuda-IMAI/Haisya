using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("T_Nippou_Stay")]
public partial class T_Nippou_Stay
{
    [Key]
    public int Nippou_ID { get; set; }

    public DateOnly Day { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Start_Datetime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? End_Datetime { get; set; }

    /// <summary>
    /// 開始市町村名
    /// </summary>
    [StringLength(255)]
    public string Start_ShikuName { get; set; }

    /// <summary>
    /// 終了市町村名
    /// </summary>
    [StringLength(255)]
    public string End_ShikuName { get; set; }

    /// <summary>
    /// 開始場所名
    /// </summary>
    [StringLength(255)]
    public string Start_PointName { get; set; }

    /// <summary>
    /// 終了場所名
    /// </summary>
    [StringLength(255)]
    public string End_PointName { get; set; }

    /// <summary>
    /// 区間距離
    /// </summary>
    public int? Interval_Time { get; set; }

    [Column(TypeName = "money")]
    public decimal Dllowance { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Insert_Datetime { get; set; }

    public int? Insert_User { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Update_Datetime { get; set; }

    public int? Update_User { get; set; }
}
