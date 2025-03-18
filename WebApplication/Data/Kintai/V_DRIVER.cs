using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Keyless]
    public partial class V_DRIVER
    {
        public int 乗務員CD { get; set; }
        [StringLength(50)]
        public string SYASYU { get; set; }
        [StringLength(50)]
        public string KATA { get; set; }
        [StringLength(50)]
        public string KUBUN { get; set; }
        public int? HAISYA_ID { get; set; }
        [Required]
        [StringLength(100)]
        public string 乗務員名 { get; set; }
        [StringLength(50)]
        public string STATUS { get; set; }
        [Column(TypeName = "date")]
        public DateTime? 業務開始日 { get; set; }
        [Column(TypeName = "date")]
        public DateTime? 退職日 { get; set; }
        [Column(TypeName = "date")]
        public DateTime? 入社日 { get; set; }
        [StringLength(30)]
        public string 事業所名 { get; set; }
        public bool 勤怠管理対象外 { get; set; }
        public int? NEN { get; set; }
        public int? TSUKI { get; set; }
        public int? MON { get; set; }
    }
}
