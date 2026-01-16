using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("T_Haisya_Around")]
public partial class T_Haisya_Around
{
    [Key]
    public int HaisyaAround_ID { get; set; }

    public int Company_ID { get; set; }

    public DateOnly Day { get; set; }

    public int Anken_ID { get; set; }

    public int Haisya_ID { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string SEKubun { get; set; }

    [StringLength(255)]
    public string Address { get; set; }

    [StringLength(255)]
    public string Address_Code { get; set; }

    [StringLength(10)]
    public string Address_Level { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Lng { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Lat { get; set; }

    [StringLength(255)]
    public string BuildingName { get; set; }

    [StringLength(100)]
    public string BuildingZid { get; set; }

    [StringLength(100)]
    public string BuildingZid_Attr { get; set; }

    [StringLength(50)]
    public string BuildingNameRead { get; set; }

    [StringLength(50)]
    public string Point_KoumokuTitle { get; set; }

    [StringLength(10)]
    public string Point_Type { get; set; }

    [StringLength(50)]
    public string PointName { get; set; }

    public DateOnly? PointDate { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string PointTime { get; set; }

    public int? PointTimeKubun { get; set; }

    public int? PointStatusKubun { get; set; }

    public bool? FlgGenchiKakunin { get; set; }

    [StringLength(20)]
    public string TollDisplay { get; set; }

    public double? TollDisplayHeight { get; set; }

    [StringLength(10)]
    public string Post_code { get; set; }

    [StringLength(255)]
    public string Address2 { get; set; }

    [StringLength(255)]
    public string Address3 { get; set; }

    [StringLength(255)]
    public string Address4 { get; set; }
}
