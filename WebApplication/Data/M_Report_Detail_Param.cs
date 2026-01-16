using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[PrimaryKey("Report_Serch_Kubun_ID", "Sort_Order")]
[Table("M_Report_Detail_Param")]
public partial class M_Report_Detail_Param
{
    [Key]
    public int Report_Serch_Kubun_ID { get; set; }

    [Key]
    public int Sort_Order { get; set; }

    [Required]
    [StringLength(50)]
    public string Param_Name { get; set; }

    [StringLength(20)]
    public string Param_Type { get; set; }

    [StringLength(50)]
    public string Param_Val { get; set; }

    [StringLength(20)]
    public string Param_Format { get; set; }

    [StringLength(50)]
    public string Param_Default { get; set; }

    [StringLength(20)]
    public string Param_Model_Prooerty { get; set; }
}
