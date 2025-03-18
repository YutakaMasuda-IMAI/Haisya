using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Anken_Publish")]
    public partial class T_Anken_Publish
    {
        [Key]
        public int Anken_ID { get; set; }
        public int PublishGroup_ID { get; set; }
        public bool Publish_Flg { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Publish_FromDatetime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Publish_ToDatetime { get; set; }
    }
}
