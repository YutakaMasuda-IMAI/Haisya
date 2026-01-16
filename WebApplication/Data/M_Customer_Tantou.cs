using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("M_Customer_Tantou")]
public partial class M_Customer_Tantou
{
    [Key]
    public int Tantou_ID { get; set; }

    public int Customer_ID { get; set; }

    public int Customer_Branch_ID { get; set; }

    public int Company_ID { get; set; }

    [StringLength(20)]
    public string Tantou_Code { get; set; }

    [StringLength(30)]
    public string Busyo_Name { get; set; }

    [StringLength(20)]
    public string Position_Name { get; set; }

    [StringLength(60)]
    public string Tantou_Name { get; set; }

    [StringLength(40)]
    public string Tantou_Name_Kana { get; set; }

    [StringLength(20)]
    public string Tantou_Name_Abbr { get; set; }

    [StringLength(8)]
    public string PostCode { get; set; }

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

    [StringLength(40)]
    public string Address3 { get; set; }

    [StringLength(13)]
    public string Phone1 { get; set; }

    [StringLength(13)]
    public string Phone2 { get; set; }

    [StringLength(15)]
    public string Cell_Phone { get; set; }

    [StringLength(13)]
    public string Fax1 { get; set; }

    [StringLength(13)]
    public string Fax2 { get; set; }

    /// <summary>
    /// 0:印刷する、1:印刷しない
    /// </summary>
    public int Report_Output_Name_Flg { get; set; }

    public bool Del_Flg { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Insert_Datetime { get; set; }

    public int Insert_User { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Update_Datetime { get; set; }

    public int Update_User { get; set; }
}
