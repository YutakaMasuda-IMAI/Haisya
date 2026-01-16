using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Kintai;

[PrimaryKey("乗務員CD", "勤怠日")]
[Table("T_KINTAI_COMMIT_NON_CREW")]
public partial class T_KINTAI_COMMIT_NON_CREW
{
    public int ID { get; set; }

    [StringLength(255)]
    public string 事業所名 { get; set; }

    public int? 車輌CD { get; set; }

    [StringLength(80)]
    public string 車輌名 { get; set; }

    [Key]
    public int 乗務員CD { get; set; }

    [StringLength(80)]
    public string 乗務員名 { get; set; }

    [Key]
    [Column(TypeName = "datetime")]
    public DateTime 勤怠日 { get; set; }

    public int? 勤怠区分 { get; set; }

    public int? 勤務区分 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 運行開始 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 運行終了 { get; set; }

    public double? 走行距離 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 実総労働時間 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 総労働時間 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 所定労働時間 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 残業時間 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 休憩時間 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 実深夜労働時間 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 深夜休憩時間 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 公休労働時間 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 法定休労働時間 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 更新日 { get; set; }

    [StringLength(50)]
    public string 更新者 { get; set; }

    public int? BASE_ID { get; set; }

    public int? 更新区分 { get; set; }

    public int? 乗務外業務区分 { get; set; }

    [StringLength(50)]
    public string 乗務外その他理由 { get; set; }

    [StringLength(50)]
    public string 乗務外作業項目 { get; set; }

    [StringLength(255)]
    public string 乗務外備考 { get; set; }
}
