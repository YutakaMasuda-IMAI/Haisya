using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 事故を表します。
    /// </summary>
    public partial class TJiko
    {
        /// <summary>
        /// 事故ID
        /// </summary>
        public int JikoId { get; set; }

        /// <summary>
        /// 事故番号
        /// </summary>
        public string JikoNo { get; set; }

        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 支店ID
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// ドライバーID
        /// </summary>
        public int DriverId { get; set; }

        /// <summary>
        /// 配車グループID
        /// </summary>
        public int HaisyaGroupId { get; set; }

        /// <summary>
        /// 事故区分
        /// </summary>
        public int JikoKubun { get; set; }

        /// <summary>
        /// 事故ステータス
        /// </summary>
        public int JikoStatus { get; set; }

        /// <summary>
        /// 事故ワークフローベースID
        /// </summary>
        public int JikoWorkFlowBaseId { get; set; }

        /// <summary>
        /// 事故ワークフローステータス
        /// </summary>
        public int JikoWorkFlowStatus { get; set; }

        /// <summary>
        /// 事故の表示名
        /// </summary>
        public string JikoDisplay { get; set; }

        /// <summary>
        /// 事故発生日
        /// </summary>
        public DateTime JikoDate { get; set; }

        /// <summary>
        /// 事故負担金額
        /// </summary>
        public decimal JikoFutanMoney { get; set; }

        /// <summary>
        /// 車両管理ID
        /// </summary>
        public int SyaryoManagementId { get; set; }

        /// <summary>
        /// 車両管理ID1
        /// </summary>
        public int SyaryoManagementId1 { get; set; }

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
