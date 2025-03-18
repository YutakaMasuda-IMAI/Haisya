using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 案件に関する情報を表します。
    /// </summary>
    public partial class TAnken
    {
        /// <summary>
        /// 案件ID
        /// </summary>
        public int AnkenId { get; set; }
        /// <summary>
        /// 案件番号
        /// </summary>
        public string AnkenNo { get; set; }
        /// <summary>
        /// 案件ステータス
        /// </summary>
        public int AnkenStatus { get; set; }
        /// <summary>
        /// 最新の案件順序
        /// </summary>
        public int AnkenLatestOrder { get; set; }
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// 支店ID
        /// </summary>
        public int BranchId { get; set; }
        /// <summary>
        /// 案件区分
        /// </summary>
        public int AnkenKubun { get; set; }
        /// <summary>
        /// 専属ID
        /// </summary>
        public int SenzokuId { get; set; }
        /// <summary>
        /// 専属ドライバーID
        /// </summary>
        public int SenzokuDriverId { get; set; }
    }
}
