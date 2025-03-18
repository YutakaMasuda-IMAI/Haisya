using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// コード情報を表すエンティティ
    /// </summary>
    [Table("M_Code")]
    public partial class M_Code
    {
        [Key]
        public int Code_ID { get; set; }
        public int Company_ID { get; set; }
        [StringLength(50)]
        public string Code_Name { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
        public bool Del_Flg { get; set; }
    }
}
