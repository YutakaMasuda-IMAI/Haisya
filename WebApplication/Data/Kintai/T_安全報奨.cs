using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data.Kintai
{
    [Table("T_安全報奨")]
    public partial class T_安全報奨
    {
        [Key]
        public int ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 支給年月 { get; set; }
        public int? 乗務員コード { get; set; }
        [StringLength(255)]
        public string 氏名 { get; set; }
        [Column(TypeName = "money")]
        public decimal? 安全報奨_1 { get; set; }
        [Column(TypeName = "money")]
        public decimal? 安全報奨_2 { get; set; }
        [Column(TypeName = "money")]
        public decimal? 安全報奨_3 { get; set; }
        [Column(TypeName = "money")]
        public decimal? 安全報奨_4 { get; set; }
        [Column(TypeName = "money")]
        public decimal? 安全報奨_5 { get; set; }
        [Column(TypeName = "money")]
        public decimal? 安全報奨_6 { get; set; }
        [Column(TypeName = "money")]
        public decimal? 安全報奨_計 { get; set; }
        public double? 評価点_1 { get; set; }
        public double? 評価点_2 { get; set; }
        public double? 評価点_3 { get; set; }
        public double? 評価点_4 { get; set; }
        public double? 評価点_5 { get; set; }
        public double? 評価点_6 { get; set; }
        [StringLength(20)]
        public string 事故の有無_1 { get; set; }
        [StringLength(20)]
        public string 事故の有無_2 { get; set; }
        [StringLength(20)]
        public string 事故の有無_3 { get; set; }
        [StringLength(20)]
        public string 事故の有無_4 { get; set; }
        [StringLength(20)]
        public string 事故の有無_5 { get; set; }
        [StringLength(20)]
        public string 事故の有無_6 { get; set; }
    }
}
