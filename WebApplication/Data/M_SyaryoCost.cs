using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("M_SyaryoCost")]
    public partial class M_SyaryoCost
    {
        [Key]
        public int Syaryo_ID { get; set; }
        [Key]
        public int From_Distance { get; set; }
        public int To_Distance { get; set; }
        public int Company_ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Syasyu { get; set; }
        [Required]
        [StringLength(20)]
        public string Kata { get; set; }
        public int Interval { get; set; }
        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? AdditionAmount { get; set; }
    }
}
