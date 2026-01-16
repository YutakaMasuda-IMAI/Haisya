using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Kintai;

[Keyless]
public partial class V_差分チェック2
{
    [StringLength(255)]
    public string 運行NO { get; set; }

    public long? EDA { get; set; }

    public int KUBUN { get; set; }

    [StringLength(255)]
    public string 事業所名1 { get; set; }

    [StringLength(255)]
    public string 事業所名2 { get; set; }

    public int? 車輌CD1 { get; set; }

    public int? 車輌CD2 { get; set; }

    [StringLength(255)]
    public string 車輌名1 { get; set; }

    [StringLength(255)]
    public string 車輌名2 { get; set; }

    public int? 乗務員CD { get; set; }

    public int? 乗務員CD2 { get; set; }

    [StringLength(255)]
    public string 乗務員名1 { get; set; }

    [StringLength(255)]
    public string 乗務員名2 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 開始日時1 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 開始日時2 { get; set; }

    [StringLength(255)]
    public string イベント名1 { get; set; }

    [StringLength(255)]
    public string イベント名2 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 終了日時1 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 終了日時2 { get; set; }

    public double? 開始走行距離1 { get; set; }

    public double? 開始走行距離2 { get; set; }

    public double? 終了走行距離1 { get; set; }

    public double? 終了走行距離2 { get; set; }

    public int? 区間時間1 { get; set; }

    public int? 区間時間2 { get; set; }

    public double? 区間距離1 { get; set; }

    public double? 区間距離2 { get; set; }

    [StringLength(255)]
    public string 開始市町村名1 { get; set; }

    [StringLength(255)]
    public string 開始市町村名2 { get; set; }

    [StringLength(255)]
    public string 終了市町村名1 { get; set; }

    [StringLength(255)]
    public string 終了市町村名2 { get; set; }

    [StringLength(255)]
    public string 開始場所名1 { get; set; }

    [StringLength(255)]
    public string 開始場所名2 { get; set; }

    [StringLength(255)]
    public string 終了場所名1 { get; set; }

    [StringLength(255)]
    public string 終了場所名2 { get; set; }
}
