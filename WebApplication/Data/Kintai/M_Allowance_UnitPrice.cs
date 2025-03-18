using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Table("M_Allowance_UnitPrice")]
    public partial class M_Allowance_UnitPrice
    {
        [Key]
        public int Allowance_ID { get; set; }
        [Key]
        [Column(TypeName = "date")]
        public DateTime Start_Month { get; set; }
        [Column(TypeName = "money")]
        public decimal Unit_Price { get; set; }
    }
}
