using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 配車情報を表すクラス
    /// </summary>
    public partial class THaisya
    {
        /// <summary>
        /// 配車ID
        /// </summary>
        public int HaisyaId { get; set; }
        /// <summary>
        /// 案件表示ID
        /// </summary>
        public int AnkenDisplayId { get; set; }
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// 案件ID
        /// </summary>
        public int AnkenId { get; set; }
        /// <summary>
        /// 日付
        /// </summary>
        public DateTime Day { get; set; }
        /// <summary>
        /// 配車区分
        /// </summary>
        public int HaisyaKubun { get; set; }
        /// <summary>
        /// ドライバーID
        /// </summary>
        public int DriverId { get; set; }
        /// <summary>
        /// ドライバー車両ID
        /// </summary>
        public int DriverSyaryoId { get; set; }
        /// <summary>
        /// 車両管理ID
        /// </summary>
        public int SyaryoManagementId { get; set; }
        /// <summary>
        /// 車両管理ID1（オプション）
        /// </summary>
        public int? SyaryoManagementId1 { get; set; }
        /// <summary>
        /// 区分
        /// </summary>
        public int Kubun { get; set; }
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 配車ステータス
        /// </summary>
        public int HaisyaStatus { get; set; }
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
