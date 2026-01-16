using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("M_Luggage")]
[Index("Company_ID", "Luggage_Group_ID", "Luggage_Name", Name = "IX_M_Luggage", IsUnique = true)]
public partial class M_Luggage
{
    [Key]
    public int Luggage_ID { get; set; }

    public int Luggage_Group_ID { get; set; }

    public int Company_ID { get; set; }

    public int SortOrder { get; set; }

    [Required]
    [StringLength(50)]
    public string Luggage_Name { get; set; }

    [StringLength(50)]
    public string Unit_Name { get; set; }

    [StringLength(50)]
    public string Remarks { get; set; }

    public bool Del_Flg { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Insert_Datetime { get; set; }

    public int? Insert_User { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Update_Datetime { get; set; }

    public int? Update_User { get; set; }
}
