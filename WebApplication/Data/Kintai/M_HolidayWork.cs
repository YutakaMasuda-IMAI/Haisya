using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Table("M_HolidayWork")]
    public partial class M_HolidayWork
    {
        [Key]
        public int HolidayWork_CD { get; set; }
        [Required]
        [StringLength(20)]
        public string HolidayWork_Name { get; set; }
        public bool HolidayWork_DEL { get; set; }
        public int Target_LEAVE_CD { get; set; }
    }
}
