using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[PrimaryKey("Company_ID", "Area", "SyasyuSize")]
[Table("M_DefaultMoney_WaitTimeForCompany")]
public partial class M_DefaultMoney_WaitTimeForCompany
{
    [Key]
    public int Company_ID { get; set; }

    [Key]
    [StringLength(10)]
    public string Area { get; set; }

    [Key]
    [StringLength(20)]
    public string SyasyuSize { get; set; }

    [Column(TypeName = "money")]
    public decimal? Amount { get; set; }

    [Column(TypeName = "money")]
    public decimal Amount_Over2Hour { get; set; }
}
