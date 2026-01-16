using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[PrimaryKey("Driver_ID", "Date")]
public partial class T_Haisya_Driver_Day_Remark
{
    [Key]
    public int Driver_ID { get; set; }

    [Key]
    public DateOnly Date { get; set; }

    [StringLength(50)]
    public string Remarks { get; set; }
}
