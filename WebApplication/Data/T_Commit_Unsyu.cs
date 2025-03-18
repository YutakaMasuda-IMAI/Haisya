using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Commit_Unsyu")]
    public partial class T_Commit_Unsyu
    {
        [Key]
        public int Commit_Unsyu_ID { get; set; }
        public int Uriage_Unsyu_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Shime_Datetime { get; set; }
        [Column(TypeName = "date")]
        public DateTime Seikyu_Month { get; set; }
        public int Shime_Day { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        public int Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Update_Datetime { get; set; }
        public int Update_User { get; set; }
    }
}
