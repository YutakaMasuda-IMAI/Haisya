using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[PrimaryKey("Anken_ID", "Anken_Order", "Point_Order")]
[Table("T_Anken_Riyounso_Point")]
public partial class T_Anken_Riyounso_Point
{
    [Key]
    public int Anken_ID { get; set; }

    [Key]
    public int Anken_Order { get; set; }

    [Key]
    public int Point_Order { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Insert_Datetime { get; set; }

    public int? Insert_User { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Update_Datetime { get; set; }

    public int? Update_User { get; set; }

    public DateOnly? From_Date { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string From_Time { get; set; }

    [StringLength(50)]
    public string From_Display { get; set; }

    [StringLength(255)]
    public string From_Address { get; set; }

    [StringLength(255)]
    public string From_Address_Code { get; set; }

    [StringLength(10)]
    public string From_Address_Level { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string From_Lng { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string From_Lat { get; set; }

    [StringLength(255)]
    public string From_BuildingName { get; set; }

    [StringLength(100)]
    public string From_BuildingZid { get; set; }

    [StringLength(100)]
    public string From_BuildingZid_Attr { get; set; }

    public DateOnly? To_Date { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string To_Time { get; set; }

    [StringLength(50)]
    public string To_Display { get; set; }

    [StringLength(255)]
    public string To_Address { get; set; }

    [StringLength(255)]
    public string To_Address_Code { get; set; }

    [StringLength(10)]
    public string To_Address_Level { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string To_Lng { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string To_Lat { get; set; }

    [StringLength(255)]
    public string To_BuildingName { get; set; }

    [StringLength(100)]
    public string To_BuildingZid { get; set; }

    [StringLength(100)]
    public string To_BuildingZid_Attr { get; set; }

    [StringLength(50)]
    public string Luggage { get; set; }
}
