using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Haisya_Detail")]
    public partial class T_Haisya_Detail
    {
        [Key]
        public int Haisya_Detail_ID { get; set; }
        public int Haisya_ID { get; set; }
    }
}
