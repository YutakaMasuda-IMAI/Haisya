using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("T_Jiko")]
public partial class T_Jiko
{
    [Key]
    public int Jiko_ID { get; set; }

    [Required]
    [StringLength(20)]
    public string Jiko_No { get; set; }

    public int Company_ID { get; set; }

    public int Branch_ID { get; set; }

    public int Driver_ID { get; set; }

    public int Haisya_Group_ID { get; set; }

    /// <summary>
    /// M_Code:18
    /// </summary>
    public int Jiko_Kubun { get; set; }

    public int Jiko_Status { get; set; }

    public int Jiko_WorkFlow_Base_ID { get; set; }

    public int Jiko_WorkFlow_Status { get; set; }

    [Required]
    [StringLength(50)]
    public string Jiko_Display { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Jiko_Date { get; set; }

    [Column(TypeName = "money")]
    public decimal Jiko_Futan_Money { get; set; }

    public int SyaryoManagement_ID { get; set; }

    public int SyaryoManagement_ID1 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Insert_Datetime { get; set; }

    public int Insert_User { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Update_Datetime { get; set; }

    public int Update_User { get; set; }
}
