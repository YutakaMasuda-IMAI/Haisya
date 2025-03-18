using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    /// <summary>
    /// 予社情報を表すクラス
    /// </summary>
    [Keyless]
    public partial class V_Yosya
    {
        /// <summary>
        /// コード
        /// </summary>
        [StringLength(50)]
        public string コード { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(40)]
        public string 名称 { get; set; }

        /// <summary>
        /// 検索カナ
        /// </summary>
        [Required]
        [StringLength(16)]
        public string 検索カナ { get; set; }

        /// <summary>
        /// 略称
        /// </summary>
        [Required]
        [StringLength(20)]
        public string 略称 { get; set; }

        /// <summary>
        /// 郵便番号
        /// </summary>
        [Required]
        [StringLength(10)]
        public string 郵便番号 { get; set; }

        /// <summary>
        /// 住所1
        /// </summary>
        [Required]
        [StringLength(40)]
        public string 住所1 { get; set; }

        /// <summary>
        /// 住所2
        /// </summary>
        [Required]
        [StringLength(40)]
        public string 住所2 { get; set; }

        /// <summary>
        /// 電話番号
        /// </summary>
        [Required]
        [StringLength(13)]
        public string 電話番号 { get; set; }

        /// <summary>
        /// FAX番号
        /// </summary>
        [Required]
        [StringLength(13)]
        public string FAX番号 { get; set; }

        /// <summary>
        /// 締日1
        /// </summary>
        public byte 締日１ { get; set; }

        /// <summary>
        /// 締日2
        /// </summary>
        public byte 締日２ { get; set; }

        /// <summary>
        /// 締日3
        /// </summary>
        public byte 締日３ { get; set; }

        /// <summary>
        /// 支払区分
        /// </summary>
        public byte 支払区分 { get; set; }

        /// <summary>
        /// 支払サイト
        /// </summary>
        public byte 支払サイト { get; set; }

        /// <summary>
        /// 支払日1
        /// </summary>
        public byte 支払日１ { get; set; }

        /// <summary>
        /// 支払日2
        /// </summary>
        public byte 支払日２ { get; set; }

        /// <summary>
        /// 支払日3
        /// </summary>
        public byte 支払日３ { get; set; }

        /// <summary>
        /// 消費税計算区分
        /// </summary>
        public short 消費税計算区分 { get; set; }

        /// <summary>
        /// 補助検索キー
        /// </summary>
        [Required]
        [StringLength(10)]
        public string 補助検索キー { get; set; }

        /// <summary>
        /// 検索キー1
        /// </summary>
        public byte 検索キー１ { get; set; }

        /// <summary>
        /// 検索キー2
        /// </summary>
        public byte 検索キー２ { get; set; }

        /// <summary>
        /// 検索キー3
        /// </summary>
        public byte 検索キー３ { get; set; }

        /// <summary>
        /// 検索キー4
        /// </summary>
        public byte 検索キー４ { get; set; }

        /// <summary>
        /// 検索キー5
        /// </summary>
        public byte 検索キー５ { get; set; }

        /// <summary>
        /// 掛率
        /// </summary>
        [Column(TypeName = "money")]
        public decimal 掛率 { get; set; }

        /// <summary>
        /// 丸め区分
        /// </summary>
        public byte 丸め区分 { get; set; }

        /// <summary>
        /// 親子区分
        /// </summary>
        public byte 親子区分 { get; set; }

        /// <summary>
        /// 削除フラグ
        /// </summary>
        public byte 削除フラグ { get; set; }

        /// <summary>
        /// 更新番号
        /// </summary>
        public short 更新番号 { get; set; }
    }
}
