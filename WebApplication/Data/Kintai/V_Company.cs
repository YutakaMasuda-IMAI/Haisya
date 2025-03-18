using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Keyless]
    public partial class V_Company
    {
        public int Company_ID { get; set; }
        [StringLength(50)]
        public string Company_Code { get; set; }
        [Required]
        [StringLength(50)]
        public string Company_Name { get; set; }
        [StringLength(50)]
        public string Company_Name_Abbr { get; set; }
        [StringLength(3)]
        public string Post1 { get; set; }
        [StringLength(4)]
        public string Post2 { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(30)]
        public string Phone { get; set; }
        [StringLength(30)]
        public string Fax { get; set; }
        public bool Del_Flg { get; set; }
    }
}
