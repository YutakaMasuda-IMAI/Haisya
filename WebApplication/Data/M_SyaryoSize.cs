using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[PrimaryKey("SIZE", "Company_ID")]
[Table("M_SyaryoSize")]
public partial class M_SyaryoSize
{
    [Key]
    public int Company_ID { get; set; }

    [Key]
    [StringLength(20)]
    public string SIZE { get; set; }

    public int SortOrder { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UP_DATE { get; set; }

    public bool HIDDEN_FLG { get; set; }

    public bool DEL_FLG { get; set; }
}
