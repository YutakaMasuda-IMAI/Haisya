using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    /// <summary>
    /// 配車車両情報を表します。
    /// </summary>
    [Keyless]
    public partial class V_Haisya_Syaryo
    {
        /// <summary>
        /// ドライバーID
        /// </summary>
        public int Driver_ID { get; set; }
        
        /// <summary>
        /// 開始日
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime START_DATE { get; set; }
        
        /// <summary>
        /// 終了日
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime? END_DATE { get; set; }
        
        /// <summary>
        /// 車種
        /// </summary>
        [Required]
        [StringLength(20)]
        public string SYASYU { get; set; }
        
        /// <summary>
        /// 型
        /// </summary>
        [Required]
        [StringLength(20)]
        public string KATA { get; set; }
        
        /// <summary>
        /// 区分
        /// </summary>
        [StringLength(50)]
        public string KUBUN { get; set; }
        
        /// <summary>
        /// 担当ID
        /// </summary>
        public int Tntou_ID { get; set; }
        
        /// <summary>
        /// 車両ID
        /// </summary>
        public int Syaryo_ID { get; set; }
        
        /// <summary>
        /// トレーラー車両ID
        /// </summary>
        public int Syaryo_ID_Trailer { get; set; }
        
        /// <summary>
        /// 会社ID
        /// </summary>
        public int Company_ID { get; set; }
    }
}
