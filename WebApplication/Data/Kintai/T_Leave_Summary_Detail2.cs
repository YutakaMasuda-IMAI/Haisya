using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Table("T_Leave_Summary_Detail2")]
    public partial class T_Leave_Summary_Detail2
    {
        [Key]
        public int 乗務員CD { get; set; }
        [Key]
        public int LEAVE_CD { get; set; }
        [Key]
        public int Leave_Give_ID { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Limit_Date { get; set; }
        public int Company_ID { get; set; }
        public double Leave_Days { get; set; }
    }
}
