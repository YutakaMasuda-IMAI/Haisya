using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Keyless]
    public partial class V_計算エラー
    {
        public int ID { get; set; }
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
        public DateTime? 勤怠日 { get; set; }
        public int? 枝番 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 業務開始 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 業務終了 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 運行開始 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 運行終了 { get; set; }
        public double? 走行距離 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 総労働時間 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 実総労働時間 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 深夜労働時間 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 深夜休憩時間 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 実深夜労働時間 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 休憩時間 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 休息時間合計 { get; set; }
    }
}
