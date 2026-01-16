using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("T_Jiko_No")]
public partial class T_Jiko_No
{
    [Key]
    public int NENDO { get; set; }

    public int NO { get; set; }
}
