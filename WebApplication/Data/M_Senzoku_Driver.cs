using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("M_Senzoku_Driver")]
    [Index(nameof(Company_ID), nameof(Driver_ID), nameof(From_Date), Name = "IX_M_Senzoku_Driver", IsUnique = true)]
    public partial class M_Senzoku_Driver
    {
        [Key]
        public int Senzoku_Driver_ID { get; set; }
        public int SenzokuID { get; set; }
        public int Company_ID { get; set; }
        public int Driver_ID { get; set; }
        public int DriverSyaryo_ID { get; set; }
        public int Yosya_Branch_ID { get; set; }
        public int YosyaDriver_ID { get; set; }
        public int YosyaDriverSyaryo_ID { get; set; }
        [Column(TypeName = "date")]
        public DateTime From_Date { get; set; }
        [Column(TypeName = "date")]
        public DateTime? To_Date { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
    }
}
