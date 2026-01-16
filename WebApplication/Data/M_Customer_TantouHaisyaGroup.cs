using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[PrimaryKey("Customer_ID", "Group_ID")]
[Table("M_Customer_TantouHaisyaGroup")]
public partial class M_Customer_TantouHaisyaGroup
{
    [Key]
    public int Customer_ID { get; set; }

    [Key]
    public int Group_ID { get; set; }

    [StringLength(255)]
    public string Remarks { get; set; }
}
