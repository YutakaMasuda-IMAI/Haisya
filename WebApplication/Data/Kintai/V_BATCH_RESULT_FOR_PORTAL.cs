using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Keyless]
    public partial class V_BATCH_RESULT_FOR_PORTAL
    {
        public int ID { get; set; }
        [StringLength(50)]
        public string BATCH_NAME { get; set; }
        [StringLength(50)]
        public string BATCH_NAME_DISPLAY { get; set; }
        [StringLength(50)]
        public string PROC_NAME { get; set; }
        [StringLength(50)]
        public string PROC_NAME_DISPLAY { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EXIT_TIME { get; set; }
        [StringLength(2)]
        public string RESULT { get; set; }
        [StringLength(255)]
        public string MESSAGE { get; set; }
    }
}
