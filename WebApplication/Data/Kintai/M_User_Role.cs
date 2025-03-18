using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Table("M_User_Role")]
    public partial class M_User_Role
    {
        [Key]
        public int Role { get; set; }
        [StringLength(50)]
        public string RoleName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UP_DATE { get; set; }
        public bool Del_Flg { get; set; }
    }
}
