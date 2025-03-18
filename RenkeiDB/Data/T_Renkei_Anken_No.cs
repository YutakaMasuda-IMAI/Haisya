using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// 連携案件番号情報を表すエンティティ
    /// </summary>
    [Table("T_Renkei_Anken_No")]
    public partial class T_Renkei_Anken_No
    {
        [Key]
        public int NENDO { get; set; }
        public int NO { get; set; }
    }
}
