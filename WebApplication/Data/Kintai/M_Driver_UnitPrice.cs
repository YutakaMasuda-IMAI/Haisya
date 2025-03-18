using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Table("M_Driver_UnitPrice")]
    public partial class M_Driver_UnitPrice
    {
        [Key]
        public int WORKER_CD { get; set; }
        [Key]
        [Column(TypeName = "date")]
        public DateTime Start_Month { get; set; }
        public int Syakaku_ID { get; set; }
        [Column(TypeName = "money")]
        public decimal UnitPrice_Day { get; set; }
        [Column(TypeName = "money")]
        public decimal Licence_Allowance { get; set; }
        [Column(TypeName = "money")]
        public decimal Responsible_Allowance { get; set; }
    }
}
