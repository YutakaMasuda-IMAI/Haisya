using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.NPData
{
    [Table("KUDGURI")]
    public partial class KUDGURI
    {
        [StringLength(22)]
        public string 運行NO { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 運行日 { get; set; }
        public int? 事業所CD { get; set; }
        [StringLength(255)]
        public string 事業所名 { get; set; }
        public int? 車輌CD { get; set; }
        [StringLength(80)]
        public string 車輌名 { get; set; }
        public int? 乗務員CD { get; set; }
        [StringLength(80)]
        public string 乗務員名 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 出庫日時 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 帰庫日時 { get; set; }
        public double? 総走行距離 { get; set; }
        public double? 総走行時間 { get; set; }
        public double? 高速道走行距離 { get; set; }
        public double? 高速道走行時間 { get; set; }
        public int? 作業２時間 { get; set; }
        public int? アイドリング時間 { get; set; }
        public double? 出庫メーター { get; set; }
        public double? 帰庫メーター { get; set; }
        [Key]
        public int ID { get; set; }
    }
}
