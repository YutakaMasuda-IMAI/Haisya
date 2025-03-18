using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Table("M_Leave_Tpye")]
    public partial class M_Leave_Tpye
    {
        [Key]
        public int Leave_Tpye_ID { get; set; }
        public int Company_ID { get; set; }
        public int Leave_Kubun { get; set; }
        [Required]
        [StringLength(20)]
        public string Leave_Type_Name { get; set; }
        public int CompanyOrganization_ID { get; set; }
        public double Leave_Days { get; set; }
        [StringLength(10)]
        public string From_Time { get; set; }
        [StringLength(10)]
        public string To_Time { get; set; }
        public int? Limit_Month { get; set; }
        public int? Limit_Kubun { get; set; }
        public bool Del_Flg { get; set; }
    }
}
