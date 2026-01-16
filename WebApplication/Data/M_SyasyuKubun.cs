using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("M_SyasyuKubun")]
[Index("SIZE", "Kata_ID", "KUBUN", Name = "IX_M_SyasyuKubun", IsUnique = true)]
public partial class M_SyasyuKubun
{
    [Key]
    public int SyasyuKubun_ID { get; set; }

    public int Company_ID { get; set; }

    [Required]
    [StringLength(20)]
    public string SIZE { get; set; }

    [StringLength(10)]
    public string Kata_ID { get; set; }

    [Required]
    [StringLength(20)]
    public string KUBUN { get; set; }

    public int SortOrder { get; set; }

    [StringLength(255)]
    public string Remarks { get; set; }

    public bool Del_Flg { get; set; }
}
