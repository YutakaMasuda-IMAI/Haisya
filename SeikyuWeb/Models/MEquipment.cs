using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 装備を表します。
    /// </summary>
    public partial class MEquipment
    {
        /// <summary>
        /// 装備ID
        /// </summary>
        public int EquipmentId { get; set; }

        /// <summary>
        /// 装備グループID
        /// </summary>
        public int EquipmentGroupId { get; set; }

        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// ソート順
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 装備名
        /// </summary>
        public string EquipmentName { get; set; }

        /// <summary>
        /// 単位名
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DelFlg { get; set; }

        /// <summary>
        /// 登録日時
        /// </summary>
        public DateTime? InsertDatetime { get; set; }

        /// <summary>
        /// 登録ユーザー
        /// </summary>
        public int? InsertUser { get; set; }

        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime? UpdateDatetime { get; set; }

        /// <summary>
        /// 更新ユーザー
        /// </summary>
        public int? UpdateUser { get; set; }
    }
}
