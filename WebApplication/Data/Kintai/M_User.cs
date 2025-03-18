using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Table("M_User")]
    public partial class M_User
    {
        [Key]
        public int USER_ID { get; set; }
        [StringLength(50)]
        public string LOGIN_ID { get; set; }
        [StringLength(6)]
        public string Password { get; set; }
        [StringLength(100)]
        public string USER_NAME { get; set; }
        public bool DEL_FLG { get; set; }
        public int ROLE { get; set; }
    }
}
