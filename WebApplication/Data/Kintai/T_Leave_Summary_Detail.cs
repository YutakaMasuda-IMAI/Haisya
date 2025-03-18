using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Table("T_Leave_Summary_Detail")]
    public partial class T_Leave_Summary_Detail
    {
        [Key]
        public int 乗務員CD { get; set; }
        [Key]
        public int LEAVE_CD { get; set; }
        public int Company_ID { get; set; }
        public double Leave_Days { get; set; }
    }
}
