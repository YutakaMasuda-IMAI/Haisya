using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Keyless]
    public partial class V_LoginUser
    {
        public int LoginUser_ID { get; set; }
        public int User_ID { get; set; }
        [Required]
        [StringLength(10)]
        public string LoginID { get; set; }
        [Required]
        [StringLength(10)]
        public string Password { get; set; }
        public bool Lock_Flg { get; set; }
        public bool Del_Flg { get; set; }
        public int Role { get; set; }
        [StringLength(50)]
        public string RoleName { get; set; }
        public int? Employee_Number { get; set; }
        [Required]
        [StringLength(50)]
        public string User_Name { get; set; }
        public int Company_ID { get; set; }
        public int Branch_ID { get; set; }
        public int Tntou_ID { get; set; }
        [StringLength(50)]
        public string Company_Code { get; set; }
        [Required]
        [StringLength(50)]
        public string Company_Name { get; set; }
        [StringLength(50)]
        public string Company_Name_Abbr { get; set; }
        [StringLength(10)]
        public string Branch_Code { get; set; }
        [Required]
        [StringLength(50)]
        public string Branch_Name { get; set; }
        [StringLength(50)]
        public string Branch_Name_Abbr { get; set; }
        public int? Area_ID { get; set; }
        [StringLength(20)]
        public string DefaultArea { get; set; }
        [StringLength(20)]
        public string DefaultSize { get; set; }
        [StringLength(20)]
        public string DefaultSyasyu { get; set; }
        [StringLength(20)]
        public string DefaultKata { get; set; }
        public int DefaultGroup { get; set; }
        [StringLength(30)]
        public string SyasyuDisplay { get; set; }
        [StringLength(20)]
        public string KataDisplay { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UP_DATE { get; set; }
        public bool CompanyUser_Del_Flg { get; set; }
    }
}
