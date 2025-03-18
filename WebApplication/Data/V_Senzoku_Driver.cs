using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Keyless]
    public partial class V_Senzoku_Driver
    {
        public int Senzoku_Driver_ID { get; set; }
        public int SenzokuID { get; set; }
        public int Company_ID { get; set; }
        public int Driver_ID { get; set; }
        public int? Employee_Number { get; set; }
        [Required]
        [StringLength(50)]
        public string Display_Name { get; set; }
        public int? SyaryoManagement_ID { get; set; }
        [StringLength(20)]
        public string Syasyu { get; set; }
        [StringLength(10)]
        public string Kata { get; set; }
        [StringLength(10)]
        public string Syaban_Number { get; set; }
        [Required]
        [StringLength(40)]
        public string SYABAN { get; set; }
        [Column(TypeName = "date")]
        public DateTime From_Date { get; set; }
        [Column(TypeName = "date")]
        public DateTime? To_Date { get; set; }
        public int DriverSyaryo_ID { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
        public int Customer_Branch_ID { get; set; }
        [StringLength(10)]
        public string Customer_Branch_Code { get; set; }
        [Required]
        [StringLength(50)]
        public string Customer_Branch_Name { get; set; }
        [StringLength(50)]
        public string Customer_Branch_Name_Abbr { get; set; }
        public int KokyakuTantouId { get; set; }
        [StringLength(20)]
        public string Tantou_Code { get; set; }
        [StringLength(60)]
        public string Tantou_Name { get; set; }
        [StringLength(20)]
        public string Tantou_Name_Abbr { get; set; }
        [StringLength(13)]
        public string Tantou_Phone1 { get; set; }
        public int? Syaryo_ID { get; set; }
        [StringLength(30)]
        public string SyasyuDisplay { get; set; }
        [Required]
        [StringLength(50)]
        public string Senzoku_Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Senzoku_Name_Abbr { get; set; }
        public int Haisya_Group_ID { get; set; }
    }
}
