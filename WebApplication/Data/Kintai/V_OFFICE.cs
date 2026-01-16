using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Kintai;

[Keyless]
public partial class V_OFFICE
{
    [StringLength(30)]
    public string OFFICE { get; set; }
}
