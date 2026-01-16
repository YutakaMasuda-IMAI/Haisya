using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("T_Admin_Info")]
public partial class T_Admin_Info
{
    [Key]
    public int AdminInfo_ID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Info_Datetime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Info_End_Datetime { get; set; }

    /// <summary>
    /// 0:お知らせ、1:重要、2:緊急、3:未定
    /// </summary>
    public int Info_Kubun { get; set; }

    [Required]
    [StringLength(100)]
    public string Info_Title { get; set; }

    [Required]
    public string Info_Detail { get; set; }
}
