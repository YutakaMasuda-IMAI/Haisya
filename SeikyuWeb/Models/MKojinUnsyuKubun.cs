using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 個人運輸区分を表します。
    /// </summary>
    public partial class MKojinUnsyuKubun
    {
        /// <summary>
        /// 個人運輸区分ID
        /// </summary>
        public int KojinUnsyuKubunId { get; set; }

        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 区分ソート順
        /// </summary>
        public int KubunSort { get; set; }

        /// <summary>
        /// 区分名
        /// </summary>
        public string KubunName { get; set; }

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
