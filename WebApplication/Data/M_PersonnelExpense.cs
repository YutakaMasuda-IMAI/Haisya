using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    public partial class M_PersonnelExpense
    {
        [Key]
        public int Company_ID { get; set; }
        [Key]
        [StringLength(50)]
        public string Syasyu { get; set; }
        [Key]
        [StringLength(20)]
        public string Kata { get; set; }
        [Column(TypeName = "money")]
        public decimal? Base { get; set; }
        [Column(TypeName = "money")]
        public decimal? Day { get; set; }
        [Column(TypeName = "money")]
        public decimal? Midnight { get; set; }
        [Column(TypeName = "money")]
        public decimal? Holiday { get; set; }
        [Column(TypeName = "money")]
        public decimal? HolidayMidnight { get; set; }
        public double? BenefitsCosts { get; set; }
        public double? IndirectCosts { get; set; }
    }
}
