using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// 連携案件情報を表すエンティティ
    /// </summary>
    [Table("T_Renkei_Anken")]
    public partial class T_Renkei_Anken
    {
        [Key]
        public int Renkei_Anken_ID { get; set; }
        [Required]
        [StringLength(20)]
        public string Renkei_Anken_No { get; set; }
        public int Renkei_Anken_Status { get; set; }
        public int Renkei_Anken_Latest_Order { get; set; }
        public int Company_ID { get; set; }
        public int Branch_ID { get; set; }
        public int Renkei_Anken_Kubun { get; set; }
        public int SenzokuID { get; set; }
        public int Senzoku_Driver_ID { get; set; }
    }
}
