using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("B_Uriage_Month")]
    public partial class B_Uriage_Month
    {
        [Key]
        public int Bak_Uriage_Month_ID { get; set; }
        public int Customer_Branch_ID { get; set; }
        public int Check_Seikyu_ID { get; set; }
        public int Seikyu_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Del_Datetime { get; set; }
        public int Print_Pattern { get; set; }
        [Column(TypeName = "date")]
        public DateTime Seikyu_Month { get; set; }
        [Column(TypeName = "date")]
        public DateTime Shime_Date { get; set; }
        public int Zei_Kubun { get; set; }
        [StringLength(40)]
        public string Customer_Name { get; set; }
        [StringLength(16)]
        public string Customer_Name_Kana { get; set; }
        [StringLength(10)]
        public string Seikyu_Date { get; set; }
        [Column(TypeName = "money")]
        public decimal? Seikyu_Previous { get; set; }
        [Column(TypeName = "money")]
        public decimal? Received_Amount_This { get; set; }
        [Column(TypeName = "money")]
        public decimal? Balance_Forward { get; set; }
        [Column(TypeName = "money")]
        public decimal? Taxable_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Non_Taxable_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Tax_Amout { get; set; }
        [Column(TypeName = "money")]
        public decimal? Tax_Included_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Seikyu_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Cash_Received_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Check_Received_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Transfer_Received_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Draft_Received_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Service_Charge_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Unchin_Offset { get; set; }
        [Column(TypeName = "money")]
        public decimal? General_Offset { get; set; }
        [Column(TypeName = "money")]
        public decimal? Adjustment_Amount { get; set; }
        public double? Qty_Total { get; set; }
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
        public decimal? Seikyu_Total_Amount { get; set; }
        [Column(TypeName = "date")]
        public DateTime? FROM_DATE { get; set; }
        [Column(TypeName = "date")]
        public DateTime? TO_DATE { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UP_DATE { get; set; }
    }
}
