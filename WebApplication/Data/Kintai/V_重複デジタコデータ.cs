using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Keyless]
    public partial class V_重複デジタコデータ
    {
        [StringLength(255)]
        public string 事業所名 { get; set; }
        public int? 乗務員CD { get; set; }
        [StringLength(80)]
        public string 乗務員名 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 業務日 { get; set; }
        public int? 車輌CD1 { get; set; }
        [StringLength(80)]
        public string 車輌名1 { get; set; }
        public int? 車輌CD2 { get; set; }
        [StringLength(80)]
        public string 車輌名2 { get; set; }
        public int? 車輌CD3 { get; set; }
        [StringLength(80)]
        public string 車輌名3 { get; set; }
    }
}
