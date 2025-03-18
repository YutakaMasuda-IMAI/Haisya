using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Nippou")]
    public partial class T_Nippou
    {
        [Key]
        public int Nippou_ID { get; set; }
        public int AnkenDisplay_ID { get; set; }
        public int Anken_ID { get; set; }
        public int Receipt { get; set; }
        [Column(TypeName = "date")]
        public DateTime Receipt_Date { get; set; }
        [StringLength(255)]
        public string Commnet { get; set; }
        public int ApprovalStatus { get; set; }
        public int RenkeiStatus { get; set; }
        public double? Distance { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Insert_Datetime { get; set; }
        public int? Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Update_Datetime { get; set; }
        public int? Update_User { get; set; }
        public int DegitakoLink_Result { get; set; }
    }
}
