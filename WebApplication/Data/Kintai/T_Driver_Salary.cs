using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Kintai;

[PrimaryKey("WORKER_CD", "Month")]
[Table("T_Driver_Salary")]
public partial class T_Driver_Salary
{
    [Key]
    public int WORKER_CD { get; set; }

    [Key]
    public DateOnly Month { get; set; }

    [Required]
    [StringLength(50)]
    public string 車種 { get; set; }

    [Column(TypeName = "money")]
    public decimal 祝日公休手当 { get; set; }

    [Column(TypeName = "money")]
    public decimal 時間外支給額 { get; set; }

    [Column(TypeName = "money")]
    public decimal 深夜支給額 { get; set; }

    [Column(TypeName = "money")]
    public decimal? 定額残業 { get; set; }

    [Column(TypeName = "money")]
    public decimal? 定額深夜 { get; set; }

    public int ルート別割増 { get; set; }

    [StringLength(100)]
    public string 備考 { get; set; }
}
