using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Kintai;

[Table("M_乗務外内容")]
public partial class M_乗務外内容
{
    [Key]
    [StringLength(50)]
    public string 乗務外内容 { get; set; }
}
