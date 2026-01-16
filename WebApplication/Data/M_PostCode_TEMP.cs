using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Keyless]
[Table("M_PostCode_TEMP")]
public partial class M_PostCode_TEMP
{
    [StringLength(50)]
    [Unicode(false)]
    public string PUBLIC_SECTOR_CODE { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string POSTAL_CODE_OLD { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string POSTAL_CODE { get; set; }

    public string KEN_KANA { get; set; }

    public string SHI_KU_CHO_KANA { get; set; }

    public string CHO_IKI_KANA { get; set; }

    public string KEN { get; set; }

    public string SHI_KU_CHO { get; set; }

    public string CHO_IKI { get; set; }

    public int? CHO_IKI_MULT_FLG { get; set; }

    public int? KOAZA_FLG { get; set; }

    public int? CHOME_FLG { get; set; }

    public int? MULTI_FLG { get; set; }

    public int? UPDATE_FLG { get; set; }

    public int? CHANGE_KUBUN { get; set; }

    public int ID { get; set; }
}
