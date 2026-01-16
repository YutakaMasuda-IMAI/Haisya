using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Kintai;

[Keyless]
[Table("T_LOCK")]
public partial class T_LOCK
{
    public int? BATCH_LOCK { get; set; }
}
