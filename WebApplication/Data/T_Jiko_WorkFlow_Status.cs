using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Jiko_WorkFlow_Status")]
    public partial class T_Jiko_WorkFlow_Status
    {
        [Key]
        public int Jiko_WorkFlow_Status_ID { get; set; }
        public int Jiko_WorkFlow_ID { get; set; }
        public int Approval_Kubun { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Approval_Datetime { get; set; }
        public int Approval_User_ID { get; set; }
        [StringLength(255)]
        public string Reject_Reason { get; set; }
    }
}
