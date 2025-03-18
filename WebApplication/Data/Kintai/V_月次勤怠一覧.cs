using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Keyless]
    public partial class V_月次勤怠一覧
    {
        [StringLength(30)]
        public string 事業所名 { get; set; }
        public int 乗務員CD { get; set; }
        [Required]
        [StringLength(100)]
        public string 乗務員名 { get; set; }
        public int? 確定日数 { get; set; }
        public int 未確定データ数 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 最新の勤怠日 { get; set; }
        [Column(TypeName = "date")]
        public DateTime? 退職日 { get; set; }
    }
}
