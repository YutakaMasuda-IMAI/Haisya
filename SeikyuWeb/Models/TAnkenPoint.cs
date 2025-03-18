using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 案件の地点情報を表します。
    /// </summary>
    public partial class TAnkenPoint
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
        /// 区分
        /// </summary>
        public int Kubun { get; set; }
        /// <summary>
        /// 地点順序
        /// </summary>
        public int PointOrder { get; set; }
        /// <summary>
        /// セクション区分
        /// </summary>
        public string Sekubun { get; set; }
        /// <summary>
        /// 住所
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 住所コード
        /// </summary>
        public string AddressCode { get; set; }
        /// <summary>
        /// 住所レベル
        /// </summary>
        public string AddressLevel { get; set; }
        /// <summary>
        /// 経度
        /// </summary>
        public string Lng { get; set; }
        /// <summary>
        /// 緯度
        /// </summary>
        public string Lat { get; set; }
        /// <summary>
        /// 建物名
        /// </summary>
        public string BuildingName { get; set; }
        /// <summary>
        /// 建物ZID
        /// </summary>
        public string BuildingZid { get; set; }
        /// <summary>
        /// 建物ZID属性
        /// </summary>
        public string BuildingZidAttr { get; set; }
        /// <summary>
        /// 建物名読み
        /// </summary>
        public string BuildingNameRead { get; set; }
        /// <summary>
        /// 地点項目タイトル
        /// </summary>
        public string PointKoumokuTitle { get; set; }
        /// <summary>
        /// 地点タイプ
        /// </summary>
        public string PointType { get; set; }
        /// <summary>
        /// 地点名
        /// </summary>
        public string PointName { get; set; }
        /// <summary>
        /// 地点日付
        /// </summary>
        public DateTime? PointDate { get; set; }
        /// <summary>
        /// 地点時間
        /// </summary>
        public string PointTime { get; set; }
        /// <summary>
        /// 地点時間区分
        /// </summary>
        public int? PointTimeKubun { get; set; }
        /// <summary>
        /// 地点ステータス区分
        /// </summary>
        public int? PointStatusKubun { get; set; }
        /// <summary>
        /// 現地確認フラグ
        /// </summary>
        public bool? FlgGenchiKakunin { get; set; }
        /// <summary>
        /// 通行料金表示
        /// </summary>
        public string TollDisplay { get; set; }
        /// <summary>
        /// 通行料金表示高さ
        /// </summary>
        public double? TollDisplayHeight { get; set; }
        /// <summary>
        /// 郵便番号
        /// </summary>
        public string PostCode { get; set; }
        /// <summary>
        /// 住所2
        /// </summary>
        public string Address2 { get; set; }
        /// <summary>
        /// 住所3
        /// </summary>
        public string Address3 { get; set; }
        /// <summary>
        /// 住所4
        /// </summary>
        public string Address4 { get; set; }
        /// <summary>
        /// 道路タイプ
        /// </summary>
        public string RoadType { get; set; }
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
    }
}
