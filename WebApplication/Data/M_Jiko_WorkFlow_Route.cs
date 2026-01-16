using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[PrimaryKey("Jiko_WorkFlow_Base_ID", "Jiko_WorkFlow_Sort")]
[Table("M_Jiko_WorkFlow_Route")]
public partial class M_Jiko_WorkFlow_Route
{
    [Key]
    public int Jiko_WorkFlow_Base_ID { get; set; }

    [Key]
    public int Jiko_WorkFlow_Sort { get; set; }

    [StringLength(50)]
    public string Jiko_WorkFlow_Display { get; set; }

    public int Jiko_WorkFlow_Group_ID { get; set; }
}
