using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Table("T_ApprovalFlow_User")]
    public partial class T_ApprovalFlow_User
    {
        [Key]
        public int ApprovalFlow_ID { get; set; }
        [Key]
        public int User_ID { get; set; }
        public int Company_ID { get; set; }
    }
}
