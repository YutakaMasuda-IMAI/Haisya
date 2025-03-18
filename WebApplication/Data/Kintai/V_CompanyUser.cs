using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Keyless]
    public partial class V_CompanyUser
    {
        public int User_ID { get; set; }
        public int Branch_ID { get; set; }
        public int Company_ID { get; set; }
        public int? Employee_Number { get; set; }
        [Required]
        [StringLength(50)]
        public string Last_Name { get; set; }
        [StringLength(50)]
        public string First_Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Display_Name { get; set; }
        public bool Tantou_Flg { get; set; }
        public bool Eigyo_Flg { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UP_DATE { get; set; }
        public bool Del_Flg { get; set; }
        public int Tntou_ID { get; set; }
    }
}
