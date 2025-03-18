using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// 連携案件確保印刷情報を表すエンティティ
    /// </summary>
    [Table("T_Renkei_Anken_Secure_Print")]
    public partial class T_Renkei_Anken_Secure_Print
    {
        [Key]
        public int Renkei_Anken_Secure_ID { get; set; }
        public int Print_Kubun { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Print_Datetime { get; set; }
        public int Print_User { get; set; }
    }
}
