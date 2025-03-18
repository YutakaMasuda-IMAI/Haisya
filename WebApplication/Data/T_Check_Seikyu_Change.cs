using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Check_Seikyu_Change")]
    public partial class T_Check_Seikyu_Change
    {
        [Key]
        public int Check_Seikyu_ID { get; set; }
        [Key]
        public int Uriage_Unchin_ID { get; set; }
        public double? Qty { get; set; }
        public int? Unit { get; set; }
        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }
        [Column(TypeName = "money")]
        public decimal? CalcPrice { get; set; }
        [Column(TypeName = "money")]
        public decimal? SeikyuUnchin { get; set; }
        [Column(TypeName = "money")]
        public decimal? Tatekaekin { get; set; }
        [Column(TypeName = "money")]
        public decimal? Warimashi1 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Warimashi2 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Warimashi3 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Warimashi4 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Warimashi5 { get; set; }
        [Column(TypeName = "money")]
        public decimal? SeikyuTotal { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        public int Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Update_Datetime { get; set; }
        public int Update_User { get; set; }
    }
}
