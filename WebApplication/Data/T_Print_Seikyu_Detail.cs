using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[PrimaryKey("Print_Seikyu_ID", "Data_Kubun", "Data_Sort")]
[Table("T_Print_Seikyu_Detail")]
public partial class T_Print_Seikyu_Detail
{
    [Key]
    public int Print_Seikyu_ID { get; set; }

    [Key]
    public int Data_Kubun { get; set; }

    [Key]
    public int Data_Sort { get; set; }

    public int Seikyu_ID { get; set; }

    public int? Uriage_Unchin_ID { get; set; }

    public int? Nyukin_ID { get; set; }

    public int? Anken_ID { get; set; }

    public int? Anken_ID_Detail { get; set; }

    [Column(TypeName = "date")]
    public DateOnly? Display_Date { get; set; }

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

    public int? Zei_Kubun { get; set; }

    [StringLength(30)]
    public string Remarks1 { get; set; }

    [StringLength(30)]
    public string Remarks2 { get; set; }

    public DateOnly? FROM_DATE { get; set; }

    public DateOnly? TO_DATE { get; set; }
}
