using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// 共有車両通知設定情報を表すエンティティ
    /// </summary>
    [Table("T_Share_Syaryo_Notify_Setting")]
    public partial class T_Share_Syaryo_Notify_Setting
    {
        [Key]
        public int Share_Syaryo_Notify_Setting_ID { get; set; }
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
        public DateTime? Empty_From_Date { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Empty_To_Date { get; set; }
        public int Empty_Flg { get; set; }
        [StringLength(20)]
        public string Empty1 { get; set; }
        [StringLength(20)]
        public string Empty2 { get; set; }
        [StringLength(20)]
        public string Empty3 { get; set; }
        public int Dest_Flg { get; set; }
        [StringLength(20)]
        public string Dest1 { get; set; }
        [StringLength(20)]
        public string Dest2 { get; set; }
        [StringLength(20)]
        public string Dest3 { get; set; }
        public int Syasuy_Flg { get; set; }
        [StringLength(20)]
        public string Syasuy { get; set; }
        [StringLength(30)]
        public string SyasuyDisplay { get; set; }
    }
}
