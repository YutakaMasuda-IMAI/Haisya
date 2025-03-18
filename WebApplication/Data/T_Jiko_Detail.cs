using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Jiko_Detail")]
    public partial class T_Jiko_Detail
    {
        [Key]
        public int Jiko_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        public int Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Update_Datetime { get; set; }
        public int Update_User { get; set; }
        public int Jiko_Weather_Kubun { get; set; }
        [StringLength(255)]
        public string Address { get; set; }
        [StringLength(255)]
        public string Address_Code { get; set; }
        [StringLength(10)]
        public string Address_Level { get; set; }
        [StringLength(100)]
        public string Lng { get; set; }
        [StringLength(100)]
        public string Lat { get; set; }
        [StringLength(10)]
        public string Post_code { get; set; }
        [StringLength(50)]
        public string Address2 { get; set; }
        [StringLength(50)]
        public string Address3 { get; set; }
        [StringLength(50)]
        public string Address4 { get; set; }
        [StringLength(255)]
        public string Address_Remarks { get; set; }
    }
}
