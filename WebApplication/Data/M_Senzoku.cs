using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("M_Senzoku")]
    public partial class M_Senzoku
    {
        [Key]
        public int SenzokuID { get; set; }
        public int Company_ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Senzoku_Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Senzoku_Name_Abbr { get; set; }
        public int KokyakuId { get; set; }
        public int KokyakuTantouId { get; set; }
        public int Seikyu_Kubun { get; set; }
        public int Calc_Kubun { get; set; }
        [Column(TypeName = "money")]
        public decimal Monthly_Fee { get; set; }
        [Column(TypeName = "money")]
        public decimal Daily_Fee { get; set; }
        public int Haisya_Group_ID { get; set; }
        [StringLength(20)]
        public string Syasyu { get; set; }
        [StringLength(20)]
        public string SyasyuSize { get; set; }
        [StringLength(50)]
        public string SyasyuDisplay { get; set; }
        [StringLength(20)]
        public string Kata { get; set; }
    }
}
