using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Kintai;

[Keyless]
[Table("ALC_TEMP")]
public partial class ALC_TEMP
{
    [StringLength(8)]
    [Unicode(false)]
    public string CODE { get; set; }

    [StringLength(8)]
    [Unicode(false)]
    public string DUMMY1 { get; set; }

    [StringLength(128)]
    [Unicode(false)]
    public string NAME { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string ALC { get; set; }

    [StringLength(11)]
    [Unicode(false)]
    public string DUMMY2 { get; set; }

    [StringLength(14)]
    [Unicode(false)]
    public string DATETIME { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string DUMMY3 { get; set; }

    [StringLength(4)]
    [Unicode(false)]
    public string FUMEI1 { get; set; }

    [StringLength(4)]
    [Unicode(false)]
    public string DUMMY4 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string STATUS { get; set; }

    [StringLength(247)]
    [Unicode(false)]
    public string DUMMY5 { get; set; }

    [StringLength(6)]
    public string 月 { get; set; }

    public int ID { get; set; }
}
