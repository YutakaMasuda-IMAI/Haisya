using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Kintai;

[Table("T_ALC_CHECK")]
public partial class T_ALC_CHECK
{
    [Key]
    public int ID { get; set; }

    [StringLength(1)]
    public string NO { get; set; }

    [Required]
    [StringLength(6)]
    public string 月 { get; set; }

    public int WORKER_CD { get; set; }

    [StringLength(255)]
    public string NAME { get; set; }

    [StringLength(5)]
    public string ALC { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime IMPLE_TIME { get; set; }

    [StringLength(15)]
    public string DATETIME2 { get; set; }

    [StringLength(4)]
    public string FUMEI1 { get; set; }

    [Required]
    [StringLength(1)]
    public string STATUS { get; set; }
}
