using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("M_LoginUser_Role")]
    public partial class M_LoginUser_Role
    {
        [Key]
        public int Company_ID { get; set; }
        [Key]
        public int Role { get; set; }
        [StringLength(50)]
        public string RoleName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UP_DATE { get; set; }
        public bool Del_Flg { get; set; }
    }
}
