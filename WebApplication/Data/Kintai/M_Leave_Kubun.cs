using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Table("M_Leave_Kubun")]
    public partial class M_Leave_Kubun
    {
        [Key]
        public int Leave_Kubun { get; set; }
        [Key]
        public int Company_ID { get; set; }
        [Required]
        [StringLength(20)]
        public string Leave_Kubun_Name { get; set; }
        [Required]
        [StringLength(10)]
        public string Leave_Kubun_Name_Abbr { get; set; }
        public bool Special_Leave_Flg { get; set; }
        public bool Limit_Flg { get; set; }
        public bool Del_Flg { get; set; }
    }
}
