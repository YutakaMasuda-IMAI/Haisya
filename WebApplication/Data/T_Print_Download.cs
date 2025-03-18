using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Print_Download")]
    public partial class T_Print_Download
    {
        [Key]
        public int Print_Rireki_ID { get; set; }
        public int Print_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Download_Datetime { get; set; }
        public int Download_User { get; set; }
        public int Download_IP_Address { get; set; }
        [Required]
        [StringLength(50)]
        public string Download_Web_Browser { get; set; }
        public bool Mail_KickOff_Flg { get; set; }
    }
}
