using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Haisya_SyabanRenraku_Detail")]
    public partial class T_Haisya_SyabanRenraku_Detail
    {
        [Key]
        public int SyabanRenraku_ID { get; set; }
        [Key]
        public int Anken_ID { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
    }
}
