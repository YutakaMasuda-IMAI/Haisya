using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Kintai;

[Table("M_Allowance")]
public partial class M_Allowance
{
    [Key]
    public int Allowance_ID { get; set; }

    public int Sort { get; set; }

    [Required]
    [StringLength(20)]
    public string Display_Name { get; set; }

    [Required]
    [StringLength(20)]
    public string Abbr_Name { get; set; }

    public bool Del_Flg { get; set; }
}
