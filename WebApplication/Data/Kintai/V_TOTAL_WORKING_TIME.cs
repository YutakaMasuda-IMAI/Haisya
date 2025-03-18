using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Keyless]
    public partial class V_TOTAL_WORKING_TIME
    {
        [Column(TypeName = "date")]
        public DateTime NENGETSU { get; set; }
        public int WORKER_CD { get; set; }

        public double? DISTANCE { get; set; }

        public string TOTAL_TIME_DISPLAY { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? TOTAL_TIME { get; set; }

        public string TOTAL_ZANGYO_TIME_DISPLAY { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? TOTAL_ZANGYO_TIME { get; set; }

        public string TOTAL_KOKYU_TIME_DISPLAY { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? TOTAL_KOKYU_TIME { get; set; }

        public string TOTAL_HOUTEIKYU_TIME_DISPLAY { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? TOTAL_HOUTEIKYU_TIME { get; set; }

        public string TOTAL_JITSUSHINYA_TIME_DISPLAY { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? TOTAL_JITSUSHINYA_TIME { get; set; }
    }
}
