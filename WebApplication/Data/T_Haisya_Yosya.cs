using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Haisya_Yosya")]
    public partial class T_Haisya_Yosya
    {
        [Key]
        public int Haisya_ID { get; set; }
        [Key]
        public int Yosya_Sort { get; set; }
        public int Yosya_Count { get; set; }
        public int Yosya_Branch_ID { get; set; }
        public int Yosya_Tantou_ID { get; set; }
        public int YosyaDriver_ID { get; set; }
        public int YosyaDriverSyaryo_ID { get; set; }
        public int Unso_Flg { get; set; }
        public int Yosya_Shiharai_Kubun { get; set; }
        [Column(TypeName = "money")]
        public decimal Yosya_Shiharai_Money { get; set; }
        [StringLength(30)]
        public string Yosya_No2_Company_Name { get; set; }
        [StringLength(30)]
        public string Yosya_No2_Driver_Name { get; set; }
        [StringLength(5)]
        public string Yosya_No2_Car_Number { get; set; }
        [StringLength(15)]
        public string Yosya_No2_Phone { get; set; }
        [StringLength(30)]
        public string Yosya_No2_Syasyu { get; set; }
    }
}
