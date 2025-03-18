using System.Collections.Generic;
using WebApplication.Data;

#nullable disable

namespace WebApplication.Model
{
    /// <summary>
    /// 車番連絡後モデル
    /// </summary>
    public class SyabanRenrakuPostModel
    {
        /// <summary>
        /// 車番連絡案件
        /// </summary>
        public T_Anken_SyabanRenraku AnkenSyabanRenraku { get; set; }

        /// <summary>
        /// 車番連絡配車
        /// </summary>
        public T_Haisya_SyabanRenraku HaisyaSyabanRenraku { get; set; }

        /// <summary>
        /// 車番連絡配車詳細
        /// </summary>

        public T_Haisya_SyabanRenraku_Detail HaisyaSyabanRenrakuDetail { get; set; }

        /// <summary>
        /// 備考一覧
        /// </summary>
        public List<string> RemarksList { get; set; }

    }
}

