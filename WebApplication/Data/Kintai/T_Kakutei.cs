using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Table("T_Kakutei")]
    public partial class T_Kakutei
    {
        [Key]
        [Column(TypeName = "date")]
        public DateTime Kakutei_Date { get; set; }
        public int? Kakutei_flg { get; set; }
    }
}
