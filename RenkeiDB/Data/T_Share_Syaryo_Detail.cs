using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// 共有車両詳細情報を表すエンティティ
    /// </summary>
    [Table("T_Share_Syaryo_Detail")]
    public partial class T_Share_Syaryo_Detail
    {
        [Key]
        public int Share_Syaryo_ID { get; set; }
        [Key]
        public int Share_Syaryo_Order { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        public int Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Update_Datetime { get; set; }
        public int Update_User { get; set; }
        [Column(TypeName = "date")]
        public DateTime Empty_Car_Day { get; set; }
        [StringLength(10)]
        public string Empty_Post_code { get; set; }
        [StringLength(255)]
        public string Empty_Address { get; set; }
        [StringLength(255)]
        public string Empty_Address2 { get; set; }
        [StringLength(255)]
        public string Empty_Address3 { get; set; }
        [Required]
        [StringLength(10)]
        public string Dest_Post_code { get; set; }
        [StringLength(255)]
        public string Dest_Address { get; set; }
        [StringLength(255)]
        public string Dest_Address2 { get; set; }
        [StringLength(255)]
        public string Dest_Address3 { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
        [StringLength(4)]
        public string Syaban { get; set; }
        public int Syasyu { get; set; }
        [StringLength(50)]
        public string SyasyuDisplay { get; set; }
        public double Syaryo_Weight { get; set; }
        public double Syaryo_Total_Weight { get; set; }
        [StringLength(100)]
        public string EquipmentDisplay { get; set; }
        [StringLength(50)]
        public string Driver_Name { get; set; }
        [StringLength(30)]
        public string Cell_Phone { get; set; }
    }
}
