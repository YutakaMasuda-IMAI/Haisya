using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("M_Report_Output_Item_Master")]
public partial class M_Report_Output_Item_Master
{
    [Key]
    public int Report_Output_Item_ID { get; set; }

    public int Report_Serch_Kubun_ID { get; set; }

    public int Sort_Order { get; set; }

    public int Row_Order { get; set; }

    [StringLength(50)]
    public string Display_Title { get; set; }

    [StringLength(20)]
    public string Display_Type { get; set; }

    [StringLength(20)]
    public string Display_Format { get; set; }

    [StringLength(20)]
    public string Model_Prooerty { get; set; }

    public double? Display_Width { get; set; }

    public int Report_ItemFlg { get; set; }

    [StringLength(20)]
    public string Page_Fotter { get; set; }

    [StringLength(20)]
    public string Report_Fotter { get; set; }
}
