using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("M_CompanyBranch")]
    [Index(nameof(Company_ID), nameof(Branch_Name), Name = "IX_M_CompanyBranch", IsUnique = true)]
    public partial class M_CompanyBranch
    {
        [Key]
        public int Branch_ID { get; set; }
        [Key]
        public int Company_ID { get; set; }
        public int SortOrder { get; set; }
        public int Oya_Branch_ID { get; set; }
        [StringLength(10)]
        public string Branch_Code { get; set; }
        [Required]
        [StringLength(50)]
        public string Branch_Name { get; set; }
        [StringLength(50)]
        public string Branch_Name_Abbr { get; set; }
        [StringLength(3)]
        public string Branch_Post1 { get; set; }
        [StringLength(4)]
        public string Branch_Post2 { get; set; }
        [StringLength(100)]
        public string Branch_Address { get; set; }
        [StringLength(30)]
        public string Branch_Phone { get; set; }
        [StringLength(30)]
        public string Branch_Fax { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UP_DATE { get; set; }
        public bool Del_Flg { get; set; }
    }
}
