using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// TCommitUnsyu クラス
    /// </summary>
    public partial class TCommitUnsyu
    {
        /// <summary>
        /// コミット運輸ID
        /// </summary>
        public int CommitUnsyuId { get; set; }

        /// <summary>
        /// 売上運輸ID
        /// </summary>
        public int UriageUnsyuId { get; set; }

        /// <summary>
        /// 締め日時
        /// </summary>
        public DateTime ShimeDatetime { get; set; }

        /// <summary>
        /// 請求月
        /// </summary>
        public DateTime SeikyuMonth { get; set; }

        /// <summary>
        /// 締め日
        /// </summary>
        public int ShimeDay { get; set; }

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
