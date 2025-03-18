using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Jiko_Type")]
    public partial class T_Jiko_Type
    {
        [Key]
        public int Jiko_ID { get; set; }
        [Key]
        public int Jiko_Type_ID { get; set; }
        [StringLength(255)]
        public string Jiko_Type_Remarks { get; set; }
    }
}
