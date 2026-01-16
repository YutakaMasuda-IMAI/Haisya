using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Kintai;

[Keyless]
public partial class V_燃料無駄候補リスト
{
    [StringLength(255)]
    public string 事業所名 { get; set; }

    public int? 車輌CD { get; set; }

    [StringLength(80)]
    public string 車輌名 { get; set; }

    public int 乗務員CD { get; set; }

    [StringLength(80)]
    public string 乗務員名 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime 勤怠日 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 実総労働時間 { get; set; }

    public double? 走行距離 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 休息時間合計 { get; set; }
}
