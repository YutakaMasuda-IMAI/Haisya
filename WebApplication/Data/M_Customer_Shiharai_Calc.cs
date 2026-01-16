using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("M_Customer_Shiharai_Calc")]
public partial class M_Customer_Shiharai_Calc
{
    [Key]
    public int Customer_Branch_ID { get; set; }

    [StringLength(10)]
    public string ShiharaiUnchin_Name { get; set; }

    [StringLength(10)]
    public string Tatekaekin_Name { get; set; }

    [StringLength(10)]
    public string Warimashi1_Name { get; set; }

    [StringLength(10)]
    public string Warimashi2_Name { get; set; }

    [StringLength(10)]
    public string Warimashi3_Name { get; set; }

    [StringLength(10)]
    public string Warimashi4_Name { get; set; }

    [StringLength(10)]
    public string Warimashi5_Name { get; set; }

    [StringLength(10)]
    public string ShiharaiTotal_Name { get; set; }

    public bool? Warimashi1_Visible { get; set; }

    public bool? Warimashi2_Visible { get; set; }

    public bool? Warimashi3_Visible { get; set; }

    public bool? Warimashi4_Visible { get; set; }

    public bool? Warimashi5_Visible { get; set; }

    [StringLength(50)]
    public string Warimashi1_Calc { get; set; }

    [StringLength(50)]
    public string Warimashi2_Calc { get; set; }

    [StringLength(50)]
    public string Warimashi3_Calc { get; set; }

    [StringLength(50)]
    public string Warimashi4_Calc { get; set; }

    [StringLength(50)]
    public string Warimashi5_Calc { get; set; }

    [StringLength(50)]
    public string ShiharaiTotal_Calc { get; set; }
}
