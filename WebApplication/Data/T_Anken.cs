using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("T_Anken")]
[Index("Anken_No", Name = "IX_T_Anken", IsUnique = true)]
public partial class T_Anken
{
    [Key]
    public int Anken_ID { get; set; }

    [Required]
    [StringLength(20)]
    [Unicode(false)]
    public string Anken_No { get; set; }

    /// <summary>
    /// 0:確定,1:暫定(配車必要),2:暫定(配車不要)
    /// </summary>
    public int Anken_Status { get; set; }

    public int Anken_Latest_Order { get; set; }

    public int Company_ID { get; set; }

    public int Branch_ID { get; set; }

    /// <summary>
    /// 0:自動車運送,1:自動車運送(過去),2:利用運送,3利用運送(過去),4:専属
    /// </summary>
    public int Anken_Kubun { get; set; }

    public int SenzokuID { get; set; }

    public int Senzoku_Driver_ID { get; set; }
}
