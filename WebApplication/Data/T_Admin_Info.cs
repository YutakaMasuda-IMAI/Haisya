using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Admin_Info")]
    public partial class T_Admin_Info
    {
        [Key]
        public int AdminInfo_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Info_Datetime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Info_End_Datetime { get; set; }
        public int Info_Kubun { get; set; }
        [Required]
        [StringLength(100)]
        public string Info_Title { get; set; }
        [Required]
        public string Info_Detail { get; set; }
    }
}
