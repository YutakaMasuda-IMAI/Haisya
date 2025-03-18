using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Keyless]
    [Table("T_Report_Layout")]
    public partial class T_Report_Layout
    {
        public int Print_Kubun { get; set; }
        public int Zei_Kubun { get; set; }
        public int Report_Sort { get; set; }
        [Required]
        [StringLength(30)]
        public string Report_Name { get; set; }
        [StringLength(20)]
        public string Report_Explan { get; set; }
        [StringLength(40)]
        public string Report_Remarks { get; set; }
        [StringLength(50)]
        public string Report_HTML { get; set; }
        public int Csv_Output_Flg { get; set; }
        public int Report_Serch_ID { get; set; }
        public int Report_Serch_Kubun_ID { get; set; }
    }
}
