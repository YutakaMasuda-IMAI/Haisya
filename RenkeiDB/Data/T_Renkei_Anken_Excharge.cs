using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// 連携案件追加料金情報を表すエンティティ
    /// </summary>
    [Table("T_Renkei_Anken_Excharge")]
    public partial class T_Renkei_Anken_Excharge
    {
        [Key]
        public int Renkei_Anken_ID { get; set; }
        [Key]
        public int Renkei_Anken_Order { get; set; }
        [Key]
        public int Komoku_ID { get; set; }
        [Column(TypeName = "money")]
        public decimal StdExcharge { get; set; }
        [Column(TypeName = "money")]
        public decimal GrossExcharge { get; set; }
        [Column(TypeName = "money")]
        public decimal Excharge { get; set; }
    }
}
