using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Kintai;

[Table("T_ALC_CHECK_IMPORT")]
public partial class T_ALC_CHECK_IMPORT
{
    [Key]
    public int ID { get; set; }

    [StringLength(1)]
    public string NO { get; set; }

    [StringLength(6)]
    public string 月 { get; set; }

    [StringLength(8)]
    [Unicode(false)]
    public string CODE { get; set; }

    [StringLength(8)]
    public string DUMMY1 { get; set; }

    [StringLength(255)]
    public string NAME { get; set; }

    [StringLength(5)]
    public string ALC { get; set; }

    [StringLength(11)]
    public string DUMMY2 { get; set; }

    [StringLength(15)]
    public string DATETIME { get; set; }

    [StringLength(50)]
    public string DUMMY3 { get; set; }

    [StringLength(4)]
    public string FUMEI1 { get; set; }

    [StringLength(4)]
    public string DUMMY4 { get; set; }

    [StringLength(1)]
    public string STATUS { get; set; }

    [StringLength(247)]
    public string DUMMY5 { get; set; }
}
