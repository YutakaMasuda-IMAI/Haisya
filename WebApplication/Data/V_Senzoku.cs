using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Keyless]
public partial class V_Senzoku
{
    public int SenzokuID { get; set; }

    public int Company_ID { get; set; }

    public int Customer_Branch_ID { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string Customer_Branch_Code { get; set; }

    [Required]
    [StringLength(50)]
    public string Customer_Branch_Name { get; set; }

    [StringLength(50)]
    public string Customer_Branch_Name_Abbr { get; set; }

    [StringLength(20)]
    public string Tantou_Code { get; set; }

    [StringLength(60)]
    public string Tantou_Name { get; set; }

    [StringLength(20)]
    public string Tantou_Name_Abbr { get; set; }

    public int KokyakuTantouId { get; set; }

    [Required]
    [StringLength(50)]
    public string Senzoku_Name { get; set; }

    [Required]
    [StringLength(50)]
    public string Senzoku_Name_Abbr { get; set; }

    public int Haisya_Group_ID { get; set; }
}
