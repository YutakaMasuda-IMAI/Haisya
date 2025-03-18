using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Anken")]
    [Index(nameof(Anken_No), Name = "IX_T_Anken", IsUnique = true)]
    public partial class T_Anken
    {
        [Key]
        public int Anken_ID { get; set; }
        [Required]
        [StringLength(20)]
        public string Anken_No { get; set; }
        public int Anken_Status { get; set; }
        public int Anken_Latest_Order { get; set; }
        public int Company_ID { get; set; }
        public int Branch_ID { get; set; }
        public int Anken_Kubun { get; set; }
        public int SenzokuID { get; set; }
        public int Senzoku_Driver_ID { get; set; }
    }
}
