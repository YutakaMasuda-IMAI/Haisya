using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Keyless]
    [Table("M_Customer_bak20240718")]
    public partial class M_Customer_bak20240718
    {
        public int Customer_ID { get; set; }
        public int Yosya_Flg { get; set; }
        public int Company_ID { get; set; }
        public int Customer_ID_Oya { get; set; }
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
        [StringLength(50)]
        public string Mail_Title { get; set; }
        [StringLength(50)]
        public string Mail_Address1 { get; set; }
        [StringLength(50)]
        public string Mail_Address2 { get; set; }
        public int Seikyu_Kubun { get; set; }
        public int SeikyuDate_Kubun { get; set; }
        public int Toll_Kubun { get; set; }
        public int Shime_Day { get; set; }
        [StringLength(255)]
        public string AnkenRemarks { get; set; }
        [StringLength(255)]
        public string SeikyuRemarks { get; set; }
        public int? SeikyuTantouID { get; set; }
        public bool Del_Flg { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Insert_Datetime { get; set; }
        public int? Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Update_Datetime { get; set; }
        public int? Update_User { get; set; }
        [StringLength(50)]
        public string Seikyu_Address { get; set; }
        [StringLength(8)]
        public string Seikuy_PostCode { get; set; }
        public int Seikyu_Customer_ID { get; set; }
        public int Shiharai_TantouID { get; set; }
        public int Shiharai_Shime_Day { get; set; }
        [StringLength(255)]
        public string Shiharai_Remarks { get; set; }
        public int Shiharai_Yosya_ID { get; set; }
    }
}
