using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("T_Print_Parameter")]
public partial class T_Print_Parameter
{
    [Key]
    public int Print_ID { get; set; }

    [Required]
    [StringLength(10)]
    public string Tokun { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Limit_Date { get; set; }

    /// <summary>
    /// 2：車番連絡、10：請求問合せ、11：下払問合せ　　M_Codeの10
    /// </summary>
    public int Print_Kubun { get; set; }

    public int Data_ID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Del_Datetime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Insert_Datetime { get; set; }

    public int Insert_User { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Update_Datetime { get; set; }

    public int Update_User { get; set; }
}
