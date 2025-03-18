using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 売上運輸を表すクラス
    /// </summary>
    public partial class TUriageUnsyu
    {
        /// <summary>
        /// 売上運輸ID
        /// </summary>
        public int UriageUnsyuId { get; set; }
        /// <summary>
        /// 売上ID
        /// </summary>
        public int UriageId { get; set; }
        /// <summary>
        /// ソート順
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// デフォルト区分
        /// </summary>
        public int DefaultKubun { get; set; }
        /// <summary>
        /// 売上区分
        /// </summary>
        public int UriageKubun { get; set; }
        /// <summary>
        /// 運輸日
        /// </summary>
        public DateTime UnsyuDate { get; set; }
        /// <summary>
        /// 車両管理ID
        /// </summary>
        public int SyaryoManagementId { get; set; }
        /// <summary>
        /// ドライバーID
        /// </summary>
        public int DriverId { get; set; }
        /// <summary>
        /// 運輸区分
        /// </summary>
        public int UnsyuKubun { get; set; }
        /// <summary>
        /// 精算額
        /// </summary>
        public decimal Seisan { get; set; }
        /// <summary>
        /// 個人負担額
        /// </summary>
        public decimal KojinFutan { get; set; }
        /// <summary>
        /// 個人運輸額
        /// </summary>
        public decimal KojinUnsyu { get; set; }
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
        /// 削除フラグ
        /// </summary>
        public bool DelFlg { get; set; }
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
