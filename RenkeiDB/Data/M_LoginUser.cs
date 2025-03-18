using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// ログインユーザー情報を表すエンティティ
    /// </summary>
    [Table("M_LoginUser")]
    public partial class M_LoginUser
    {
        [Key]
        public int LoginUser_ID { get; set; }
        public int User_ID { get; set; }
        [Required]
        [StringLength(10)]
        public string LoginID { get; set; }
        [Required]
        [StringLength(10)]
        public string Password { get; set; }
        public bool Lock_Flg { get; set; }
        public int Role { get; set; }
        [StringLength(20)]
        public string DefaultEria { get; set; }
        [StringLength(20)]
        public string DefaultSyasyu { get; set; }
        [StringLength(20)]
        public string DefaultKata { get; set; }
        public int DefaultGroup { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UP_DATE { get; set; }
        public bool Del_Flg { get; set; }
    }
}
