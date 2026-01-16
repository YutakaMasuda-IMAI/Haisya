using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Table("TEMP_CUSTOMER")]
public partial class TEMP_CUSTOMER
{
    [Key]
    [StringLength(8)]
    public string コード { get; set; }

    [StringLength(8)]
    public string コード1 { get; set; }

    [Required]
    [StringLength(40)]
    public string 社名 { get; set; }

    [Required]
    [StringLength(16)]
    public string 検索カナ { get; set; }

    [Required]
    [StringLength(20)]
    public string 略称 { get; set; }

    [Required]
    [StringLength(10)]
    public string 郵便番号 { get; set; }

    [Required]
    [StringLength(40)]
    public string 住所１ { get; set; }

    [Required]
    [StringLength(40)]
    public string 住所２ { get; set; }

    [Required]
    [StringLength(13)]
    public string 電話番号 { get; set; }

    [Required]
    [StringLength(13)]
    public string FAX番号 { get; set; }
}
