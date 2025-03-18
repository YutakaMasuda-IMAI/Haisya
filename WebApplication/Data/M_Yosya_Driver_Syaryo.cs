using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Keyless]
    public partial class M_Yosya_Driver_Syaryo
    {
        public int Yosya_DriverSyaryo_ID { get; set; }
        public int Yosya_Driver_ID { get; set; }
        public int Yosya_Branch_ID { get; set; }
        public int Yosya_ID { get; set; }
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
    }
}
