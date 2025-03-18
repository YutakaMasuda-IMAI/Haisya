using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("M_Jiko_WorkFlow")]
    public partial class M_Jiko_WorkFlow
    {
        [Key]
        public int Jiko_WorkFlow_Base_ID { get; set; }
        [StringLength(50)]
        public string Jiko_WorkFlow_Name { get; set; }
        public int Del_Flg { get; set; }
    }
}
