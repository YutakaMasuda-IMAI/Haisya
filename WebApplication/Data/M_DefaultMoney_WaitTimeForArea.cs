using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[PrimaryKey("SyasyuSize", "Area")]
[Table("M_DefaultMoney_WaitTimeForArea")]
public partial class M_DefaultMoney_WaitTimeForArea
{
    [Key]
    [StringLength(10)]
    public string Area { get; set; }

    [Key]
    [StringLength(20)]
    public string SyasyuSize { get; set; }

    [Column(TypeName = "money")]
    public decimal Amount { get; set; }

    [Column(TypeName = "money")]
    public decimal Amount_Over2Hour { get; set; }
}
