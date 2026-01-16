using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("M_Customer_Driver")]
public partial class M_Customer_Driver
{
    [Key]
    public int Customer_Driver_ID { get; set; }

    public int Customer_ID { get; set; }

    public int Customer_Branch_ID { get; set; }

    /// <summary>
    /// 0:傭車、１:専属庸車
    /// </summary>
    public int Yosya_Kubun { get; set; }

    public int Group_ID { get; set; }

    public int? Employee_Number { get; set; }

    [Required]
    [StringLength(50)]
    public string Last_Name { get; set; }

    [StringLength(50)]
    public string First_Name { get; set; }

    [Required]
    [StringLength(50)]
    public string Display_Name { get; set; }

    [StringLength(13)]
    public string Phone1 { get; set; }

    [StringLength(13)]
    public string Phone2 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime From_Date { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? To_Date { get; set; }

    public bool Del_Flg { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Insert_Datetime { get; set; }

    public int Insert_User { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Update_Datetime { get; set; }

    public int Update_User { get; set; }
}
