using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("T_Uriage_Futan")]
public partial class T_Uriage_Futan
{
    [Key]
    public int Uriage_Futan_ID { get; set; }

    public int Uriage_ID { get; set; }

    public int Sort { get; set; }

    public int Default_Kubun { get; set; }

    /// <summary>
    /// 高速代、フェリー等の区分
    /// </summary>
    public int futan_Kubun { get; set; }

    [Column(TypeName = "money")]
    public decimal FutanPrice { get; set; }

    public bool Del_Flg { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Insert_Datetime { get; set; }

    public int Insert_User { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Update_Datetime { get; set; }

    public int Update_User { get; set; }
}
