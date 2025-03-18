using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Nippou_Toll")]
    public partial class T_Nippou_Toll
    {
        [Key]
        public int Nippou_Toll_ID { get; set; }
        public int Nippou_ID { get; set; }
        public int Sort { get; set; }
        public int DriverSyaryo_ID { get; set; }
        public int Driver_ID { get; set; }
        public int YosyaDriverSyaryo_ID { get; set; }
        public int YosyaDriver_ID { get; set; }
        public int Yosya_Branch_ID { get; set; }
        [StringLength(25)]
        public string 運行NO { get; set; }
        [Column(TypeName = "date")]
        public DateTime? 読取日 { get; set; }
        public int? 事業所CD { get; set; }
        [Column(TypeName = "date")]
        public DateTime 運行日 { get; set; }
        [StringLength(50)]
        public string 事業所名 { get; set; }
        public int? 車輌CD { get; set; }
        [StringLength(50)]
        public string 車輌名 { get; set; }
        public int? 乗務員CD { get; set; }
        [StringLength(50)]
        public string 乗務員名 { get; set; }
        public int? 対象乗務員区分 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 開始日時 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 終了日時 { get; set; }
        [StringLength(20)]
        public string 開始道路番号 { get; set; }
        [StringLength(50)]
        public string 開始道路名 { get; set; }
        [StringLength(50)]
        public string 開始ETC番号 { get; set; }
        [Required]
        [StringLength(50)]
        public string 開始IC名 { get; set; }
        [StringLength(20)]
        public string 終了道路番号 { get; set; }
        [StringLength(50)]
        public string 終了道路名 { get; set; }
        [StringLength(50)]
        public string 終了ETC番号 { get; set; }
        [Required]
        [StringLength(50)]
        public string 終了IC名 { get; set; }
        public int? 精算区分 { get; set; }
        [StringLength(50)]
        public string 精算区分名 { get; set; }
        [Column(TypeName = "money")]
        public decimal? 料金 { get; set; }
        public double? 走行距離 { get; set; }
        public int? 高速車種区分 { get; set; }
        [StringLength(50)]
        public string 高速車種区分名 { get; set; }
        [Column(TypeName = "money")]
        public decimal? 標準料金 { get; set; }
        public int? 料金区分 { get; set; }
        [StringLength(50)]
        public string 料金区分名 { get; set; }
        public int Futan_Kubun { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Insert_Datetime { get; set; }
        public int? Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Update_Datetime { get; set; }
        public int? Update_User { get; set; }
    }
}
