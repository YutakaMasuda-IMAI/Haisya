using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[PrimaryKey("Customer_Branch_ID", "Seikyu_Type", "Sort")]
[Table("M_Customer_TollSeikyuKubun")]
public partial class M_Customer_TollSeikyuKubun
{
    [Key]
    public int Customer_Branch_ID { get; set; }

    /// <summary>
    /// 1:距離、2:住所、3:IC
    /// </summary>
    [Key]
    public int Seikyu_Type { get; set; }

    [Key]
    public int Sort { get; set; }

    public double Distance1 { get; set; }

    public double Distance2 { get; set; }

    [StringLength(50)]
    public string Address1 { get; set; }

    [StringLength(50)]
    public string Address2 { get; set; }

    [StringLength(50)]
    public string IC1 { get; set; }

    [StringLength(50)]
    public string IC2 { get; set; }

    public int? SeikyuKubun { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Insert_Datetime { get; set; }

    public int? Insert_User { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Update_Datetime { get; set; }

    public int? Update_User { get; set; }
}
