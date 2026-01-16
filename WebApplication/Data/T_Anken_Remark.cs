using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[PrimaryKey("Anken_ID", "Anken_Order")]
[Table("T_Anken_Remark")]
public partial class T_Anken_Remark
{
    [Key]
    public int Anken_ID { get; set; }

    [Key]
    public int Anken_Order { get; set; }

    [StringLength(255)]
    public string Remarks { get; set; }
}
