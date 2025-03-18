using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 個人運輸ルート情報を表すクラス
    /// </summary>
    public partial class MKojinUnsyuRoute
    {
        /// <summary>
        /// 個人運輸ルートID
        /// </summary>
        public int KojinUnsyuRouteId { get; set; }
        
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        
        /// <summary>
        /// ルート手当
        /// </summary>
        public decimal RouteTeate { get; set; }
        
        /// <summary>
        /// ルート残業
        /// </summary>
        public decimal RouteOverTime { get; set; }
        
        /// <summary>
        /// ルート深夜
        /// </summary>
        public decimal RouteMidnight { get; set; }
        
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
