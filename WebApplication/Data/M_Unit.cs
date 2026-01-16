using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("M_Unit")]
public partial class M_Unit
{
    [Key]
    public int Unit_ID { get; set; }

    public int Company_ID { get; set; }

    public int? Sort_Order { get; set; }

    [StringLength(10)]
    public string Unit_Display { get; set; }

    [StringLength(10)]
    public string Unit_Format { get; set; }

    public int? Del_Flg { get; set; }
}
