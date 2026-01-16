using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Kintai;

[Keyless]
public partial class Q_F_事業所名
{
    [StringLength(30)]
    public string 事業所名 { get; set; }

    public long? SORT { get; set; }
}
