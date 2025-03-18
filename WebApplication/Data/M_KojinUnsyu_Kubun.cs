using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("M_KojinUnsyu_Kubun")]
    public partial class M_KojinUnsyu_Kubun
    {
        [Key]
        public int KojinUnsyuKubun_ID { get; set; }
        public int Company_ID { get; set; }
        public int Kubun_Sort { get; set; }
        [Required]
        [StringLength(20)]
        public string Kubun_Name { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        public int Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Update_Datetime { get; set; }
        public int Update_User { get; set; }
    }
}
