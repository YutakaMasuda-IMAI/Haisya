using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// 共有荷物通知設定情報を表すエンティティ
    /// </summary>
    [Table("T_Share_Luggage_Notify_Setting")]
    public partial class T_Share_Luggage_Notify_Setting
    {
        [Key]
        public int Share_Luggage_Notify_Setting_ID { get; set; }
        public int Company_ID { get; set; }
        public int Branch_ID { get; set; }
        public int Tantou_Group_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        public int Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Update_Datetime { get; set; }
        public int Update_User { get; set; }
        public int Notify_Flg { get; set; }
        public int Meil_Flg { get; set; }
        [StringLength(50)]
        public string Mail_Address { get; set; }
        [Column(TypeName = "date")]
        public DateTime? From_Date { get; set; }
        [Column(TypeName = "date")]
        public DateTime? To_Date { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Tumi_From_Date { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Tumi_To_Date { get; set; }
        public int Tumi_Flg { get; set; }
        [StringLength(20)]
        public string Tumi1 { get; set; }
        [StringLength(20)]
        public string Tumi2 { get; set; }
        [StringLength(20)]
        public string Tumi3 { get; set; }
        public int Oroshi_Flg { get; set; }
        [StringLength(20)]
        public string Oroshi1 { get; set; }
        [StringLength(20)]
        public string Oroshi2 { get; set; }
        [StringLength(20)]
        public string Oroshi3 { get; set; }
        public int Syasuy_Flg { get; set; }
        [StringLength(20)]
        public string Syasuy { get; set; }
        [StringLength(30)]
        public string SyasuyDisplay { get; set; }
    }
}
