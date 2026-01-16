using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[PrimaryKey("Jiko_ID", "Jiko_Type_ID")]
[Table("T_Jiko_Type")]
public partial class T_Jiko_Type
{
    [Key]
    public int Jiko_ID { get; set; }

    /// <summary>
    /// M_Code:17
    /// </summary>
    [Key]
    public int Jiko_Type_ID { get; set; }

    [StringLength(255)]
    public string Jiko_Type_Remarks { get; set; }
}
