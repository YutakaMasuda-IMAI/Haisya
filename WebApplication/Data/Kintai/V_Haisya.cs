using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data.Kintai;

[Keyless]
public partial class V_Haisya
{
    public int HAISYA_ID { get; set; }

    [Required]
    [StringLength(100)]
    public string HAISYA_NAME { get; set; }

    [StringLength(100)]
    public string HAISYA_NAME_SUB { get; set; }

    public bool YUSOUBU { get; set; }

    public bool AREA { get; set; }

    public int? TRACMEITO { get; set; }

    [Required]
    [StringLength(100)]
    public string HAISYA_DISPLAY { get; set; }

    public bool SOUKO { get; set; }

    [StringLength(20)]
    public string TRACMEITO_NM { get; set; }

    public int? TRACMEITO_ID { get; set; }
}
