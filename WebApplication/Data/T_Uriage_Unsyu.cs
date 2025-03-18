using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Uriage_Unsyu")]
    public partial class T_Uriage_Unsyu
    {
        [Key]
        public int Uriage_Unsyu_ID { get; set; }
        public int Uriage_ID { get; set; }
        public int Sort { get; set; }
        public int Default_Kubun { get; set; }
        public int Uriage_Kubun { get; set; }
        [Column(TypeName = "date")]
        public DateTime Unsyu_Date { get; set; }
        public int SyaryoManagement_ID { get; set; }
        public int Driver_ID { get; set; }
        public int Unsyu_Kubun { get; set; }
        [Column(TypeName = "money")]
        public decimal Seisan { get; set; }
        [Column(TypeName = "money")]
        public decimal KojinFutan { get; set; }
        [Column(TypeName = "money")]
        public decimal KojinUnsyu { get; set; }
        [Column(TypeName = "money")]
        public decimal Route_Teate { get; set; }
        [Column(TypeName = "money")]
        public decimal Route_OverTime { get; set; }
        [Column(TypeName = "money")]
        public decimal Route_Midnight { get; set; }
        public bool Del_Flg { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        public int Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Update_Datetime { get; set; }
        public int Update_User { get; set; }
    }
}
