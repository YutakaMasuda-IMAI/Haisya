using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("M_CompanyDriver_Syaryo")]
public partial class M_CompanyDriver_Syaryo
{
    [Key]
    public int DriverSyaryo_ID { get; set; }

    public int Driver_ID { get; set; }

    public int SyaryoManagement_ID { get; set; }

    public int? SyaryoManagement_ID1 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Start_Date { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? End_Date { get; set; }

    public int Group_ID { get; set; }

    public int SyasyuKubun_ID { get; set; }

    public int Company_ID { get; set; }

    [StringLength(255)]
    public string REMARKS { get; set; }

    public bool Del_Flg { get; set; }
}
