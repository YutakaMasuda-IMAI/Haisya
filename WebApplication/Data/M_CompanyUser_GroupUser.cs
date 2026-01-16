using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[PrimaryKey("Group_ID", "User_ID")]
[Table("M_CompanyUser_GroupUser")]
public partial class M_CompanyUser_GroupUser
{
    [Key]
    public int Group_ID { get; set; }

    [Key]
    public int User_ID { get; set; }

    public bool Default_Flg { get; set; }

    public bool Del_Flg { get; set; }
}
