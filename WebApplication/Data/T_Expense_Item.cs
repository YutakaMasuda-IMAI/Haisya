using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Keyless]
    [Table("T_Expense_Item")]
    public partial class T_Expense_Item
    {
        public int Expense_Item_ID { get; set; }
        public int Expense_Kubun { get; set; }
        public int Sort_Order { get; set; }
        [StringLength(30)]
        public string Item_Name { get; set; }
        [StringLength(30)]
        public string Item_Display { get; set; }
        public int Del_Flg { get; set; }
    }
}
