using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Anken_Excharge")]
    public partial class T_Anken_Excharge
    {
        [Key]
        public int Anken_ID { get; set; }
        [Key]
        public int Anken_Order { get; set; }
        [Key]
        public int Komoku_ID { get; set; }
        [Column(TypeName = "money")]
        public decimal StdExcharge { get; set; }
        [Column(TypeName = "money")]
        public decimal GrossExcharge { get; set; }
        [Column(TypeName = "money")]
        public decimal Excharge { get; set; }
    }
}
