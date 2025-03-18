using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 会社ユーザーグループを表すクラス
    /// </summary>
    public partial class MCompanyUserGroup
    {
        /// <summary>
        /// グループID
        /// </summary>
        public int GroupId { get; set; }
        
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        
        /// <summary>
        /// グループ区分
        /// </summary>
        public int GroupKubun { get; set; }
        
        /// <summary>
        /// ソート順
        /// </summary>
        public int SortOrder { get; set; }
        
        /// <summary>
        /// グループ名
        /// </summary>
        public string GroupName { get; set; }
        
        /// <summary>
        /// 表示名
        /// </summary>
        public string DisplayName { get; set; }
        
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DelFlg { get; set; }
        
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks { get; set; }
        
        /// <summary>
        /// 更新日
        /// </summary>
        public DateTime? UpDate { get; set; }
        
        /// <summary>
        /// 電話番号
        /// </summary>
        public string Phone { get; set; }
    }
}
