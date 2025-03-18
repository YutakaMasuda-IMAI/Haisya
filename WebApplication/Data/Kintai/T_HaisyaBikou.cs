using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Table("T_HaisyaBikou")]
    public partial class T_HaisyaBikou
    {
        [Key]
        public int ID { get; set; }
        public int Driver_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
    }
}
