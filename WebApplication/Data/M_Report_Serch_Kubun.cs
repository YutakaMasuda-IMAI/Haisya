using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("M_Report_Serch_Kubun")]
public partial class M_Report_Serch_Kubun
{
    [Key]
    public int Report_Serch_Kubun_ID { get; set; }

    public int Report_Serch_ID { get; set; }

    public int Sort_Order { get; set; }

    [Required]
    [StringLength(50)]
    public string Display_Title { get; set; }

    [StringLength(50)]
    public string Report_Html { get; set; }

    [StringLength(50)]
    public string Proc_Name { get; set; }

    [StringLength(50)]
    public string Class_Name { get; set; }

    [StringLength(255)]
    public string Data_Sort { get; set; }
}
