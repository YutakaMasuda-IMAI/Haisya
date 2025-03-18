using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Shitabarai")]
    [Index(nameof(Yosya_Branch_ID), nameof(Zei_Kubun), nameof(Shime_Day), nameof(Shitabarai_Month), nameof(Del_Datetime), Name = "IX_T_Shitabarai", IsUnique = true)]
    public partial class T_Shitabarai
    {
        [Key]
        public int Shitabarai_ID { get; set; }
        public int Company_ID { get; set; }
        public int Shitabarai_Kubun { get; set; }
        public int Print_Pattern { get; set; }
        public int Yosya_Branch_ID { get; set; }
        public int Zei_Kubun { get; set; }
        [Column(TypeName = "date")]
        public DateTime Shitabarai_Month { get; set; }
        public int Shime_Day { get; set; }
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
    }
}
