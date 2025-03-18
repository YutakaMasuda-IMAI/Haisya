using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("M_CompanyUser_Group")]
    public partial class M_CompanyUser_Group
    {
        [Key]
        public int Group_ID { get; set; }
        public int Company_ID { get; set; }
        public int Group_Kubun { get; set; }
        public int SortOrder { get; set; }
        [Required]
        [StringLength(50)]
        public string Group_Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Display_Name { get; set; }
        public bool Del_Flg { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UP_DATE { get; set; }
        [StringLength(30)]
        public string Phone { get; set; }
    }
}
