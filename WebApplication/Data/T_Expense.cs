using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Expense")]
    public partial class T_Expense
    {
        [Key]
        public int Expense_ID { get; set; }
        public int Company_ID { get; set; }
        public int Expense_Kubun { get; set; }
        public int Jiko_ID { get; set; }
        public int SyaryoManagement_ID { get; set; }
        public int Driver_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        public int Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Update_Datetime { get; set; }
        public int Update_User { get; set; }
    }
}
