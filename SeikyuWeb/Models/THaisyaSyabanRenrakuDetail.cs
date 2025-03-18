using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 配車車番連絡詳細情報を表すクラス
    /// </summary>
    public partial class THaisyaSyabanRenrakuDetail
    {
        /// <summary>
        /// 車番連絡ID
        /// </summary>
        public int SyabanRenrakuId { get; set; }
        /// <summary>
        /// 案件ID
        /// </summary>
        public int AnkenId { get; set; }
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks { get; set; }
    }
}
