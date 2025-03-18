using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("M_Report_Serch")]
    public partial class M_Report_Serch
    {
        [Key]
        public int Report_Serch_ID { get; set; }
        public int Report_Number { get; set; }
        [Required]
        [StringLength(50)]
        public string Report_Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Display_Title { get; set; }
        public int Csv_Flg { get; set; }
        public int Pdf_Flg { get; set; }
        [StringLength(50)]
        public string Csv_FileName { get; set; }
    }
}
