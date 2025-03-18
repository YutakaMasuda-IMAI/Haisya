using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("M_Customer_TantouHaisyaGroup")]
    public partial class M_Customer_TantouHaisyaGroup
    {
        [Key]
        public int Customer_ID { get; set; }
        [Key]
        public int Group_ID { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
    }
}
