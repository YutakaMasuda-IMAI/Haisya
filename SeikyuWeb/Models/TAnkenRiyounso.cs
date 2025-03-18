using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 案件の利用倉庫に関する情報を表します。
    /// </summary>
    public partial class TAnkenRiyounso
    {
        /// <summary>
        /// 案件ID
        /// </summary>
        public int AnkenId { get; set; }
        /// <summary>
        /// 案件の順序
        /// </summary>
        public int AnkenOrder { get; set; }
        /// <summary>
        /// 対象日
        /// </summary>
        public DateTime TargetDate { get; set; }
        /// <summary>
        /// 登録日時
        /// </summary>
        public DateTime? InsertDatetime { get; set; }
        /// <summary>
        /// 登録ユーザー
        /// </summary>
        public int? InsertUser { get; set; }
        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime? UpdateDatetime { get; set; }
        /// <summary>
        /// 更新ユーザー
        /// </summary>
        public int? UpdateUser { get; set; }
        /// <summary>
        /// 担当者ID
        /// </summary>
        public int? TantouId { get; set; }
        /// <summary>
        /// 営業ID
        /// </summary>
        public int? EigyoId { get; set; }
        /// <summary>
        /// 顧客ID
        /// </summary>
        public int? KokyakuId { get; set; }
        /// <summary>
        /// 顧客コード
        /// </summary>
        public string KokyakuCode { get; set; }
        /// <summary>
        /// 顧客名
        /// </summary>
        public string KokyakuName { get; set; }
        /// <summary>
        /// 顧客担当者ID
        /// </summary>
        public int? KokyakuTantouId { get; set; }
        /// <summary>
        /// 顧客担当者名
        /// </summary>
        public string KokyakuTantouName { get; set; }
        /// <summary>
        /// 顧客担当者電話番号
        /// </summary>
        public string KokyakuTantouPhone { get; set; }
        /// <summary>
        /// 車両ID
        /// </summary>
        public int SyaryoId { get; set; }
        /// <summary>
        /// 車種
        /// </summary>
        public string Syasyu { get; set; }
        /// <summary>
        /// 車種サイズ
        /// </summary>
        public string SyasyuSize { get; set; }
        /// <summary>
        /// 車種表示
        /// </summary>
        public string SyasyuDisplay { get; set; }
        /// <summary>
        /// 型
        /// </summary>
        public string Kata { get; set; }
        /// <summary>
        /// 台数
        /// </summary>
        public int? Daisuu { get; set; }
        /// <summary>
        /// ルートフェリー
        /// </summary>
        public string RootFerry { get; set; }
        /// <summary>
        /// ルート規制
        /// </summary>
        public string RootRegulation { get; set; }
        /// <summary>
        /// ルート往復
        /// </summary>
        public string RootTwouturn { get; set; }
        /// <summary>
        /// 他社ドライバーID
        /// </summary>
        public int? YosyaDriverId { get; set; }
        /// <summary>
        /// 他社支店ID
        /// </summary>
        public int YosyaBranchId { get; set; }
        /// <summary>
        /// 他社ドライバーID1
        /// </summary>
        public int YosyaDriverId1 { get; set; }
        /// <summary>
        /// 他社ドライバー車両ID
        /// </summary>
        public int YosyaDriverSyaryoId { get; set; }
        /// <summary>
        /// 荷物
        /// </summary>
        public string Luggage { get; set; }
        /// <summary>
        /// 請求区分
        /// </summary>
        public int? SeikyuKubun { get; set; }
        /// <summary>
        /// 総額
        /// </summary>
        public decimal? GrossAmount { get; set; }
        /// <summary>
        /// 通行料金
        /// </summary>
        public decimal? Toll { get; set; }
        /// <summary>
        /// 支払額
        /// </summary>
        public decimal? PaymentAmount { get; set; }
        /// <summary>
        /// 前払い金
        /// </summary>
        public decimal? AdvancesPaid { get; set; }
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks { get; set; }
    }
}
