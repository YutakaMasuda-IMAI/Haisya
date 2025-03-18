using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Haisya_Batch")]
    public partial class T_Haisya_Batch
    {
        [Key]
        public int HaisyaBatch_ID { get; set; }
        public int Haisya_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdateDateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? BatchEndTime { get; set; }
        [StringLength(50)]
        public string BatchResult { get; set; }
        [StringLength(255)]
        public string BatchErrorMsg { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Reported { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
    }
}
