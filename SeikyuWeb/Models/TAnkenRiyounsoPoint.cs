using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 案件の利用倉庫地点に関する情報を表します。
    /// </summary>
    public partial class TAnkenRiyounsoPoint
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
        /// 地点順序
        /// </summary>
        public int PointOrder { get; set; }
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
        /// 出発日
        /// </summary>
        public DateTime? FromDate { get; set; }
        /// <summary>
        /// 出発時間
        /// </summary>
        public string FromTime { get; set; }
        /// <summary>
        /// 出発表示
        /// </summary>
        public string FromDisplay { get; set; }
        /// <summary>
        /// 出発住所
        /// </summary>
        public string FromAddress { get; set; }
        /// <summary>
        /// 出発住所コード
        /// </summary>
        public string FromAddressCode { get; set; }
        /// <summary>
        /// 出発住所レベル
        /// </summary>
        public string FromAddressLevel { get; set; }
        /// <summary>
        /// 出発経度
        /// </summary>
        public string FromLng { get; set; }
        /// <summary>
        /// 出発緯度
        /// </summary>
        public string FromLat { get; set; }
        /// <summary>
        /// 出発建物名
        /// </summary>
        public string FromBuildingName { get; set; }
        /// <summary>
        /// 出発建物ZID
        /// </summary>
        public string FromBuildingZid { get; set; }
        /// <summary>
        /// 出発建物ZID属性
        /// </summary>
        public string FromBuildingZidAttr { get; set; }
        /// <summary>
        /// 到着日
        /// </summary>
        public DateTime? ToDate { get; set; }
        /// <summary>
        /// 到着時間
        /// </summary>
        public string ToTime { get; set; }
        /// <summary>
        /// 到着表示
        /// </summary>
        public string ToDisplay { get; set; }
        /// <summary>
        /// 到着住所
        /// </summary>
        public string ToAddress { get; set; }
        /// <summary>
        /// 到着住所コード
        /// </summary>
        public string ToAddressCode { get; set; }
        /// <summary>
        /// 到着住所レベル
        /// </summary>
        public string ToAddressLevel { get; set; }
        /// <summary>
        /// 到着経度
        /// </summary>
        public string ToLng { get; set; }
        /// <summary>
        /// 到着緯度
        /// </summary>
        public string ToLat { get; set; }
        /// <summary>
        /// 到着建物名
        /// </summary>
        public string ToBuildingName { get; set; }
        /// <summary>
        /// 到着建物ZID
        /// </summary>
        public string ToBuildingZid { get; set; }
        /// <summary>
        /// 到着建物ZID属性
        /// </summary>
        public string ToBuildingZidAttr { get; set; }
        /// <summary>
        /// 荷物
        /// </summary>
        public string Luggage { get; set; }
    }
}
