using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Keyless]
    [Table("V_Print_Seikyu")]
    public partial class V_Print_Seikyu
    {
        public int RESULT { get; set; }
        public int? Customer_Branch_ID { get; set; }
        public int? Check_Seikyu_ID { get; set; }
        public int? Seikyu_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Del_Datetime { get; set; }
        public int? Print_Pattern { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Seikyu_Month { get; set; }
        public int? Shime_Day { get; set; }
        public int? Zei_Kubun { get; set; }
        [StringLength(50)]
        public string Mail_Title { get; set; }
        [StringLength(255)]
        public string Mail_Detail { get; set; }
        [StringLength(40)]
        public string Customer_Name { get; set; }
        [StringLength(16)]
        public string Customer_Name_Kana { get; set; }
        [StringLength(8)]
        public string PostCode { get; set; }
        [StringLength(50)]
        public string Mail_Address1 { get; set; }
        [StringLength(50)]
        public string Mail_Address2 { get; set; }
        [StringLength(80)]
        public string Address1 { get; set; }
        [StringLength(40)]
        public string Address2 { get; set; }
        [StringLength(40)]
        public string Address3 { get; set; }
        [StringLength(13)]
        public string Phone1 { get; set; }
        [StringLength(13)]
        public string Phone2 { get; set; }
        [StringLength(13)]
        public string Fax1 { get; set; }
        [StringLength(13)]
        public string Fax2 { get; set; }
        [StringLength(10)]
        public string Seikyu_Date { get; set; }
        [Column(TypeName = "money")]
        public decimal? Seikyu_Previous { get; set; }
        public int? Tax_Fraction_Kubun { get; set; }
        public double? Tax_Fraction_Position { get; set; }
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
        public decimal? Cash_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Check_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Transfer_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Draft_Amount { get; set; }
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
        public decimal? SeikyuUnchin_NoTax { get; set; }
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
        [Column(TypeName = "date")]
        public DateTime? SEIKYUDATE_TO { get; set; }
        public int? Seikyu_ID_Detail { get; set; }
        public int? Data_Kubun { get; set; }
        public int? Data_Sort { get; set; }
        public int? Uriage_Unchin_ID { get; set; }
        public int? Nyukin_ID { get; set; }
        public int? Anken_ID { get; set; }
        public int? Anken_ID_Detail { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? Display_Date { get; set; }
        [StringLength(5)]
        public string Syaban { get; set; }
        [StringLength(16)]
        public string SyasyuKataName { get; set; }
        [StringLength(16)]
        public string DriverName { get; set; }
        [StringLength(20)]
        public string Tsumi { get; set; }
        [StringLength(20)]
        public string Oroshi { get; set; }
        [StringLength(20)]
        public string Luggage { get; set; }
        [StringLength(100)]
        public string Work_Name { get; set; }
        [StringLength(30)]
        public string Remarks1 { get; set; }
        [StringLength(30)]
        public string Remarks2 { get; set; }
        public double? Qty { get; set; }
        public int? Unit { get; set; }
        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }
        [Column(TypeName = "money")]
        public decimal? CalcPrice { get; set; }
        [Column(TypeName = "money")]
        public decimal? SeikyuUnchin_D { get; set; }
        [Column(TypeName = "money")]
        public decimal? Tatekaekin_D { get; set; }
        [Column(TypeName = "money")]
        public decimal? Warimashi1_D { get; set; }
        [Column(TypeName = "money")]
        public decimal? Warimashi2_D { get; set; }
        [Column(TypeName = "money")]
        public decimal? Warimashi3_D { get; set; }
        [Column(TypeName = "money")]
        public decimal? Warimashi4_D { get; set; }
        [Column(TypeName = "money")]
        public decimal? Warimashi5_D { get; set; }
        [Column(TypeName = "money")]
        public decimal? SeikyuTotal { get; set; }
        public int? Zei_Kubun_D { get; set; }
        [Column(TypeName = "date")]
        public DateTime? FROM_DATE_D { get; set; }
        [Column(TypeName = "date")]
        public DateTime? TO_DATE_D { get; set; }
    }
}
