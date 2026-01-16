using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("M_Report_Serch_Item")]
public partial class M_Report_Serch_Item
{
    [Key]
    public int Report_Serch_Item_ID { get; set; }

    public int Report_Serch_Kubun_ID { get; set; }

    public int Sort_Order { get; set; }

    public int Row_Order { get; set; }

    [StringLength(50)]
    public string Display_Title { get; set; }

    [StringLength(50)]
    public string Display_Message { get; set; }

    public int SelectBtn { get; set; }

    [StringLength(50)]
    public string SelectBtnOnClick { get; set; }

    public int Inputbox_Enabled { get; set; }

    public int Calender_Flg { get; set; }

    public double Inputbox_Width { get; set; }

    public int MaxLength { get; set; }

    [StringLength(20)]
    public string Inputbox_Type { get; set; }

    [StringLength(20)]
    public string Inputbox_Formart { get; set; }

    [StringLength(20)]
    public string Model_Prooerty { get; set; }

    [StringLength(10)]
    public string Between_Display { get; set; }

    public int NotNull_Flg { get; set; }

    [StringLength(50)]
    public string Default_Val { get; set; }
}
