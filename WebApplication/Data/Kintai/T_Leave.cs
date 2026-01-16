using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Kintai;

[Table("T_Leave")]
public partial class T_Leave
{
    [Key]
    public int Leave_ID { get; set; }

    public int 乗務員CD { get; set; }

    public DateOnly Day { get; set; }

    public int LEAVE_CD { get; set; }

    public int REASON_CD { get; set; }

    public double Days { get; set; }

    [StringLength(255)]
    public string Remarks { get; set; }

    public bool ApprovalFlg { get; set; }

    public int LeaveApplication_ID { get; set; }

    public int Kintai_ID { get; set; }
}
