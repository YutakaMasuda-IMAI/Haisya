using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Kintai;

[Table("T_Kakutei")]
public partial class T_Kakutei
{
    [Key]
    public DateOnly Kakutei_Date { get; set; }

    public int? Kakutei_flg { get; set; }
}
