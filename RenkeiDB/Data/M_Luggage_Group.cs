using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// 荷物グループ情報を表すエンティティ
    /// </summary>
    [Table("M_Luggage_Group")]
    public partial class M_Luggage_Group
    {
        [Key]
        public int Luggage_Group_ID { get; set; }
        public int Company_ID { get; set; }
        public int SortOrder { get; set; }
        [Required]
        [StringLength(50)]
        public string Luggage_GroupName { get; set; }
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
