using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Check_Seikyu")]
    [Index(nameof(Customer_Branch_ID), nameof(Seikyu_Month), nameof(Shime_Day), nameof(Del_Datetime), nameof(Zei_Kubun), Name = "IX_T_Check_Seikyu", IsUnique = true)]
    public partial class T_Check_Seikyu
    {
        [Key]
        public int Check_Seikyu_ID { get; set; }
        public int Company_ID { get; set; }
        public int Print_Pattern { get; set; }
        public int Check_Kubun { get; set; }
        public int Check_Status { get; set; }
        public int Customer_Branch_ID { get; set; }
        [Column(TypeName = "date")]
        public DateTime Seikyu_Month { get; set; }
        public int Shime_Day { get; set; }
        public int Zei_Kubun { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Del_Datetime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Print_Datetime { get; set; }
        [Column(TypeName = "date")]
        public DateTime Print_Date { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Print_To_Date { get; set; }
        [StringLength(50)]
        public string Mail_Address1 { get; set; }
        [StringLength(50)]
        public string Mail_Address2 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        public int Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Update_Datetime { get; set; }
        public int Update_User { get; set; }
        [Column(TypeName = "date")]
        public DateTime? FROM_DATE { get; set; }
        [Column(TypeName = "date")]
        public DateTime? TO_DATE { get; set; }
    }
}
