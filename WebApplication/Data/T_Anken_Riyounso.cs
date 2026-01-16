using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[PrimaryKey("Anken_ID", "Anken_Order")]
[Table("T_Anken_Riyounso")]
public partial class T_Anken_Riyounso
{
    [Key]
    public int Anken_ID { get; set; }

    [Key]
    public int Anken_Order { get; set; }

    public DateOnly Target_Date { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Insert_Datetime { get; set; }

    public int? Insert_User { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Update_Datetime { get; set; }

    public int? Update_User { get; set; }

    public int? TantouID { get; set; }

    public int? EigyoID { get; set; }

    public int? KokyakuId { get; set; }

    [StringLength(50)]
    public string KokyakuCode { get; set; }

    [StringLength(50)]
    public string KokyakuName { get; set; }

    public int? KokyakuTantouId { get; set; }

    [StringLength(50)]
    public string KokyakuTantouName { get; set; }

    [StringLength(50)]
    public string KokyakuTantouPhone { get; set; }

    public int Syaryo_ID { get; set; }

    [StringLength(20)]
    public string Syasyu { get; set; }

    [StringLength(20)]
    public string SyasyuSize { get; set; }

    [StringLength(50)]
    public string SyasyuDisplay { get; set; }

    [StringLength(20)]
    public string Kata { get; set; }

    public int? Daisuu { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string Root_Ferry { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string Root_Regulation { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string Root_Twouturn { get; set; }

    public int? YosyaDriverID { get; set; }

    public int Yosya_Branch_ID { get; set; }

    public int YosyaDriver_ID { get; set; }

    public int YosyaDriverSyaryo_ID { get; set; }

    [StringLength(50)]
    public string Luggage { get; set; }

    public int? SeikyuKubun { get; set; }

    [Column(TypeName = "money")]
    public decimal? GrossAmount { get; set; }

    [Column(TypeName = "money")]
    public decimal? Toll { get; set; }

    [Column(TypeName = "money")]
    public decimal? PaymentAmount { get; set; }

    [Column(TypeName = "money")]
    public decimal? AdvancesPaid { get; set; }

    [StringLength(255)]
    public string Remarks { get; set; }
}
