using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// 連携案件確保情報を表すエンティティ
    /// </summary>
    [Table("T_Renkei_Anken_Secure")]
    public partial class T_Renkei_Anken_Secure
    {
        [Key]
        public int Renkei_Anken_Secure_ID { get; set; }
        public int Renkei_Anken_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        public int Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Update_Datetime { get; set; }
        public int Update_User { get; set; }
        public int Renkei_Anken_Secure_Status { get; set; }
        public int Driver_ID { get; set; }
        [StringLength(50)]
        public string Last_Name { get; set; }
        [StringLength(50)]
        public string First_Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Display_Name { get; set; }
        [StringLength(30)]
        public string Phone1 { get; set; }
        [StringLength(30)]
        public string Phone2 { get; set; }
        [StringLength(20)]
        public string LineID { get; set; }
        public int SyaryoManagement_ID { get; set; }
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
        [Column(TypeName = "datetime")]
        public DateTime? Del_Datetime { get; set; }
        [Column(TypeName = "money")]
        public decimal Del_Kubun { get; set; }
    }
}
