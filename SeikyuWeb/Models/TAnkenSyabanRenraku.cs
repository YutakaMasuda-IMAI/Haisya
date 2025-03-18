using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 案件車番連絡を表すクラス
    /// </summary>
    public partial class TAnkenSyabanRenraku
    {
        /// <summary>
        /// 案件ID
        /// </summary>
        public int AnkenId { get; set; }

        /// <summary>
        /// 連絡区分
        /// </summary>
        public int RenrakuKubun { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks { get; set; }
    }
}
