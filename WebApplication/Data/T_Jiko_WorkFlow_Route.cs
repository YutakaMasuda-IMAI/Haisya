using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("T_Jiko_WorkFlow_Route")]
public partial class T_Jiko_WorkFlow_Route
{
    [Key]
    public int Jiko_WorkFlow_ID { get; set; }

    public int Jiko_ID { get; set; }

    public int Jiko_WorkFlow_Sort { get; set; }

    [Required]
    [StringLength(50)]
    public string Jiko_WorkFlow_Display { get; set; }

    public int Jiko_WorkFlow_Group_ID { get; set; }
}
