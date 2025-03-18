using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Kintai")]
    public partial class T_Kintai
    {
        [Key]
        public int Driver_ID { get; set; }
        [Key]
        [Column(TypeName = "date")]
        public DateTime Day { get; set; }
        public int Renzoku_Days { get; set; }
        public double Work_Time { get; set; }
    }
}
