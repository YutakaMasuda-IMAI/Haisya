using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Keyless]
    [Table("KEN_ALL")]
    public partial class KEN_ALL
    {
        [Required]
        [StringLength(255)]
        public string column1 { get; set; }
        [Required]
        [StringLength(255)]
        public string column2 { get; set; }
        [Required]
        [StringLength(255)]
        public string column3 { get; set; }
        [Required]
        [StringLength(255)]
        public string column4 { get; set; }
        [Required]
        [StringLength(255)]
        public string column5 { get; set; }
        [Required]
        [StringLength(255)]
        public string column6 { get; set; }
        [Required]
        [StringLength(255)]
        public string column7 { get; set; }
        [Required]
        [StringLength(255)]
        public string column8 { get; set; }
        [Required]
        [StringLength(255)]
        public string column9 { get; set; }
        public int column10 { get; set; }
        public int column11 { get; set; }
        public int column12 { get; set; }
        public int column13 { get; set; }
        public int column14 { get; set; }
        public int column15 { get; set; }
    }
}
