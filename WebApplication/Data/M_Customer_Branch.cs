using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("M_Customer_Branch")]
    public partial class M_Customer_Branch
    {
        [Key]
        public int Customer_Branch_ID { get; set; }
        public int Customer_ID { get; set; }
        public int SortOrder { get; set; }
        public int Oya_Branch_ID { get; set; }
        [StringLength(10)]
        public string Customer_Branch_Number { get; set; }
        [StringLength(10)]
        public string Customer_Branch_Code { get; set; }
        [Required]
        [StringLength(50)]
        public string Customer_Branch_Name { get; set; }
        [StringLength(40)]
        public string Customer_Branch_Name_Kana { get; set; }
        [StringLength(50)]
        public string Customer_Branch_Name_Abbr { get; set; }
        [StringLength(8)]
        public string Customer_Branch_Post { get; set; }
        [StringLength(80)]
        public string Customer_Branch_Address1 { get; set; }
        [StringLength(40)]
        public string Customer_Branch_Address2 { get; set; }
        [StringLength(40)]
        public string Customer_Branch_Address3 { get; set; }
        [StringLength(13)]
        public string Customer_Branch_Phone1 { get; set; }
        [StringLength(13)]
        public string Customer_Branch_Phone2 { get; set; }
        [StringLength(13)]
        public string Customer_Branch_Fax1 { get; set; }
        [StringLength(13)]
        public string Customer_Branch_Fax2 { get; set; }
        [StringLength(50)]
        public string Mail_Title { get; set; }
        [StringLength(50)]
        public string Mail_Address1 { get; set; }
        [StringLength(50)]
        public string Mail_Address2 { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
        [StringLength(255)]
        public string AnkenRemarks { get; set; }
        [StringLength(255)]
        public string SeikyuRemarks { get; set; }
        [StringLength(255)]
        public string Shiharai_Remarks { get; set; }
        public int Seikyu_Customer_Branch_ID { get; set; }
        public int SeikyuTantouID { get; set; }
        public int Seikyu_Kubun { get; set; }
        public int SeikyuDate_Kubun { get; set; }
        [StringLength(100)]
        public string Seikyu_Mail_Address1 { get; set; }
        [StringLength(50)]
        public string Seikyu_Mail_Header { get; set; }
        [StringLength(13)]
        public string Seikyu_Phone1 { get; set; }
        [StringLength(13)]
        public string Seikyu_Fax1 { get; set; }
        [StringLength(8)]
        public string Seikuy_PostCode { get; set; }
        [StringLength(50)]
        public string Seikyu_Atesaki { get; set; }
        [StringLength(50)]
        public string Seikyu_Address1 { get; set; }
        [StringLength(50)]
        public string Seikyu_Address2 { get; set; }
        public int Shime_Day { get; set; }
        public int Tax_Fraction_Kubun { get; set; }
        public double Tax_Fraction_Position { get; set; }
        public int Shiharai_TantouID { get; set; }
        public int Shiharai_Shime_Day { get; set; }
        public int Shiharai_Customer_Branch_ID { get; set; }
        [StringLength(100)]
        public string Shiharai_Mail_Address1 { get; set; }
        [StringLength(50)]
        public string Shiharai_Mail_Header { get; set; }
        [StringLength(13)]
        public string Shiharai_Phone1 { get; set; }
        [StringLength(13)]
        public string Shiharai_Fax1 { get; set; }
        [StringLength(8)]
        public string Shiharai_PostCode { get; set; }
        [StringLength(50)]
        public string Shiharai_Atesaki { get; set; }
        [StringLength(50)]
        public string Shiharai_Address1 { get; set; }
        [StringLength(50)]
        public string Shiharai_Address2 { get; set; }
        public int Shiharai_Sight { get; set; }
        public int Shiharai_Day { get; set; }
        public int? Customer_Branch_Code_Trac_Tokuisaki { get; set; }
        public int? Customer_Branch_Code_Trac_Yosyasaki { get; set; }
        public int Collection_Sight { get; set; }
        public int Collection_Day { get; set; }
        public int Receipt_Output_Flg { get; set; }
        [StringLength(8)]
        public string Receipt_Post { get; set; }
        [StringLength(50)]
        public string Receipt_Atesaki { get; set; }
        [StringLength(80)]
        public string Receipt_Address1 { get; set; }
        [StringLength(13)]
        public string Receipt_Phone1 { get; set; }
        [StringLength(13)]
        public string Receipt_Fax1 { get; set; }
        public int Toll_Kubun { get; set; }
        public bool Del_Flg { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        public int Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Update_Datetime { get; set; }
        public int Update_User { get; set; }
    }
}
