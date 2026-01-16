using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("M_CompanyOrganization")]
public partial class M_CompanyOrganization
{
    [Key]
    public int CompanyOrganization_ID { get; set; }

    public int Company_ID { get; set; }

    public int CompanyOrganization_ID_Oya { get; set; }

    [StringLength(10)]
    public string Organization_Code { get; set; }

    [Required]
    [StringLength(50)]
    public string Organization_Name { get; set; }

    [Required]
    [StringLength(50)]
    public string Organization_Name_Abbr { get; set; }

    [StringLength(255)]
    public string Remarks { get; set; }

    public bool Del_Flg { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UP_DATE { get; set; }
}
