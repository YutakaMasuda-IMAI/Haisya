using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.NPData
{
    [Keyless]
    [Table("KUDGIVT_ANYTIME_FORUPDATE")]
    public partial class KUDGIVT_ANYTIME_FORUPDATE
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
        public string 運行開始 { get; set; }
        [StringLength(255)]
        public string 運行終了 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 勤怠開始 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 勤怠終了 { get; set; }
    }
}
