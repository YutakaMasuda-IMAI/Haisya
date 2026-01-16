using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Kintai;

[PrimaryKey("LEAVE_CD", "REASON_CD", "Company_ID")]
[Table("M_LEAVE_REASON")]
[Index("REASON_NAME", Name = "IX_M_LEAVE_REASON", IsUnique = true)]
public partial class M_LEAVE_REASON
{
    [Key]
    public int REASON_CD { get; set; }

    [Key]
    public int LEAVE_CD { get; set; }

    [Key]
    public int Company_ID { get; set; }

    public int SORT_ORDER { get; set; }

    [Required]
    [StringLength(50)]
    public string REASON_NAME { get; set; }

    public bool DEL_FLG { get; set; }
}
