using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Table("M_Role")]
    public partial class M_Role
    {
        [Key]
        public int Company_ID { get; set; }
        [Key]
        public int Role { get; set; }
        [Key]
        [StringLength(50)]
        public string Controller { get; set; }
        [Key]
        [StringLength(100)]
        public string Action { get; set; }
        [Key]
        [StringLength(50)]
        public string Method { get; set; }
        public bool Enabled { get; set; }
    }
}
