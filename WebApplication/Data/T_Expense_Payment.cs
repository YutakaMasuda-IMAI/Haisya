using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("T_Expense_Payment")]
public partial class T_Expense_Payment
{
    [Key]
    public int Expense_Payment_ID { get; set; }

    public int Expense_ID { get; set; }

    public int Vender_ID { get; set; }

    public int Expense_Item_ID { get; set; }

    public DateOnly Expense_Day { get; set; }

    [Column(TypeName = "money")]
    public decimal Expense_Money { get; set; }

    [Column(TypeName = "money")]
    public decimal Payment_Money { get; set; }

    public DateOnly Payment_Month { get; set; }

    [StringLength(255)]
    public string Remarks { get; set; }

    /// <summary>
    /// 0:未,1:締め	
    /// </summary>
    public int Shime_Kubun { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Insert_Datetime { get; set; }

    public int Insert_User { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Update_Datetime { get; set; }

    public int Update_User { get; set; }
}
