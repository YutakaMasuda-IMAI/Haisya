using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// ポイント情報を表します。
    /// </summary>
    public partial class TPoint
    {
        /// <summary>
        /// ポイントID
        /// </summary>
        public int PointId { get; set; }
        /// <summary>
        /// ユーザーID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// グループID
        /// </summary>
        public int GroupId { get; set; }
        /// <summary>
        /// 建物ZID
        /// </summary>
        public string BuildingZid { get; set; }
        /// <summary>
        /// 建物ZID属性
        /// </summary>
        public string BuildingZidAttr { get; set; }
        /// <summary>
        /// 建物名
        /// </summary>
        public string BuildingName { get; set; }
        /// <summary>
        /// 建物名読み
        /// </summary>
        public string BuildingNameRead { get; set; }
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
        /// 登録日時
        /// </summary>
        public DateTime? InsertDatetime { get; set; }
        /// <summary>
        /// 登録ユーザー
        /// </summary>
        public int? InsertUser { get; set; }
    }
}
