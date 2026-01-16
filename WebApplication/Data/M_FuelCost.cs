using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[PrimaryKey("Company_ID", "Branch_ID", "FromDate")]
[Table("M_FuelCost")]
public partial class M_FuelCost
{
    [Key]
    public int Company_ID { get; set; }

    [Key]
    public int Branch_ID { get; set; }

    [Key]
    public DateOnly FromDate { get; set; }

    [Column(TypeName = "money")]
    public decimal FuelAmount { get; set; }
}
