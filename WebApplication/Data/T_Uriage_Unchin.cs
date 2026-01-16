using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("T_Uriage_Unchin")]
public partial class T_Uriage_Unchin
{
    [Key]
    public int Uriage_Unchin_ID { get; set; }

    public int Uriage_ID { get; set; }

    public int Sort { get; set; }

    /// <summary>
    /// 0：伝票、1：赤黒伝票
    /// </summary>
    public int Uriage_Kubun { get; set; }

    /// <summary>
    /// 0：追加、1：初期値
    /// </summary>
    public int Default_Kubun { get; set; }

    public DateOnly Seikyu_Date { get; set; }

    public int Shime_Day { get; set; }

    public int Customer_Branch_ID { get; set; }

    [StringLength(20)]
    public string Tsumi { get; set; }

    [StringLength(20)]
    public string Oroshi { get; set; }

    [StringLength(20)]
    public string Luggage { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    public double Qty { get; set; }

    /// <summary>
    /// 単位
    /// </summary>
    public int Unit { get; set; }

    /// <summary>
    /// 単価
    /// </summary>
    [Column(TypeName = "money")]
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// 計算運賃
    /// </summary>
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

    /// <summary>
    /// 0:課税、1:非課税
    /// </summary>
    public int Zei_Kubun { get; set; }

    public bool Del_Flg { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Insert_Datetime { get; set; }

    public int Insert_User { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Update_Datetime { get; set; }

    public int Update_User { get; set; }

    [StringLength(255)]
    public string AkaKuro_Remarks { get; set; }

    public int Syaryo_ID { get; set; }

    [StringLength(30)]
    public string SyasyuDisplay { get; set; }

    [StringLength(10)]
    public string Syaban { get; set; }

    [StringLength(30)]
    public string Remarks1 { get; set; }

    [StringLength(30)]
    public string Remarks2 { get; set; }
}
