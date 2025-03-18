using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_YosyaShiharai")]
    public partial class T_YosyaShiharai
    {
        [Key]
        public int YosyaShiharai_ID { get; set; }
        public int Shitabarai_ID { get; set; }
        public int Process_Kubun { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Process_Date { get; set; }
        [Column(TypeName = "money")]
        public decimal Cash_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal Transfer_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal Draft_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal Check_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal Service_Charge_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal Unchin_Offset { get; set; }
        [Column(TypeName = "money")]
        public decimal General_Offset { get; set; }
        [Column(TypeName = "money")]
        public decimal Adjustment_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal Total_Amount { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
        public bool Del_Flg { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        public int Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Update_Datetime { get; set; }
        public int Update_User { get; set; }
    }
}
