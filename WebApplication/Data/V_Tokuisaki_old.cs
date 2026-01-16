using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data;

[Keyless]
[Table("V_Tokuisaki_old")]
public partial class V_Tokuisaki_old
{
    [StringLength(50)]
    public string コード { get; set; }

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

    public byte 締日１ { get; set; }

    public byte 締日２ { get; set; }

    public byte 締日３ { get; set; }

    public byte 回収区分 { get; set; }

    public byte 回収サイト { get; set; }

    public byte 回収日１ { get; set; }

    public byte 回収日２ { get; set; }

    public byte 回収日３ { get; set; }

    public short 消費税計算区分 { get; set; }

    public byte 運賃計算区分 { get; set; }

    [Required]
    [StringLength(10)]
    public string 補助検索キー { get; set; }

    public byte 検索キー１ { get; set; }

    public byte 検索キー２ { get; set; }

    public byte 検索キー３ { get; set; }

    public byte 検索キー４ { get; set; }

    public byte 検索キー５ { get; set; }

    [Column(TypeName = "money")]
    public decimal 掛率 { get; set; }

    public byte 丸め区分 { get; set; }

    public byte 親子区分 { get; set; }

    public int 営業所コード { get; set; }

    public byte 削除フラグ { get; set; }

    public short 更新番号 { get; set; }
}
