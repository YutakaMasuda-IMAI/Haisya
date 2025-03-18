using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 売上運賃を表すクラス
    /// </summary>
    public partial class TUriageUnchin
    {
        /// <summary>
        /// 売上運賃ID
        /// </summary>
        public int UriageUnchinId { get; set; }
        /// <summary>
        /// 売上ID
        /// </summary>
        public int UriageId { get; set; }
        /// <summary>
        /// ソート順
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 売上区分
        /// </summary>
        public int UriageKubun { get; set; }
        /// <summary>
        /// デフォルト区分
        /// </summary>
        public int DefaultKubun { get; set; }
        /// <summary>
        /// 請求日
        /// </summary>
        public DateTime SeikyuDate { get; set; }
        /// <summary>
        /// 締め日
        /// </summary>
        public int ShimeDay { get; set; }
        /// <summary>
        /// 顧客ID
        /// </summary>
        public int CustomerBranchId { get; set; }
        /// <summary>
        /// 積み地
        /// </summary>
        public string Tsumi { get; set; }
        /// <summary>
        /// 降ろし地
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
        /// 請求運賃
        /// </summary>
        public decimal SeikyuUnchin { get; set; }
        /// <summary>
        /// 立替金
        /// </summary>
        public decimal Tatekaekin { get; set; }
        /// <summary>
        /// 割増1
        /// </summary>
        public decimal Warimashi1 { get; set; }
        /// <summary>
        /// 割増2
        /// </summary>
        public decimal Warimashi2 { get; set; }
        /// <summary>
        /// 割増3
        /// </summary>
        public decimal Warimashi3 { get; set; }
        /// <summary>
        /// 割増4
        /// </summary>
        public decimal Warimashi4 { get; set; }
        /// <summary>
        /// 割増5
        /// </summary>
        public decimal Warimashi5 { get; set; }
        /// <summary>
        /// 請求合計
        /// </summary>
        public decimal SeikyuTotal { get; set; }
        /// <summary>
        /// 税区分
        /// </summary>
        public int ZeiKubun { get; set; }
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
        /// <summary>
        /// 赤黒備考
        /// </summary>
        public string AkaKuroRemarks { get; set; }
        /// <summary>
        /// 車両ID
        /// </summary>
        public int SyaryoId { get; set; }
        /// <summary>
        /// 車種表示
        /// </summary>
        public string SyasyuDisplay { get; set; }
        /// <summary>
        /// 車番
        /// </summary>
        public string Syaban { get; set; }
        /// <summary>
        /// 備考1
        /// </summary>
        public string Remarks1 { get; set; }
        /// <summary>
        /// 備考2
        /// </summary>
        public string Remarks2 { get; set; }
    }
}
