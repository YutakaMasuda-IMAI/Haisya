using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Table("T_Leave_Application")]
    public partial class T_Leave_Application
    {
        [Key]
        public int LeaveApplication_ID { get; set; }
        public int Company_ID { get; set; }
        public int Driver_ID { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ApplicationDay { get; set; }
        public int Leave_Kubun { get; set; }
        public int ApprovalFlow_ID { get; set; }
        [Required]
        [StringLength(255)]
        public string Remarks { get; set; }
    }
}
