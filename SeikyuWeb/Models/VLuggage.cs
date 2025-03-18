using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 荷物情報を表すクラス
    /// </summary>
    public partial class VLuggage
    {
        /// <summary>
        /// 荷物グループID
        /// </summary>
        public int LuggageGroupId { get; set; }
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// グループソート順
        /// </summary>
        public int GroupSortOrder { get; set; }
        /// <summary>
        /// 荷物グループ名
        /// </summary>
        public string LuggageGroupName { get; set; }
        /// <summary>
        /// グループ備考
        /// </summary>
        public string GroupRemarks { get; set; }
        /// <summary>
        /// グループ削除フラグ
        /// </summary>
        public bool GroupDelFlg { get; set; }
        /// <summary>
        /// 荷物ID
        /// </summary>
        public int LuggageId { get; set; }
        /// <summary>
        /// ソート順
        /// </summary>
        public int SortOrder { get; set; }
        /// <summary>
        /// 荷物名
        /// </summary>
        public string LuggageName { get; set; }
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
    }
}
