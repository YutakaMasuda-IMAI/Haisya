using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Table("T_ApprovalFlow")]
    public partial class T_ApprovalFlow
    {
        [Key]
        public int ApprovalFlow_ID { get; set; }
        public int? Approval_Kubun { get; set; }
        public int CompanyOrganization_ID { get; set; }
    }
}
