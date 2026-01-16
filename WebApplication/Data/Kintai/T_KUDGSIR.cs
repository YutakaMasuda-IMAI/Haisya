using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Kintai;

[Keyless]
public partial class T_KUDGSIR
{
    public int ID { get; set; }

    [StringLength(255)]
    public string 運行NO { get; set; }

    public DateOnly? 読取日 { get; set; }

    public int? 事業所CD { get; set; }

    public DateOnly? 運行日 { get; set; }

    [StringLength(255)]
    public string 事業所名 { get; set; }

    public int? 車輌CD { get; set; }

    [StringLength(255)]
    public string 車輌名 { get; set; }

    public int? 乗務員CD { get; set; }

    [StringLength(255)]
    public string 乗務員名 { get; set; }

    public int? 対象乗務員区分 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 開始日時 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 終了日時 { get; set; }

    [StringLength(20)]
    public string 開始道路番号 { get; set; }

    [StringLength(50)]
    public string 開始道路名 { get; set; }

    [StringLength(50)]
    public string 開始ETC番号 { get; set; }

    [StringLength(50)]
    public string 開始IC名 { get; set; }

    [StringLength(20)]
    public string 終了道路番号 { get; set; }

    [StringLength(50)]
    public string 終了道路名 { get; set; }

    [StringLength(50)]
    public string 終了ETC番号 { get; set; }

    [StringLength(50)]
    public string 終了IC名 { get; set; }

    public int? 精算区分 { get; set; }

    [StringLength(50)]
    public string 精算区分名 { get; set; }

    [Column(TypeName = "money")]
    public decimal? 料金 { get; set; }

    public double? 走行距離 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime 取り込み日 { get; set; }

    public int 取り込みID { get; set; }

    [StringLength(50)]
    public string 読取NO { get; set; }
}
