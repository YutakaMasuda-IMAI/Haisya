using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[PrimaryKey("Anken_ID", "Anken_Order", "Kokyaku_Order")]
[Table("T_Anken_OyaKokyaku")]
public partial class T_Anken_OyaKokyaku
{
    [Key]
    public int Anken_ID { get; set; }

    [Key]
    public int Anken_Order { get; set; }

    [Key]
    public int Kokyaku_Order { get; set; }

    [StringLength(50)]
    public string KomokuTitle { get; set; }

    public int? KokyakuId { get; set; }

    [StringLength(50)]
    public string KokyakuName { get; set; }

    public int? TantouId { get; set; }

    [StringLength(50)]
    public string TantouName { get; set; }

    [StringLength(50)]
    public string TantouPhone { get; set; }
}
