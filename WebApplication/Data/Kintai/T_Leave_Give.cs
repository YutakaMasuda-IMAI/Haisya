using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Kintai;

[Table("T_Leave_Give")]
public partial class T_Leave_Give
{
    [Key]
    public int Leave_Give_ID { get; set; }

    public int 乗務員CD { get; set; }

    public int LEAVE_CD { get; set; }

    public DateOnly Start_Date { get; set; }

    public double? Add_Days { get; set; }

    public DateOnly? Limit_Date { get; set; }

    public int Kintai_ID { get; set; }

    [StringLength(255)]
    public string Remarks { get; set; }
}
