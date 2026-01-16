using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("T_Haisya_SyabanRenraku")]
public partial class T_Haisya_SyabanRenraku
{
    [Key]
    public int SyabanRenraku_ID { get; set; }

    public int Company_ID { get; set; }

    public DateOnly PrintDate { get; set; }

    public int Group_ID { get; set; }

    public DateOnly Day { get; set; }

    public int Tantou_ID { get; set; }

    public int Customer_Branch_ID { get; set; }

    [StringLength(50)]
    public string Mail_Address1 { get; set; }

    [StringLength(50)]
    public string Mail_Address2 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Del_Datetime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Insert_Datetime { get; set; }

    public int Insert_User { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Update_Datetime { get; set; }

    public int Update_User { get; set; }
}
