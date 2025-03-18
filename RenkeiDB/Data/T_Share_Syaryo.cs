using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// 共有車両情報を表すエンティティ
    /// </summary>
    [Table("T_Share_Syaryo")]
    public partial class T_Share_Syaryo
    {
        [Key]
        public int Share_Syaryo_ID { get; set; }
        [Required]
        [StringLength(20)]
        public string Share_Syaryo_No { get; set; }
        public int Share_Syaryo_Status { get; set; }
        public int Share_Syaryo_Latest_Order { get; set; }
        public int Company_ID { get; set; }
        public int Branch_ID { get; set; }
        public int Tantou_Group_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Cancel_Datetime { get; set; }
    }
}
