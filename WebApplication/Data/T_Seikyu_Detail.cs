using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Seikyu_Detail")]
    public partial class T_Seikyu_Detail
    {
        [Key]
        public int Seikyu_ID { get; set; }
        [Key]
        public int Uriage_Unchin_ID { get; set; }
        public int Seikyu_Detaii_No { get; set; }
        public int Anken_ID { get; set; }
        public int Uriage_ID { get; set; }
        public int Nippou_ID { get; set; }
    }
}
