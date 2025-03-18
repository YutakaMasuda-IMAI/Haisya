using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 配車周辺情報を表すクラス
    /// </summary>
    public partial class THaisyaAround
    {
        /// <summary>
        /// 配車周辺ID
        /// </summary>
        public int HaisyaAroundId { get; set; }
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// 日付
        /// </summary>
        public DateTime Day { get; set; }
        /// <summary>
        /// 案件ID
        /// </summary>
        public int AnkenId { get; set; }
        /// <summary>
        /// 配車ID
        /// </summary>
        public int HaisyaId { get; set; }
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
        /// ポイント項目タイトル
        /// </summary>
        public string PointKoumokuTitle { get; set; }
        /// <summary>
        /// ポイントタイプ
        /// </summary>
        public string PointType { get; set; }
        /// <summary>
        /// ポイント名
        /// </summary>
        public string PointName { get; set; }
        /// <summary>
        /// ポイント日付
        /// </summary>
        public DateTime? PointDate { get; set; }
        /// <summary>
        /// ポイント時間
        /// </summary>
        public string PointTime { get; set; }
        /// <summary>
        /// ポイント時間区分
        /// </summary>
        public int? PointTimeKubun { get; set; }
        /// <summary>
        /// ポイントステータス区分
        /// </summary>
        public int? PointStatusKubun { get; set; }
        /// <summary>
        /// 現地確認フラグ
        /// </summary>
        public bool? FlgGenchiKakunin { get; set; }
        /// <summary>
        /// 通行料表示
        /// </summary>
        public string TollDisplay { get; set; }
        /// <summary>
        /// 通行料表示高さ
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
    }
}
