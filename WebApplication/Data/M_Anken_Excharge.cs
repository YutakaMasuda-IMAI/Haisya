using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("M_Anken_Excharge")]
[Index("Company_ID", "SIZE", "Komoku_Key", Name = "IX_M_Anken_Excharge_1", IsUnique = true)]
public partial class M_Anken_Excharge
{
    [Key]
    public int Komoku_ID { get; set; }

    public int Company_ID { get; set; }

    [Required]
    [StringLength(20)]
    public string SIZE { get; set; }

    [Required]
    [StringLength(20)]
    public string Komoku_Key { get; set; }

    public int SortOrder { get; set; }

    [Required]
    [StringLength(50)]
    public string Komoku_Name { get; set; }

    [StringLength(50)]
    public string Komoku_Name_abbr { get; set; }

    [Column(TypeName = "money")]
    public decimal StdExcharge { get; set; }

    [Column(TypeName = "money")]
    public decimal GrossExcharge { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UP_DATE { get; set; }

    public bool HIDDEN_FLG { get; set; }

    public bool DEL_FLG { get; set; }
}
