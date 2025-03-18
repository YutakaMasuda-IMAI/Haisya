using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Table("M_Leave_Setting_Table")]
    public partial class M_Leave_Setting_Table
    {
        [Key]
        public int Leave_Setting_CD { get; set; }
        [Key]
        public int Leave_Setting_Month { get; set; }
        [Required]
        [StringLength(50)]
        public string Leave_Setting_Display { get; set; }
        public double Leave_Setting_Table_Val { get; set; }
    }
}
