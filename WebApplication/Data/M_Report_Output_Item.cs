using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("M_Report_Output_Item")]
    public partial class M_Report_Output_Item
    {
        [Key]
        public int Report_Serch_Kubun_ID { get; set; }
        [Key]
        public int Company_ID { get; set; }
        [Key]
        public int User_ID { get; set; }
        [Key]
        public int Report_Output_Item_ID { get; set; }
        public int Sort_Order { get; set; }
        [StringLength(20)]
        public string Csv_Title { get; set; }
    }
}
