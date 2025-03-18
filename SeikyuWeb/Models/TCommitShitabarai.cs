using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// TCommitShitabarai クラス
    /// </summary>
    public partial class TCommitShitabarai
    {
        /// <summary>
        /// 支払コミットID
        /// </summary>
        public int ShitabaraiCommitId { get; set; }

        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 顧客支店ID
        /// </summary>
        public int CustomerBranchId { get; set; }

        /// <summary>
        /// 税区分
        /// </summary>
        public int ZeiKubun { get; set; }

        /// <summary>
        /// 月
        /// </summary>
        public DateTime Month { get; set; }

        /// <summary>
        /// 締め日
        /// </summary>
        public int ShimeDay { get; set; }

        /// <summary>
        /// 削除日時
        /// </summary>
        public DateTime? DelDatetime { get; set; }

        /// <summary>
        /// 締め日時
        /// </summary>
        public DateTime ShimeDatetime { get; set; }

        /// <summary>
        /// 挿入日時
        /// </summary>
        public DateTime InsertDatetime { get; set; }

        /// <summary>
        /// 挿入ユーザー
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
