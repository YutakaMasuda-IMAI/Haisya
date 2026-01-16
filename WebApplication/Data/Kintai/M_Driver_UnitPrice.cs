using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Kintai;

[PrimaryKey("WORKER_CD", "Start_Month")]
[Table("M_Driver_UnitPrice")]
public partial class M_Driver_UnitPrice
{
    [Key]
    public int WORKER_CD { get; set; }

    [Key]
    public DateOnly Start_Month { get; set; }

    public int Syakaku_ID { get; set; }

    [Column(TypeName = "money")]
    public decimal UnitPrice_Day { get; set; }

    [Column(TypeName = "money")]
    public decimal Licence_Allowance { get; set; }

    [Column(TypeName = "money")]
    public decimal Responsible_Allowance { get; set; }
}
