using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Kintai;

[Table("M_BATCH")]
[Index("BATCH_NAME", "PROC_NAME", Name = "IX_M_BATCH", IsUnique = true)]
public partial class M_BATCH
{
    [Key]
    public int ID { get; set; }

    [Required]
    [StringLength(50)]
    public string BATCH_NAME { get; set; }

    [StringLength(50)]
    public string PROC_NAME { get; set; }

    [Required]
    [StringLength(50)]
    public string BATCH_NAME_DISPLAY { get; set; }

    [StringLength(50)]
    public string PROC_NAME_DISPLAY { get; set; }
}
