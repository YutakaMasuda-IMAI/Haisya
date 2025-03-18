using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Table("M_Holiday")]
    public partial class M_Holiday
    {
        [Key]
        [Column(TypeName = "date")]
        public DateTime Day { get; set; }
        [StringLength(10)]
        public string WeekDay { get; set; }
        [StringLength(50)]
        public string HolidayName { get; set; }
    }
}
