using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Kintai;

[PrimaryKey("LEAVE_CD", "Company_ID")]
[Table("M_LEAVE")]
[Index("LEAVE_NAME", Name = "IX_M_LEAVE", IsUnique = true)]
[Index("ABBR_NAME", Name = "IX_M_LEAVE_1", IsUnique = true)]
public partial class M_LEAVE
{
    [Key]
    public int LEAVE_CD { get; set; }

    [Key]
    public int Company_ID { get; set; }

    [Required]
    [StringLength(50)]
    public string LEAVE_NAME { get; set; }

    [StringLength(10)]
    public string ABBR_NAME { get; set; }

    [StringLength(20)]
    public string OUTPUT_NAME { get; set; }

    public int SORT_ORDER { get; set; }

    public bool DEL_FLG { get; set; }

    public bool AUTO_EXEC_FLG { get; set; }

    public bool LEAVE_FLG { get; set; }

    public bool DEF_LEAVE_FLG { get; set; }

    public double DAYS { get; set; }

    public bool IKKATSU_FLG { get; set; }

    public bool WORKING_FLG { get; set; }

    public bool ATTENDANCE_FLG { get; set; }
}
