using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 売上支払情報を表します。
    /// </summary>
    public partial class TUriageShitabarai
    {
        /// <summary>
        /// 売上支払ID
        /// </summary>
        public int UriageShiharaiId { get; set; }
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
        /// 支払日
        /// </summary>
        public DateTime ShiharaiDate { get; set; }
        /// <summary>
        /// 締め日
        /// </summary>
        public int ShimeDay { get; set; }
        /// <summary>
        /// 車番
        /// </summary>
        public string Syaban { get; set; }
        /// <summary>
        /// 傭車支店ID
        /// </summary>
        public int YosyaBranchId { get; set; }
        /// <summary>
        /// 積み地
        /// </summary>
        public string Tsumi { get; set; }
        /// <summary>
        /// 卸し地
        /// </summary>
        public string Oroshi { get; set; }
        /// <summary>
        /// 荷物
        /// </summary>
        public string Luggage { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public double Qty { get; set; }
        /// <summary>
        /// 単位
        /// </summary>
        public int Unit { get; set; }
        /// <summary>
        /// 単価
        /// </summary>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// 計算価格
        /// </summary>
        public decimal CalcPrice { get; set; }
        /// <summary>
        /// 支払価格
        /// </summary>
        public decimal ShiharaiPrice { get; set; }
        /// <summary>
        /// 割増価格
        /// </summary>
        public decimal WarimashiPrice { get; set; }
        /// <summary>
        /// 立替金
        /// </summary>
        public decimal Tatekaekin { get; set; }
        /// <summary>
        /// 税区分
        /// </summary>
        public int ZeiKubun { get; set; }
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
