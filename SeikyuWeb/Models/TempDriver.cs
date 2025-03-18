using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 一時ドライバー情報を表すクラス
    /// </summary>
    public partial class TempDriver
    {
        /// <summary>
        /// 作業者コード
        /// </summary>
        public int WorkerCd { get; set; }
        /// <summary>
        /// 作業者名
        /// </summary>
        public string WorkerName { get; set; }
        /// <summary>
        /// ステータス
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 業務開始日
        /// </summary>
        public DateTime? GyoumuStart { get; set; }
        /// <summary>
        /// 退職日
        /// </summary>
        public DateTime? TaisyokuDate { get; set; }
        /// <summary>
        /// 入社日
        /// </summary>
        public DateTime? NyusyaDate { get; set; }
        /// <summary>
        /// 事務所
        /// </summary>
        public string Office { get; set; }
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DelFlg { get; set; }
        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime UpDate { get; set; }
        /// <summary>
        /// 勤怠対象外フラグ
        /// </summary>
        public bool NotKintaiFlg { get; set; }
    }
}
