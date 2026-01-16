using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("T_Nippou_Anken_Degitako")]
public partial class T_Nippou_Anken_Degitako
{
    [Key]
    public int Nippou_Anken_Degi_ID { get; set; }

    public int Nippou_ID { get; set; }

    public int KUDGIVT_ID { get; set; }

    [StringLength(22)]
    public string 運行NO { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 読取日 { get; set; }

    [StringLength(255)]
    public string 事業所名 { get; set; }

    public int? 車輌CD { get; set; }

    [StringLength(80)]
    public string 車輌名 { get; set; }

    public int? 乗務員CD { get; set; }

    [StringLength(80)]
    public string 乗務員名 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 開始日時 { get; set; }

    [StringLength(80)]
    public string イベント名 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 終了日時 { get; set; }

    public double? 開始走行距離 { get; set; }

    public double? 終了走行距離 { get; set; }

    public int? 区間時間 { get; set; }

    public double? 区間距離 { get; set; }

    [StringLength(100)]
    public string 開始市町村名 { get; set; }

    [StringLength(100)]
    public string 終了市町村名 { get; set; }

    [StringLength(100)]
    public string 開始場所名 { get; set; }

    [StringLength(100)]
    public string 終了場所名 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 早朝深夜_休憩 { get; set; }
}
