using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 案件の設備に関する情報を表します。
    /// </summary>
    public partial class TAnkenEquipment
    {
        /// <summary>
        /// 案件ID
        /// </summary>
        public int AnkenId { get; set; }
        /// <summary>
        /// 案件の順序
        /// </summary>
        public int AnkenOrder { get; set; }
        /// <summary>
        /// 設備ID
        /// </summary>
        public int EquipmentId { get; set; }
        /// <summary>
        /// 設備の数量
        /// </summary>
        public double? EquipmentCount { get; set; }
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 登録日時
        /// </summary>
        public DateTime InsertDatetime { get; set; }
        /// <summary>
        /// 登録ユーザー
        /// </summary>
        public int InsertUser { get; set; }
        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime UpdateDatetime { get; set; }
        /// <summary>
        /// 更新ユーザー
        /// </summary>
        public int UpdateUser { get; set; }
    }
}
