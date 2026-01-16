using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("M_Area_Ken")]
public partial class M_Area_Ken
{
    [Key]
    [StringLength(10)]
    public string Ken { get; set; }

    public int Area_ID { get; set; }

    public int Company_ID { get; set; }
}
