using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("T_Print_Seikyu")]
[Index("Customer_Branch_ID", "Shime_Day", "Check_Seikyu_ID", "Seikyu_ID", "Del_Datetime", Name = "IX_T_Print_Seikyu", IsUnique = true)]
public partial class T_Print_Seikyu
{
    [Key]
    public int Print_Seikyu_ID { get; set; }

    public int Customer_Branch_ID { get; set; }

    public int Check_Seikyu_ID { get; set; }

    public int Seikyu_ID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Del_Datetime { get; set; }

    /// <summary>
    /// M_Code:11
    /// </summary>
    public int Print_Pattern { get; set; }

    public DateOnly Seikyu_Month { get; set; }

    public int Shime_Day { get; set; }

    /// <summary>
    /// 0:課税、1:非課税
    /// </summary>
    public int Zei_Kubun { get; set; }

    [StringLength(50)]
    public string Mail_Title { get; set; }

    [StringLength(255)]
    public string Mail_Detail { get; set; }

    [StringLength(40)]
    public string Customer_Name { get; set; }

    [StringLength(16)]
    public string Customer_Name_Kana { get; set; }

    [StringLength(50)]
    public string Mail_Address1 { get; set; }

    [StringLength(50)]
    public string Mail_Address2 { get; set; }

    [StringLength(8)]
    public string PostCode { get; set; }

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
    public decimal? SeikyuUnchin { get; set; }

    [Column(TypeName = "money")]
    public decimal? SeikyuUnchin_NoTax { get; set; }

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

    public DateOnly? FROM_DATE { get; set; }

    public DateOnly? TO_DATE { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UP_DATE { get; set; }

    public DateOnly? SEIKYUDATE_TO { get; set; }

    /// <summary>
    /// ０：切り捨て、１：四捨五入、２：切り上げ
    /// </summary>
    public int Tax_Fraction_Kubun { get; set; }

    public double Tax_Fraction_Position { get; set; }
}
