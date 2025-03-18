using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 顧客IC請求区分を表すクラス
    /// </summary>
    public partial class MCustomerIcseikyuKubun
    {
        /// <summary>
        /// 顧客支店ID
        /// </summary>
        public int CustomerBranchId { get; set; }
        
        /// <summary>
        /// ソート順
        /// </summary>
        public int Sort { get; set; }
        
        /// <summary>
        /// 選択区分
        /// </summary>
        public int SelectKubun { get; set; }
        
        /// <summary>
        /// IC1
        /// </summary>
        public string Ic1 { get; set; }
        
        /// <summary>
        /// IC2
        /// </summary>
        public string Ic2 { get; set; }
        
        /// <summary>
        /// 住所1
        /// </summary>
        public string Address1 { get; set; }
        
        /// <summary>
        /// 住所2
        /// </summary>
        public string Address2 { get; set; }
        
        /// <summary>
        /// 請求区分
        /// </summary>
        public int? SeikyuKubun { get; set; }
        
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
