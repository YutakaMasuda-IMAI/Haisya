using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Nippou_Stay")]
    public partial class T_Nippou_Stay
    {
        [Key]
        public int Nippou_ID { get; set; }
        [Column(TypeName = "date")]
        public DateTime Day { get; set; }
        [StringLength(255)]
        public string Start_Datetime { get; set; }
        [StringLength(255)]
        public string End_Datetime { get; set; }
        [StringLength(255)]
        public string Start_ShikuName { get; set; }
        [StringLength(255)]
        public string End_ShikuName { get; set; }
        [StringLength(255)]
        public string Start_PointName { get; set; }
        [StringLength(255)]
        public string End_PointName { get; set; }
        public int? Interval_Time { get; set; }
        [Column(TypeName = "money")]
        public decimal Dllowance { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Insert_Datetime { get; set; }
        public int? Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Update_Datetime { get; set; }
        public int? Update_User { get; set; }
    }
}
