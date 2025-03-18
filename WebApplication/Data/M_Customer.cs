using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("M_Customer")]
    [Index(nameof(Customer_Code), Name = "IX_M_Customer")]
    public partial class M_Customer
    {
        [Key]
        public int Customer_ID { get; set; }
        public int Yosya_Flg { get; set; }
        public int Company_ID { get; set; }
        public int Customer_ID_Oya { get; set; }
        [Required]
        [StringLength(20)]
        public string Customer_Code { get; set; }
        [StringLength(20)]
        public string Customer_Code_Oya { get; set; }
        [Required]
        [StringLength(60)]
        public string Customer_Name { get; set; }
        [StringLength(40)]
        public string Customer_Name_Kana { get; set; }
        [StringLength(40)]
        public string Customer_Name_Abbr { get; set; }
        [StringLength(8)]
        public string PostCode { get; set; }
        [StringLength(80)]
        public string Address1 { get; set; }
        [StringLength(40)]
        public string Address2 { get; set; }
        [StringLength(40)]
        public string Address3 { get; set; }
        [StringLength(13)]
        public string Phone1 { get; set; }
        [StringLength(13)]
        public string Phone2 { get; set; }
        [StringLength(13)]
        public string Fax1 { get; set; }
        [StringLength(13)]
        public string Fax2 { get; set; }
        public bool Del_Flg { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        public int Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Update_Datetime { get; set; }
        public int Update_User { get; set; }
    }
}
