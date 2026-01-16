using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("M_KojinUnsyu_Route")]
public partial class M_KojinUnsyu_Route
{
    [Key]
    public int KojinUnsyuRoute_ID { get; set; }

    public int Company_ID { get; set; }

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
