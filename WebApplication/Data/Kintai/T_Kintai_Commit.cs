using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Kintai;

[PrimaryKey("乗務員CD", "勤怠日")]
[Table("T_KINTAI_COMMIT")]
public partial class T_KINTAI_COMMIT
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
    [Column(TypeName = "date")]
    public DateOnly 勤怠日 { get; set; }

    public int 勤怠区分 { get; set; }

    public int 勤務区分 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 運行開始 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 運行終了 { get; set; }

    public double? 走行距離 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 総労働時間 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 所定労働時間 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 残業時間 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? 実深夜労働時間 { get; set; }

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

    public int 休暇区分 { get; set; }

    public int 休暇理由 { get; set; }

    public int 休日出勤区分 { get; set; }

    /// <summary>
    /// 1:締め,2:仮
    /// </summary>
    public int 確定区分 { get; set; }

    public int 振替_ID { get; set; }

    public DateOnly? 振替休暇日 { get; set; }

    public DateOnly? 代休取得期限日 { get; set; }

    public int 振替休暇理由 { get; set; }

    public int 手当区分 { get; set; }
}
