using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Uriage_Shitabarai")]
    public partial class T_Uriage_Shitabarai
    {
        [Key]
        public int Uriage_Shiharai_ID { get; set; }
        public int Uriage_ID { get; set; }
        public int Sort { get; set; }
        public int Default_Kubun { get; set; }
        public int Uriage_Kubun { get; set; }
        [Column(TypeName = "date")]
        public DateTime Shiharai_Date { get; set; }
        public int Shime_Day { get; set; }
        [StringLength(5)]
        public string Syaban { get; set; }
        public int Yosya_Branch_ID { get; set; }
        [StringLength(20)]
        public string Tsumi { get; set; }
        [StringLength(20)]
        public string Oroshi { get; set; }
        [StringLength(20)]
        public string Luggage { get; set; }
        public double Qty { get; set; }
        public int Unit { get; set; }
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }
        [Column(TypeName = "money")]
        public decimal CalcPrice { get; set; }
        [Column(TypeName = "money")]
        public decimal ShiharaiPrice { get; set; }
        [Column(TypeName = "money")]
        public decimal WarimashiPrice { get; set; }
        [Column(TypeName = "money")]
        public decimal Tatekaekin { get; set; }
        public int Zei_Kubun { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
        public bool Del_Flg { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        public int Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Update_Datetime { get; set; }
        public int Update_User { get; set; }
    }
}
