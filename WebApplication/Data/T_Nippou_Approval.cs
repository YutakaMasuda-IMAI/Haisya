using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Nippou_Approval")]
    public partial class T_Nippou_Approval
    {
        [Key]
        public int Nippou_Approval_ID { get; set; }
        public int Nippou_ID { get; set; }
        public int Approval_Group_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Limit_DateTime { get; set; }
        public int Approval_Result { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Result_Datetime { get; set; }
        [StringLength(255)]
        public string Order_Memo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Insert_Datetime { get; set; }
        public int? Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Update_Datetime { get; set; }
        public int? Update_User { get; set; }
    }
}
