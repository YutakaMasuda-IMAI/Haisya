using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// 連携案件装備品情報を表すエンティティ
    /// </summary>
    [Table("T_Renkei_Anken_Equipment")]
    public partial class T_Renkei_Anken_Equipment
    {
        [Key]
        public int Renkei_Anken_ID { get; set; }
        [Key]
        public int Renkei_Anken_Order { get; set; }
        [Key]
        public int Equipment_ID { get; set; }
        public double? Equipment_Count { get; set; }
        [StringLength(50)]
        public string Remarks { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Insert_Datetime { get; set; }
        public int? Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Update_Datetime { get; set; }
        public int? Update_User { get; set; }
    }
}
