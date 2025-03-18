using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 予社支払いを表すクラス
    /// </summary>
    public partial class TYosyaShiharai
    {
        /// <summary>
        /// 予社支払いID
        /// </summary>
        public int YosyaShiharaiId { get; set; }
        /// <summary>
        /// 支払ID
        /// </summary>
        public int ShitabaraiId { get; set; }
        /// <summary>
        /// 処理区分
        /// </summary>
        public int ProcessKubun { get; set; }
        /// <summary>
        /// 処理日
        /// </summary>
        public DateTime ProcessDate { get; set; }
        /// <summary>
        /// 現金額
        /// </summary>
        public decimal CashAmount { get; set; }
        /// <summary>
        /// 振込額
        /// </summary>
        public decimal TransferAmount { get; set; }
        /// <summary>
        /// 手形額
        /// </summary>
        public decimal DraftAmount { get; set; }
        /// <summary>
        /// 小切手額
        /// </summary>
        public decimal CheckAmount { get; set; }
        /// <summary>
        /// サービス料額
        /// </summary>
        public decimal ServiceChargeAmount { get; set; }
        /// <summary>
        /// 運賃相殺
        /// </summary>
        public decimal UnchinOffset { get; set; }
        /// <summary>
        /// 一般相殺
        /// </summary>
        public decimal GeneralOffset { get; set; }
        /// <summary>
        /// 調整額
        /// </summary>
        public decimal AdjustmentAmount { get; set; }
        /// <summary>
        /// 合計額
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks { get; set; }
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
