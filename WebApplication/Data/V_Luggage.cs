using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Keyless]
    public partial class V_Luggage
    {
        public int Luggage_Group_ID { get; set; }
        public int Company_ID { get; set; }
        public int GroupSortOrder { get; set; }
        [Required]
        [StringLength(50)]
        public string Luggage_GroupName { get; set; }
        [StringLength(50)]
        public string Group_Remarks { get; set; }
        public bool Group_Del_Flg { get; set; }
        public int Luggage_ID { get; set; }
        public int SortOrder { get; set; }
        [Required]
        [StringLength(50)]
        public string Luggage_Name { get; set; }
        [StringLength(50)]
        public string Unit_Name { get; set; }
        [StringLength(50)]
        public string Remarks { get; set; }
        public bool Del_Flg { get; set; }
    }
}
