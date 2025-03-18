using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Table("M_Salary_Syakaku")]
    public partial class M_Salary_Syakaku
    {
        [Key]
        public int Syakaku_ID { get; set; }
        [Required]
        [StringLength(20)]
        public string Syakaku_Name { get; set; }
        [StringLength(20)]
        public string Display_Name { get; set; }
        public double Base_Rate { get; set; }
        public double Add_Rate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CarWash_MonthlyTime { get; set; }
    }
}
