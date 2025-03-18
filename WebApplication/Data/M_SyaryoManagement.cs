using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("M_SyaryoManagement")]
    public partial class M_SyaryoManagement
    {
        [Key]
        public int SyaryoManagement_ID { get; set; }
        public int Company_ID { get; set; }
        public int Branch_ID { get; set; }
        public int Tntou_ID { get; set; }
        public int Group_ID { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Ope_Date { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Scrap_Date { get; set; }
        public int? Syaryo_ID { get; set; }
        [StringLength(20)]
        public string Syasyu { get; set; }
        [StringLength(10)]
        public string Kata { get; set; }
        [StringLength(10)]
        public string Syaban_Chiiki { get; set; }
        [StringLength(10)]
        public string Syaban_Bunrui { get; set; }
        [StringLength(10)]
        public string Syaban_Kana { get; set; }
        [StringLength(10)]
        public string Syaban_Number { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Touroku_Date { get; set; }
        [StringLength(7)]
        public string FirstYear { get; set; }
        [StringLength(10)]
        public string Syamei { get; set; }
        [StringLength(30)]
        public string Syatai_Number { get; set; }
        [StringLength(30)]
        public string Syatai_Model { get; set; }
        [StringLength(10)]
        public string Engin_Model { get; set; }
        [StringLength(20)]
        public string Syatai_Shape { get; set; }
        [StringLength(20)]
        public string MaxLoadCapa { get; set; }
        [StringLength(50)]
        public string BaseEaseItem { get; set; }
        [StringLength(30)]
        public string Syaryo_Weight { get; set; }
        [StringLength(30)]
        public string Syaryo_Total_Weight { get; set; }
        [StringLength(10)]
        public string ETC { get; set; }
        [Column(TypeName = "money")]
        public decimal? Syaryo_Price { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
    }
}
