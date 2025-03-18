using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Portal_Info")]
    public partial class T_Portal_Info
    {
        [Key]
        public int Portal_Info_ID { get; set; }
        public int Portal_Kubun { get; set; }
        public int Critical_Kubun { get; set; }
        public int User_ID { get; set; }
        public int Company_ID { get; set; }
        [StringLength(50)]
        public string Category { get; set; }
        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        [Required]
        [StringLength(255)]
        public string Detail { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Limit_Date { get; set; }
        public int Display_Flg { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        public int Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Update_Datetime { get; set; }
        public int Update_User { get; set; }
        [StringLength(50)]
        public string Controller { get; set; }
        [StringLength(50)]
        public string Action { get; set; }
        public double? Param_Int_1 { get; set; }
        [StringLength(20)]
        public string Param_Int_1_Name { get; set; }
        public double? Param_Int_2 { get; set; }
        [StringLength(20)]
        public string Param_Int_2_Name { get; set; }
        public double? Param_Int_3 { get; set; }
        [StringLength(20)]
        public string Param_Int_3_Name { get; set; }
        [StringLength(20)]
        public string Param_string_1 { get; set; }
        [StringLength(20)]
        public string Param_string_1_Name { get; set; }
        [StringLength(20)]
        public string Param_string_2 { get; set; }
        [StringLength(20)]
        public string Param_string_2_Name { get; set; }
        [StringLength(20)]
        public string Param_string_3 { get; set; }
        [StringLength(20)]
        public string Param_string_3_Name { get; set; }
        public int Print_ID { get; set; }
    }
}
