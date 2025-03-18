using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("M_Equipment_Group")]
    public partial class M_Equipment_Group
    {
        [Key]
        public int Equipment_Group_ID { get; set; }
        public int Company_ID { get; set; }
        public int SortOrder { get; set; }
        [Required]
        [StringLength(50)]
        public string Equipment_GroupName { get; set; }
        [StringLength(50)]
        public string Remarks { get; set; }
        public bool Del_Flg { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Insert_Datetime { get; set; }
        public int? Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Update_Datetime { get; set; }
        public int? Update_User { get; set; }
    }
}
