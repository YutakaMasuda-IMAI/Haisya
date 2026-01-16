using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[PrimaryKey("Bak_Uriage_Month_ID", "Data_Kubun", "Data_Sort")]
[Table("B_Uriage_Month_Detail")]
public partial class B_Uriage_Month_Detail
{
    [Key]
    public int Bak_Uriage_Month_ID { get; set; }

    /// <summary>
    /// 1：ヘッダー、2：入金、３：案件明細
    /// </summary>
    [Key]
    public int Data_Kubun { get; set; }

    [Key]
    public int Data_Sort { get; set; }

    public int Uriage_Unchin_ID { get; set; }

    public int Anken_ID { get; set; }

    public int Anken_ID_Detail { get; set; }

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

    public double Qty { get; set; }

    public int Unit { get; set; }

    [Column(TypeName = "money")]
    public decimal UnitPrice { get; set; }

    [Column(TypeName = "money")]
    public decimal CalcPrice { get; set; }

    [Column(TypeName = "money")]
    public decimal SeikyuUnchin { get; set; }

    [Column(TypeName = "money")]
    public decimal Tatekaekin { get; set; }

    [Column(TypeName = "money")]
    public decimal Warimashi1 { get; set; }

    [Column(TypeName = "money")]
    public decimal Warimashi2 { get; set; }

    [Column(TypeName = "money")]
    public decimal Warimashi3 { get; set; }

    [Column(TypeName = "money")]
    public decimal Warimashi4 { get; set; }

    [Column(TypeName = "money")]
    public decimal Warimashi5 { get; set; }

    [Column(TypeName = "money")]
    public decimal SeikyuTotal { get; set; }

    public int? Zei_Kubun { get; set; }

    public int YosyaDriver_ID { get; set; }

    public int Yosya_Branch_ID { get; set; }

    [Required]
    [StringLength(50)]
    public string Yosya_Name { get; set; }

    [Required]
    [StringLength(50)]
    public string Yosya_Driver_Name { get; set; }

    public int? Remarks_ID { get; set; }

    [StringLength(20)]
    public string Remaks { get; set; }

    public DateOnly? FROM_DATE { get; set; }

    public DateOnly? TO_DATE { get; set; }
}
