using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// 会社ユーザー情報を表すエンティティ
    /// </summary>
    [Table("M_CompanyUser")]
    public partial class M_CompanyUser
    {
        [Key]
        public int User_ID { get; set; }
        public int Branch_ID { get; set; }
        public int Company_ID { get; set; }
        public int? Employee_Number { get; set; }
        [Required]
        [StringLength(50)]
        public string Last_Name { get; set; }
        [StringLength(50)]
        public string First_Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Display_Name { get; set; }
        public int Haisya_Send_Kubun { get; set; }
        public bool Del_Flg { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        public int Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Update_Datetime { get; set; }
        public int Update_User { get; set; }
    }
}
