using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Keyless]
    public partial class KUDGIVT_ANYTIME
    {
        [StringLength(255)]
        public string 運行NO { get; set; }
        [StringLength(255)]
        public string 読取日 { get; set; }
        [StringLength(255)]
        public string 事業所名 { get; set; }
        public int? 車輌CD { get; set; }
        [StringLength(255)]
        public string 車輌名 { get; set; }
        public int? 乗務員CD { get; set; }
        [StringLength(255)]
        public string 乗務員名 { get; set; }
        [StringLength(255)]
        public string 開始日時 { get; set; }
        [StringLength(255)]
        public string イベント名 { get; set; }
        [StringLength(255)]
        public string 終了日時 { get; set; }
        public double? 開始走行距離 { get; set; }
        public double? 終了走行距離 { get; set; }
        public int? 区間時間 { get; set; }
        public double? 区間距離 { get; set; }
        [StringLength(255)]
        public string 開始市町村名 { get; set; }
        [StringLength(255)]
        public string 終了市町村名 { get; set; }
        [StringLength(255)]
        public string 開始場所名 { get; set; }
        [StringLength(255)]
        public string 終了場所名 { get; set; }
        public int ID { get; set; }
    }
}
