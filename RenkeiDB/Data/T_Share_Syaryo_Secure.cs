using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// 共有車両確保情報を表すエンティティ
    /// </summary>
    [Table("T_Share_Syaryo_Secure")]
    public partial class T_Share_Syaryo_Secure
    {
        [Key]
        public int Share_Syaryo_Secure_ID { get; set; }
        public int Share_Syaryo_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Cancel_Datetime { get; set; }
        public int Company_ID { get; set; }
        public int Branch_ID { get; set; }
        public int Tantou_Group_ID { get; set; }
        public int Renkei_Anken_ID { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        public int Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Update_Datetime { get; set; }
        public int Update_User { get; set; }
    }
}
