using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Commit_Shitabarai")]
    [Index(nameof(Customer_Branch_ID), nameof(Zei_Kubun), nameof(Shime_Day), nameof(Month), nameof(Del_Datetime), Name = "IX_T_Commit_Shitabarai", IsUnique = true)]
    public partial class T_Commit_Shitabarai
    {
        [Key]
        public int Shitabarai_Commit_ID { get; set; }
        public int Company_ID { get; set; }
        public int Customer_Branch_ID { get; set; }
        public int Zei_Kubun { get; set; }
        [Column(TypeName = "date")]
        public DateTime Month { get; set; }
        public int Shime_Day { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Del_Datetime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Shime_Datetime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        public int Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Update_Datetime { get; set; }
        public int Update_User { get; set; }
    }
}
