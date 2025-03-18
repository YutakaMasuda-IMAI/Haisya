using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("M_Area")]
    [Index(nameof(Company_ID), nameof(Area), Name = "IX_M_Area", IsUnique = true)]
    public partial class M_Area
    {
        [Key]
        public int Area_ID { get; set; }
        [Key]
        public int Company_ID { get; set; }
        [Required]
        [StringLength(10)]
        public string Area { get; set; }
        public int Sort_Order { get; set; }
    }
}
