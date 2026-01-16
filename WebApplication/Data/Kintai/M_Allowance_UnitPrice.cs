using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Kintai;

[PrimaryKey("Allowance_ID", "Start_Month")]
[Table("M_Allowance_UnitPrice")]
public partial class M_Allowance_UnitPrice
{
    [Key]
    public int Allowance_ID { get; set; }

    [Key]
    public DateOnly Start_Month { get; set; }

    [Column(TypeName = "money")]
    public decimal Unit_Price { get; set; }
}
