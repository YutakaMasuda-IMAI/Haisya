using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Kintai;

[Table("M_Holiday")]
public partial class M_Holiday
{
    [Key]
    public DateOnly Day { get; set; }

    [StringLength(10)]
    public string WeekDay { get; set; }

    [StringLength(50)]
    public string HolidayName { get; set; }
}
