using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Keyless]
    [Table("KEN_ALL2")]
    public partial class KEN_ALL2
    {
        [Column("列 0")]
        [StringLength(50)]
        public string 列_0 { get; set; }
        [Column("列 1")]
        [StringLength(50)]
        public string 列_1 { get; set; }
        [Column("列 2")]
        [StringLength(50)]
        public string 列_2 { get; set; }
        [Column("列 3")]
        [StringLength(50)]
        public string 列_3 { get; set; }
        [Column("列 4")]
        [StringLength(50)]
        public string 列_4 { get; set; }
        [Column("列 5")]
        [StringLength(50)]
        public string 列_5 { get; set; }
        [Column("列 6")]
        [StringLength(50)]
        public string 列_6 { get; set; }
        [Column("列 7")]
        [StringLength(50)]
        public string 列_7 { get; set; }
        [Column("列 8")]
        [StringLength(50)]
        public string 列_8 { get; set; }
        [Column("列 9")]
        [StringLength(50)]
        public string 列_9 { get; set; }
        [Column("列 10")]
        [StringLength(50)]
        public string 列_10 { get; set; }
        [Column("列 11")]
        [StringLength(50)]
        public string 列_11 { get; set; }
        [Column("列 12")]
        [StringLength(50)]
        public string 列_12 { get; set; }
        [Column("列 13")]
        [StringLength(50)]
        public string 列_13 { get; set; }
        [Column("列 14")]
        [StringLength(50)]
        public string 列_14 { get; set; }
    }
}
