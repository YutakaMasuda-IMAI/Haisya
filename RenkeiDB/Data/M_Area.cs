using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// エリア情報を表すエンティティ
    /// </summary>
    [Table("M_Area")]
    public partial class M_Area
    {
        [Key]
        public int Area_ID { get; set; }
        [Key]
        public int Company_ID { get; set; }
        [Required]
        [StringLength(10)]
        public string Area { get; set; }
        public int Sort_Order { get; set; }
    }
}
