using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Keyless]
public partial class M_Yosya_Branch
{
    public int Yosya_Branch_ID { get; set; }

    public int Yosya_ID { get; set; }

    public int SortOrder { get; set; }

    public int Oya_Branch_ID { get; set; }

    [StringLength(10)]
    [Unicode(false)]
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
    public string AnkenRemarks { get; set; }

    [StringLength(255)]
    public string SeikyuRemarks { get; set; }

    public int SeikyuTantouID { get; set; }

    public int Seikyu_Kubun { get; set; }

    public int SeikyuDate_Kubun { get; set; }

    public int Toll_Kubun { get; set; }

    public int Shime_Day { get; set; }

    [StringLength(50)]
    public string Seikyu_Address1 { get; set; }

    [StringLength(50)]
    public string Seikyu_Address2 { get; set; }

    [StringLength(8)]
    public string Seikuy_PostCode { get; set; }

    public int Seikyu_Customer_Branch_ID { get; set; }

    public int Shiharai_TantouID { get; set; }

    public int Shiharai_Shime_Day { get; set; }

    [StringLength(255)]
    public string Shiharai_Remarks { get; set; }

    public int Shiharai_Customer_Branch_ID { get; set; }

    public bool Del_Flg { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Insert_Datetime { get; set; }

    public int Insert_User { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Update_Datetime { get; set; }

    public int Update_User { get; set; }
}
