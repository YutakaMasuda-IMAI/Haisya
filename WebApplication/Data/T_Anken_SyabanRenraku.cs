using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("T_Anken_SyabanRenraku")]
public partial class T_Anken_SyabanRenraku
{
    [Key]
    public int Anken_ID { get; set; }

    public int Renraku_Kubun { get; set; }

    [StringLength(50)]
    public string Remarks { get; set; }
}
