using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Kintai;

[Table("T_Leave_Summary")]
public partial class T_Leave_Summary
{
    [Key]
    public int 乗務員CD { get; set; }

    public double Leave_Days { get; set; }
}
