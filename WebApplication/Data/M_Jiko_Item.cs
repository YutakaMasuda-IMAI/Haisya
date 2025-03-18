using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    public partial class M_Jiko_Item
    {
        [Key]
        public int Jiko_Items_ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Jiko_Items_Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Jiko_Items_Prop_Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Jiko_Items_Type { get; set; }
    }
}
