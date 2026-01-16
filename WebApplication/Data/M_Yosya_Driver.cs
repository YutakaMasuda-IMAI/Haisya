using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Keyless]
public partial class M_Yosya_Driver
{
    public int Yosya_Driver_ID { get; set; }

    public int Yosya_Branch_ID { get; set; }

    public int Yosya_ID { get; set; }

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
