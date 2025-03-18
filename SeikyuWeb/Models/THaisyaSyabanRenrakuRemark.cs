using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 配車車番連絡備考情報を表すクラス
    /// </summary>
    public partial class THaisyaSyabanRenrakuRemark
    {
        /// <summary>
        /// 案件表示ID
        /// </summary>
        public int AnkenDisplayId { get; set; }
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
        public DateTime? UpdateDatetime { get; set; }
        /// <summary>
        /// 更新ユーザー
        /// </summary>
        public int? UpdateUser { get; set; }
    }
}
