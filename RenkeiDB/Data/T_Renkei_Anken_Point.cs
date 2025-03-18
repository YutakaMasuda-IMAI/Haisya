using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// 連携案件ポイント情報を表すエンティティ
    /// </summary>
    [Table("T_Renkei_Anken_Point")]
    public partial class T_Renkei_Anken_Point
    {
        [Key]
        public int Renkei_Anken_ID { get; set; }
        [Key]
        public int Renkei_Anken_Order { get; set; }
        [Key]
        public int Kubun { get; set; }
        [Key]
        public int Point_Order { get; set; }
        [StringLength(10)]
        public string SEKubun { get; set; }
        [StringLength(255)]
        public string Address { get; set; }
        [StringLength(255)]
        public string Address_Code { get; set; }
        [StringLength(10)]
        public string Address_Level { get; set; }
        [StringLength(100)]
        public string Lng { get; set; }
        [StringLength(100)]
        public string Lat { get; set; }
        [StringLength(255)]
        public string BuildingName { get; set; }
        [StringLength(100)]
        public string BuildingZid { get; set; }
        [StringLength(100)]
        public string BuildingZid_Attr { get; set; }
        [StringLength(50)]
        public string BuildingNameRead { get; set; }
        [StringLength(50)]
        public string Point_KoumokuTitle { get; set; }
        [StringLength(10)]
        public string Point_Type { get; set; }
        [StringLength(50)]
        public string PointName { get; set; }
        [Column(TypeName = "date")]
        public DateTime? PointDate { get; set; }
        [StringLength(5)]
        public string PointTime { get; set; }
        public int? PointTimeKubun { get; set; }
        public int? PointStatusKubun { get; set; }
        public bool? FlgGenchiKakunin { get; set; }
        [StringLength(20)]
        public string TollDisplay { get; set; }
        public double? TollDisplayHeight { get; set; }
        [StringLength(10)]
        public string Post_code { get; set; }
        [StringLength(255)]
        public string Address2 { get; set; }
        [StringLength(255)]
        public string Address3 { get; set; }
        [StringLength(255)]
        public string Address4 { get; set; }
        [StringLength(10)]
        public string RoadType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Insert_Datetime { get; set; }
        public int? Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Update_Datetime { get; set; }
        public int? Update_User { get; set; }
    }
}
