using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Expense_Payment")]
    public partial class T_Expense_Payment
    {
        [Key]
        public int Expense_Payment_ID { get; set; }
        public int Expense_ID { get; set; }
        public int Vender_ID { get; set; }
        public int Expense_Item_ID { get; set; }
        [Column(TypeName = "date")]
        public DateTime Expense_Day { get; set; }
        [Column(TypeName = "money")]
        public decimal Expense_Money { get; set; }
        [Column(TypeName = "money")]
        public decimal Payment_Money { get; set; }
        [Column(TypeName = "date")]
        public DateTime Payment_Month { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
        public int Shime_Kubun { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        public int Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Update_Datetime { get; set; }
        public int Update_User { get; set; }
    }
}
