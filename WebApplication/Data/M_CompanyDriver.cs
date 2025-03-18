using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("M_CompanyDriver")]
    public partial class M_CompanyDriver
    {
        [Key]
        public int Driver_ID { get; set; }
        public int Branch_ID { get; set; }
        public int Company_ID { get; set; }
        public int? Employee_Number { get; set; }
        [Required]
        [StringLength(50)]
        public string Last_Name { get; set; }
        [StringLength(50)]
        public string First_Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Display_Name { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Nyusya_Date { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? GyomuStart_Date { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Taisyoku_Date { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UP_DATE { get; set; }
        public bool Del_Flg { get; set; }
        [StringLength(30)]
        public string Phone1 { get; set; }
        [StringLength(30)]
        public string Phone2 { get; set; }
        [StringLength(100)]
        public string Address1 { get; set; }
        [StringLength(100)]
        public string Address2 { get; set; }
        [StringLength(20)]
        public string LineID { get; set; }
    }
}
