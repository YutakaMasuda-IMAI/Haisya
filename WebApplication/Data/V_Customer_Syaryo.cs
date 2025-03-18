using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Keyless]
    public partial class V_Customer_Syaryo
    {
        public int Customer_DriverSyaryo_ID { get; set; }
        public int Company_ID { get; set; }
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
        [Required]
        [StringLength(40)]
        public string FULL_SYABAN { get; set; }
        [StringLength(255)]
        public string REMARKS { get; set; }
        public bool Del_Flg { get; set; }
        public int Yosya_Kubun { get; set; }
        public int Group_ID { get; set; }
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
        public DateTime From_Date { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? To_Date { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        public int Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Update_Datetime { get; set; }
        public int Update_User { get; set; }
    }
}
