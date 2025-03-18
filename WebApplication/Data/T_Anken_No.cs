using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Anken_No")]
    public partial class T_Anken_No
    {
        [Key]
        public int NENDO { get; set; }
        public int NO { get; set; }
    }
}
