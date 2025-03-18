using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("M_Customer_Driver_Syaryo")]
    public partial class M_Customer_Driver_Syaryo
    {
        [Key]
        public int Customer_DriverSyaryo_ID { get; set; }
        public int Customer_Driver_ID { get; set; }
        public int Customer_ID { get; set; }
        public int Customer_Branch_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Start_Date { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? End_Date { get; set; }
        [Required]
        [StringLength(20)]
        public string SIZE { get; set; }
        [Required]
        [StringLength(20)]
        public string Syasyu { get; set; }
        [Required]
        [StringLength(10)]
        public string Kata { get; set; }
        [StringLength(10)]
        public string Syaban_Chiiki { get; set; }
        [StringLength(10)]
        public string Syaban_Bunrui { get; set; }
        [StringLength(10)]
        public string Syaban_Kana { get; set; }
        [Required]
        [StringLength(10)]
        public string Syaban_Number { get; set; }
        [StringLength(255)]
        public string REMARKS { get; set; }
        public bool Del_Flg { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        public int Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Update_Datetime { get; set; }
        public int Update_User { get; set; }
    }
}
