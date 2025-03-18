using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// 連携案件確保チェック情報を表すエンティティ
    /// </summary>
    [Table("T_Renkei_Anken_Secure_Check")]
    public partial class T_Renkei_Anken_Secure_Check
    {
        [Key]
        public int Renkei_Anken_Secure_ID { get; set; }
        public int Check_Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        public int Insert_User { get; set; }
    }
}
