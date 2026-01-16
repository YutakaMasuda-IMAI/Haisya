using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("M_PublishGroup")]
public partial class M_PublishGroup
{
    [Key]
    public int PublishGroup_ID { get; set; }

    [Required]
    [StringLength(50)]
    public string Publish_Group_Name { get; set; }

    public int? User_ID { get; set; }

    public int? Company_ID { get; set; }

    public int? Branch_ID { get; set; }

    public bool DEL_FLG { get; set; }
}
