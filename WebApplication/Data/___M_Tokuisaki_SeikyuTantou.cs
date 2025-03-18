using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("___M_Tokuisaki_SeikyuTantou")]
    public partial class ___M_Tokuisaki_SeikyuTantou
    {
        [Key]
        public int コード { get; set; }
        public int? Tantou1 { get; set; }
        public int? Tantou2 { get; set; }
        public int? Tantou3 { get; set; }
        public int? Tantou4 { get; set; }
        public int? Tantou5 { get; set; }
    }
}
