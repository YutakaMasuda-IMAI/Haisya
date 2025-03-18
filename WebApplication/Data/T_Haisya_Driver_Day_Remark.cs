using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    public partial class T_Haisya_Driver_Day_Remark
    {
        [Key]
        public int Driver_ID { get; set; }
        [Key]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        [StringLength(50)]
        public string Remarks { get; set; }
    }
}
