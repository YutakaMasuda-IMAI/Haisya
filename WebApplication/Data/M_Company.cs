using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("M_Company")]
public partial class M_Company
{
    [Key]
    public int Company_ID { get; set; }

    [StringLength(50)]
    public string Company_Code { get; set; }

    [Required]
    [StringLength(50)]
    public string Company_Name { get; set; }

    [StringLength(50)]
    public string Company_Name_Abbr { get; set; }

    [StringLength(3)]
    public string Post1 { get; set; }

    [StringLength(4)]
    public string Post2 { get; set; }

    [StringLength(100)]
    public string Address { get; set; }

    [StringLength(30)]
    public string Phone { get; set; }

    [StringLength(30)]
    public string Fax { get; set; }

    public bool Del_Flg { get; set; }

    public bool HaisyaWeb_License { get; set; }

    public bool SeikyuWeb_License { get; set; }

    public bool RenkeiWeb_License { get; set; }
}
