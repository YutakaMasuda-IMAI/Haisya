using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// 共有荷物詳細情報を表すエンティティ
    /// </summary>
    [Table("T_Share_Luggage_Detail")]
    public partial class T_Share_Luggage_Detail
    {
        [Key]
        public int Share_Luggage_ID { get; set; }
        [Key]
        public int Share_Luggage_Order { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        public int Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Update_Datetime { get; set; }
        public int Update_User { get; set; }
        [StringLength(100)]
        public string KokyakuName { get; set; }
        public int Kokyaku_Public_Flg { get; set; }
        [StringLength(100)]
        public string Prime_Contractor { get; set; }
        [Column(TypeName = "money")]
        public decimal Unchin { get; set; }
        public int Toll_Kubun { get; set; }
        [Column(TypeName = "money")]
        public decimal Toll_Money { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Tumi_Datetime { get; set; }
        public int Tumi_TimeKubun { get; set; }
        public int Tumi_StatusKubun { get; set; }
        [StringLength(10)]
        public string Tumi_Post_code { get; set; }
        [StringLength(255)]
        public string Tumi_Address { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Oroshi_Datetime { get; set; }
        public int Oroshi_TimeKubun { get; set; }
        public int Oroshi_StatusKubun { get; set; }
        [StringLength(10)]
        public string Oroshi_Post_code { get; set; }
        [StringLength(255)]
        public string Oroshi_Address { get; set; }
        public double Luggage_Weight { get; set; }
        public int Syasyu { get; set; }
        [StringLength(50)]
        public string SyasyuDisplay { get; set; }
        [StringLength(100)]
        public string LuggageDisplay { get; set; }
        [StringLength(100)]
        public string EquipmentDisplay { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
    }
}
