using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("M_Senzoku")]
public partial class M_Senzoku
{
    [Key]
    public int SenzokuID { get; set; }

    public int Company_ID { get; set; }

    [Required]
    [StringLength(50)]
    public string Senzoku_Name { get; set; }

    [Required]
    [StringLength(50)]
    public string Senzoku_Name_Abbr { get; set; }

    public int KokyakuId { get; set; }

    public int KokyakuTantouId { get; set; }

    /// <summary>
    /// 0:案件ごと、1:月額
    /// </summary>
    public int Seikyu_Kubun { get; set; }

    /// <summary>
    /// 0:月額から計算、１:日額から計算
    /// </summary>
    public int Calc_Kubun { get; set; }

    [Column(TypeName = "money")]
    public decimal Monthly_Fee { get; set; }

    [Column(TypeName = "money")]
    public decimal Daily_Fee { get; set; }

    public int Haisya_Group_ID { get; set; }

    [StringLength(20)]
    public string Syasyu { get; set; }

    [StringLength(20)]
    public string SyasyuSize { get; set; }

    [StringLength(50)]
    public string SyasyuDisplay { get; set; }

    [StringLength(20)]
    public string Kata { get; set; }
}
