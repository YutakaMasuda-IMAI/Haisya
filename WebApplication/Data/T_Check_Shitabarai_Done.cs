using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("T_Check_Shitabarai_Done")]
public partial class T_Check_Shitabarai_Done
{
    [Key]
    public int Check_Shitabarai_ID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Check_Datetime { get; set; }

    public int Check_User { get; set; }

    public int Check_Reault { get; set; }

    /// <summary>
    /// 0：金額変更無し、1：金額変更あり
    /// </summary>
    public int? Change_Flg { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Insert_Datetime { get; set; }

    public int Insert_User { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Update_Datetime { get; set; }

    public int Update_User { get; set; }
}
