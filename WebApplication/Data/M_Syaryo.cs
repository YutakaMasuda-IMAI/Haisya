using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("M_Syaryo")]
[Index("KATA", "KataDisplay", Name = "IX_M_Syaryo")]
[Index("Company_ID", "SYASYU", "KATA", Name = "IX_M_Syaryo_1", IsUnique = true)]
public partial class M_Syaryo
{
    [Key]
    public int Syaryo_ID { get; set; }

    public int Company_ID { get; set; }

    [Required]
    [StringLength(20)]
    public string SYASYU { get; set; }

    [Required]
    [StringLength(20)]
    public string KATA { get; set; }

    public int SortOrder { get; set; }

    [Required]
    [StringLength(30)]
    public string SyasyuDisplay { get; set; }

    [Required]
    [StringLength(20)]
    public string KataDisplay { get; set; }

    public double? LONG { get; set; }

    public double? WIDTH { get; set; }

    public double? HEIGHT { get; set; }

    public double? MAX_LOAD_CAPA { get; set; }

    public double? CAR_WEIGHT { get; set; }

    public double? CAR_GROSS_WEIGHT { get; set; }

    public double? AVG_FUEL_COSTS { get; set; }

    [Required]
    [StringLength(20)]
    public string SIZE { get; set; }

    [Required]
    [StringLength(10)]
    public string Kata_ID { get; set; }

    public int? SyasyuKubun_ID { get; set; }

    [StringLength(20)]
    public string TOLL_TYPE { get; set; }

    [StringLength(10)]
    public string RegulationType { get; set; }

    [StringLength(1)]
    public string CARDETAILINFO { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UP_DATE { get; set; }

    public bool HIDDEN_FLG { get; set; }

    public bool DEL_FLG { get; set; }
}
