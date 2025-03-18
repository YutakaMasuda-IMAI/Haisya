using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Table("M_Driver")]
    public partial class M_Driver
    {
        [Key]
        public int WORKER_CD { get; set; }
        [Required]
        [StringLength(100)]
        public string WORKER_NAME { get; set; }
        [StringLength(50)]
        public string STATUS { get; set; }
        [Column(TypeName = "date")]
        public DateTime? GYOUMU_START { get; set; }
        [Column(TypeName = "date")]
        public DateTime? TAISYOKU_DATE { get; set; }
        [Column(TypeName = "date")]
        public DateTime? NYUSYA_DATE { get; set; }
        [StringLength(30)]
        public string OFFICE { get; set; }
        public bool Del_Flg { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UP_DATE { get; set; }
        public bool NotKintaiFlg { get; set; }
    }
}
