using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Keyless]
    [Table("ALC_TEMP")]
    public partial class ALC_TEMP
    {
        [StringLength(8)]
        public string CODE { get; set; }
        [StringLength(8)]
        public string DUMMY1 { get; set; }
        [StringLength(128)]
        public string NAME { get; set; }
        [StringLength(5)]
        public string ALC { get; set; }
        [StringLength(11)]
        public string DUMMY2 { get; set; }
        [StringLength(14)]
        public string DATETIME { get; set; }
        [StringLength(50)]
        public string DUMMY3 { get; set; }
        [StringLength(4)]
        public string FUMEI1 { get; set; }
        [StringLength(4)]
        public string DUMMY4 { get; set; }
        [StringLength(1)]
        public string STATUS { get; set; }
        [StringLength(247)]
        public string DUMMY5 { get; set; }
        [StringLength(6)]
        public string 月 { get; set; }
        public int ID { get; set; }
    }
}
