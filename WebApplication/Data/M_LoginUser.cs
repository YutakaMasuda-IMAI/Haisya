using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("M_LoginUser")]
    public partial class M_LoginUser
    {
        [Key]
        public int LoginUser_ID { get; set; }
        public int User_ID { get; set; }
        [Required]
        [StringLength(20)]
        public string LoginID { get; set; }
        [Required]
        [StringLength(10)]
        public string Password { get; set; }
        public bool Lock_Flg { get; set; }
        public int Role { get; set; }
        [StringLength(20)]
        public string DefaultArea { get; set; }
        [StringLength(20)]
        public string DefaultSyasyu { get; set; }
        [StringLength(20)]
        public string DefaultKata { get; set; }
        public int DefaultGroup { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UP_DATE { get; set; }
        public bool Del_Flg { get; set; }
        public bool HaisyaWeb_License { get; set; }
        public bool SeikyuWeb_License { get; set; }
        public bool RenkeiWeb_License { get; set; }
    }
}
