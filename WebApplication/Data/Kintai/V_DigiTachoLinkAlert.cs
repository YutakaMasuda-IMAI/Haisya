using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Keyless]
    public partial class V_DigiTachoLinkAlert
    {
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
        public int? 未連携 { get; set; }
    }
}
