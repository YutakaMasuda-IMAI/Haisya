using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("M_PublishGroup_Detail")]
public partial class M_PublishGroup_Detail
{
    [Key]
    public int PublishGroup_Detail_ID { get; set; }

    public int PublishGroup_ID { get; set; }

    public int? Comany_ID { get; set; }

    public int? Branch_ID { get; set; }
}
