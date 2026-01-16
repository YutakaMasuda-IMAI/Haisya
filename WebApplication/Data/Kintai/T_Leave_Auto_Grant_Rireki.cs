using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Kintai;

[Table("T_Leave_Auto_Grant_Rireki")]
public partial class T_Leave_Auto_Grant_Rireki
{
    [Key]
    public int ID { get; set; }

    public int WORKER_CD { get; set; }

    [StringLength(100)]
    public string WORKER_NAME { get; set; }

    [StringLength(50)]
    public string STATUS { get; set; }

    public DateOnly? TAISYOKU_DATE { get; set; }

    public DateOnly? NYUSYA_DATE { get; set; }

    [StringLength(30)]
    public string OFFICE { get; set; }

    public DateOnly? ADD_DAY { get; set; }

    public int LEAVE_CD { get; set; }

    public int? ADD_TARGET_MONTH { get; set; }

    public int? NEN { get; set; }

    public int? TSUKI { get; set; }

    public int? MON { get; set; }

    public double? ADD_DAYS { get; set; }

    public DateOnly? LIMIT_DATE { get; set; }

    [StringLength(2)]
    public string ADD_FLG { get; set; }

    [StringLength(50)]
    public string NG_MESSAGE { get; set; }

    public int? TOTALWORK_DAYS { get; set; }

    public int? ATTENDANCE_DAYS { get; set; }

    public int? NON_ATTENDANCE_DAYS { get; set; }

    public int? LEAVE_DAYS { get; set; }

    public int? NON_LEAVE_DAYS { get; set; }

    public double? ATTENDANCE_RATE { get; set; }

    public double? NON_ATTENDANCE_RATE { get; set; }

    public double? LEAVE_RATE { get; set; }
}
