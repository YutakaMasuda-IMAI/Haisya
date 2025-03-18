using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Table("M_Leave_Setting")]
    public partial class M_Leave_Setting
    {
        [Key]
        public int Leave_Setting_CD { get; set; }
        [Required]
        [StringLength(50)]
        public string Leave_Setting_Name { get; set; }
        [StringLength(50)]
        public string Leave_Setting_SubName { get; set; }
        [Required]
        [StringLength(50)]
        public string Val_Kubun { get; set; }
        public int Val_Int { get; set; }
        public bool Val_Flg { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Val_Date { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Val_Datetime { get; set; }
    }
}
