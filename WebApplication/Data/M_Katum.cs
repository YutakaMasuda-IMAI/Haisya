using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

public partial class M_Katum
{
    [Key]
    [StringLength(10)]
    public string Kata_ID { get; set; }

    public int Company_ID { get; set; }

    public int SortOrder { get; set; }

    [StringLength(50)]
    public string Kata_Display { get; set; }

    [StringLength(255)]
    public string Remarks { get; set; }

    public bool Del_Flg { get; set; }
}
