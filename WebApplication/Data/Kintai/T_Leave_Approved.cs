using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Table("T_Leave_Approved")]
    public partial class T_Leave_Approved
    {
        [Key]
        public int Leave_Approved_ID { get; set; }
        public int LeaveApplication_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Approved_DateTime { get; set; }
        public bool? Approved_Flg { get; set; }
        [StringLength(10)]
        public string Approved_Remarks { get; set; }
    }
}
