using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Keyless]
    public partial class M_Yosya
    {
        public int Yosya_ID { get; set; }
        public int Company_ID { get; set; }
        public int Yosya_ID_Oya { get; set; }
        [StringLength(20)]
        public string Yosya_Code { get; set; }
        [StringLength(20)]
        public string Yosya_Code_Oya { get; set; }
        [Required]
        [StringLength(60)]
        public string Yosya_Name { get; set; }
        [StringLength(40)]
        public string Yosya_Name_Kana { get; set; }
        [StringLength(40)]
        public string Yosya_Name_Abbr { get; set; }
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
        public DateTime? Insert_Datetime { get; set; }
        public int? Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Update_Datetime { get; set; }
        public int? Update_User { get; set; }
    }
}
