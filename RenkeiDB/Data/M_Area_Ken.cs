using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// エリア県情報を表すエンティティ
    /// </summary>
    [Table("M_Area_Ken")]
    public partial class M_Area_Ken
    {
        /// <summary>
        /// 県名
        /// </summary>
        [Key]
        [StringLength(10)]
        public string Ken { get; set; }
        
        /// <summary>
        /// エリアID
        /// </summary>
        public int Area_ID { get; set; }
        
        /// <summary>
        /// 会社ID
        /// </summary>
        public int Company_ID { get; set; }
    }
}
