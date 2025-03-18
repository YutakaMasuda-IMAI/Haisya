using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Keyless]
    public partial class V_CompanyDriver
    {
        public int JISYA_YOSYA_KUBUN { get; set; }
        public int Driver_ID { get; set; }
        public int Company_ID { get; set; }
        public int Branch_ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Branch_Name { get; set; }
        [StringLength(50)]
        public string Branch_Name_Abbr { get; set; }
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
        public int? DriverSyaryo_ID { get; set; }
        public int? SyaryoManagement_ID { get; set; }
        public int? SyaryoManagement_ID1 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Start_Date { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? End_Date { get; set; }
        public int? Group_ID { get; set; }
        public int? SyasyuKubun_ID { get; set; }
        public int? Syaryo_ID { get; set; }
        [StringLength(20)]
        public string Syasyu { get; set; }
        [StringLength(10)]
        public string Kata { get; set; }
        [StringLength(10)]
        public string Syaban_Chiiki { get; set; }
        [StringLength(10)]
        public string Syaban_Bunrui { get; set; }
        [StringLength(10)]
        public string Syaban_Kana { get; set; }
        [StringLength(10)]
        public string Syaban_Number { get; set; }
        [StringLength(30)]
        public string SyasyuDisplay { get; set; }
        [StringLength(20)]
        public string SIZE { get; set; }
        public int? Kata_Sort { get; set; }
        [StringLength(50)]
        public string Kata_Display { get; set; }
        public int? Kubun_Sort { get; set; }
        [StringLength(20)]
        public string Kubun_Name { get; set; }
        public int? HaisyaGroupSortOrder { get; set; }
        [StringLength(50)]
        public string HaisyaGroupName { get; set; }
    }
}
