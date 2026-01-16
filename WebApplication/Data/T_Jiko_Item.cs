using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[PrimaryKey("Jiko_ID", "Jiko_Items_ID")]
public partial class T_Jiko_Item
{
    [Key]
    public int Jiko_ID { get; set; }

    [Key]
    public int Jiko_Items_ID { get; set; }

    [StringLength(255)]
    public string Jiko_Items_Val_String { get; set; }

    public int? Jiko_Items_Val_Int { get; set; }

    public double? Jiko_Items_Val_Double { get; set; }

    [Column(TypeName = "money")]
    public decimal? Jiko_Items_Val_Money { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Jiko_Items_Val_Datetime { get; set; }
}
