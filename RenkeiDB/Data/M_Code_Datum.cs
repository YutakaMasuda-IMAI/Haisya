using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// コードデータ情報を表すエンティティ
    /// </summary>
    public partial class M_Code_Datum
    {
        [Key]
        public int Code_ID { get; set; }
        [Key]
        [StringLength(10)]
        public string Code_Data { get; set; }
        public int SortOrder { get; set; }
        [Required]
        [StringLength(50)]
        public string Code_Name { get; set; }
        [StringLength(50)]
        public string Code_Name_abbr { get; set; }
        [StringLength(200)]
        public string Remarks { get; set; }
        public bool Del_Flg { get; set; }
    }
}
