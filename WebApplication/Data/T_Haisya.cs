using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("T_Haisya")]
[Index("AnkenDisplay_ID", Name = "IX_T_Haisya", IsUnique = true)]
public partial class T_Haisya
{
    [Key]
    public int Haisya_ID { get; set; }

    public int AnkenDisplay_ID { get; set; }

    public int Company_ID { get; set; }

    public int Anken_ID { get; set; }

    public DateOnly Day { get; set; }

    /// <summary>
    /// 1：自車、2：傭車、3：専属傭車、4：自車専属、5：自車専任
    /// </summary>
    public int Haisya_Kubun { get; set; }

    public int Driver_ID { get; set; }

    public int DriverSyaryo_ID { get; set; }

    public int SyaryoManagement_ID { get; set; }

    public int? SyaryoManagement_ID1 { get; set; }

    public int KUBUN { get; set; }

    [StringLength(255)]
    public string Remarks { get; set; }

    /// <summary>
    /// 1:暫定、0：確定
    /// </summary>
    public int Haisya_Status { get; set; }

    [Column(TypeName = "money")]
    public decimal Route_Teate { get; set; }

    [Column(TypeName = "money")]
    public decimal Route_OverTime { get; set; }

    [Column(TypeName = "money")]
    public decimal Route_Midnight { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Insert_Datetime { get; set; }

    public int Insert_User { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Update_Datetime { get; set; }

    public int Update_User { get; set; }
}
