using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// ポイント情報を表すエンティティ
    /// </summary>
    [Table("T_Point")]
    public partial class T_Point
    {
        [Key]
        public int Point_ID { get; set; }
        public int User_ID { get; set; }
        public int Group_ID { get; set; }
        [Required]
        [StringLength(100)]
        public string BuildingZid { get; set; }
        [StringLength(100)]
        public string BuildingZid_Attr { get; set; }
        [StringLength(255)]
        public string BuildingName { get; set; }
        [StringLength(50)]
        public string BuildingNameRead { get; set; }
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
        [StringLength(10)]
        public string Post_code { get; set; }
        [StringLength(255)]
        public string Address2 { get; set; }
        [StringLength(255)]
        public string Address3 { get; set; }
        [StringLength(255)]
        public string Address4 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Insert_Datetime { get; set; }
        public int? Insert_User { get; set; }
    }
}
