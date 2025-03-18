using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// 会社情報を表すエンティティ
    /// </summary>
    [Table("M_Company")]
    public partial class M_Company
    {
        [Key]
        public int Renkei_Company_ID { get; set; }
        [StringLength(50)]
        public string Company_Code { get; set; }
        [Required]
        [StringLength(50)]
        public string Company_Name { get; set; }
        [StringLength(50)]
        public string Company_Name_Display { get; set; }
        public int Owner_Flg { get; set; }
        [StringLength(3)]
        public string Post1 { get; set; }
        [StringLength(4)]
        public string Post2 { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(30)]
        public string Phone { get; set; }
        [StringLength(30)]
        public string Fax { get; set; }
        public bool Del_Flg { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        public int Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Update_Datetime { get; set; }
        public int Update_User { get; set; }
    }
}
