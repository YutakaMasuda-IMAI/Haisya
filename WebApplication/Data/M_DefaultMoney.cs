using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("M_DefaultMoney")]
    public partial class M_DefaultMoney
    {
        [Key]
        [StringLength(10)]
        public string Area { get; set; }
        [Key]
        [StringLength(20)]
        public string SyasyuSize { get; set; }
        [Key]
        public int From_Distance { get; set; }
        public int To_Distance { get; set; }
        public int Interval { get; set; }
        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? AdditionAmount { get; set; }
    }
}
