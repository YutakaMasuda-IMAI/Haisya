using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.NPData
{
    [Table("T_KUDGIVT")]
    [Index(nameof(取り込み日), nameof(取り込みID), Name = "IX_T_KUDGIVT", IsUnique = true)]
    [Index(nameof(乗務員CD), nameof(開始日時), Name = "IX_T_KUDGIVT_1")]
    [Index(nameof(運行NO), Name = "IX_T_KUDGIVT_2")]
    public partial class T_KUDGIVT
    {
        [Key]
        public int ID { get; set; }
        [StringLength(22)]
        public string 運行NO { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 読取日 { get; set; }
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
        public DateTime? 開始日時 { get; set; }
        public int? イベントCD { get; set; }
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
        public int? 開始GPS方位 { get; set; }
        public int? 開始GPS有効 { get; set; }
        [StringLength(20)]
        public string 開始GPS緯度 { get; set; }
        [StringLength(20)]
        public string 開始GPS経度 { get; set; }
        public int? 終了GPS方位 { get; set; }
        public int? 終了GPS有効 { get; set; }
        [StringLength(20)]
        public string 終了GPS緯度 { get; set; }
        [StringLength(20)]
        public string 終了GPS経度 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 早朝深夜_休憩 { get; set; }
        [StringLength(10)]
        public string OP { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 日勤_休憩 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 取り込み日 { get; set; }
        public int 取り込みID { get; set; }
        [StringLength(1)]
        public string 休憩FLG { get; set; }
        [StringLength(1)]
        public string 休息FLG { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 次回運行開始 { get; set; }
        public int? 次回運行ID { get; set; }
    }
}
