using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("T_Nippou_Toll_Other")]
public partial class T_Nippou_Toll_Other
{
    [Key]
    public int Nippou_Toll_Other_ID { get; set; }

    public int Nippou_ID { get; set; }

    public int Sort { get; set; }

    public int DriverSyaryo_ID { get; set; }

    public int Driver_ID { get; set; }

    public int YosyaDriverSyaryo_ID { get; set; }

    public int YosyaDriver_ID { get; set; }

    public int Yosya_Branch_ID { get; set; }

    public DateOnly Day { get; set; }

    public int Toll_Kubun { get; set; }

    [Required]
    [StringLength(50)]
    public string Start_Name { get; set; }

    [StringLength(50)]
    public string End_Name { get; set; }

    [Column(TypeName = "money")]
    public decimal Toll_Fee { get; set; }

    public double? Distance { get; set; }

    public int Futan_Kubun { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Insert_Datetime { get; set; }

    public int? Insert_User { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Update_Datetime { get; set; }

    public int? Update_User { get; set; }
}
