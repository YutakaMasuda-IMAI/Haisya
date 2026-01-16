using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("M_Vender")]
public partial class M_Vender
{
    [Key]
    public int Vender_ID { get; set; }

    public int Company_ID { get; set; }

    [StringLength(50)]
    public string Vender_Code { get; set; }

    [Required]
    [StringLength(40)]
    public string Vender_Name { get; set; }

    [StringLength(16)]
    public string Vender_Name_Kana { get; set; }

    [StringLength(20)]
    public string Vender_Name_Abbr { get; set; }

    [StringLength(10)]
    public string Post_Code { get; set; }

    [StringLength(50)]
    public string Mail_Title { get; set; }

    [StringLength(50)]
    public string Mail_Address1 { get; set; }

    [StringLength(50)]
    public string Mail_Address2 { get; set; }

    [StringLength(40)]
    public string Address1 { get; set; }

    [StringLength(40)]
    public string Address2 { get; set; }

    [StringLength(13)]
    public string Phone1 { get; set; }

    [StringLength(13)]
    public string Phone2 { get; set; }

    [StringLength(13)]
    public string Fax1 { get; set; }

    [StringLength(13)]
    public string Fax2 { get; set; }

    public int ShiharaiTantouID { get; set; }

    public int? ClosingDate1 { get; set; }

    public int Shime_Day { get; set; }

    [StringLength(255)]
    public string ShiharaiRemarks { get; set; }

    public int Shiharai_Vender_ID { get; set; }

    public bool Del_Flg { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Insert_Datetime { get; set; }

    public int Insert_User { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Update_Datetime { get; set; }

    public int Update_User { get; set; }
}
