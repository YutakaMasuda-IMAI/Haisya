using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 案件の表示に関する情報を表します。
    /// </summary>
    public partial class TAnkenDisplay
    {
        /// <summary>
        /// 案件表示ID
        /// </summary>
        public int AnkenDisplayId { get; set; }
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// 案件ID
        /// </summary>
        public int AnkenId { get; set; }
        /// <summary>
        /// 台数ソート
        /// </summary>
        public int DaisuuSort { get; set; }
        /// <summary>
        /// 案件キー
        /// </summary>
        public int AnkenKey { get; set; }
        /// <summary>
        /// 日付
        /// </summary>
        public DateTime Day { get; set; }
        /// <summary>
        /// 表示区分
        /// </summary>
        public int DisplayKubun { get; set; }
        /// <summary>
        /// 開始日時
        /// </summary>
        public DateTime StartDatetime { get; set; }
        /// <summary>
        /// 終了日時
        /// </summary>
        public DateTime EndDatetime { get; set; }
        /// <summary>
        /// 開始地点区分
        /// </summary>
        public int StartPointKubun { get; set; }
        /// <summary>
        /// 開始建物名
        /// </summary>
        public string StartBuildingName { get; set; }
        /// <summary>
        /// 開始建物ZID
        /// </summary>
        public string StartBuildingZid { get; set; }
        /// <summary>
        /// 開始建物ZID属性
        /// </summary>
        public string StartBuildingZidAttr { get; set; }
        /// <summary>
        /// 開始建物名読み
        /// </summary>
        public string StartBuildingNameRead { get; set; }
        /// <summary>
        /// 開始地点項目タイトル
        /// </summary>
        public string StartPointKoumokuTitle { get; set; }
        /// <summary>
        /// 開始地点タイプ
        /// </summary>
        public string StartPointType { get; set; }
        /// <summary>
        /// 開始地点名
        /// </summary>
        public string StartPointName { get; set; }
        /// <summary>
        /// 開始経度
        /// </summary>
        public string StartLng { get; set; }
        /// <summary>
        /// 開始緯度
        /// </summary>
        public string StartLat { get; set; }
        /// <summary>
        /// 開始郵便番号
        /// </summary>
        public string StartPostCode { get; set; }
        /// <summary>
        /// 開始住所
        /// </summary>
        public string StartAddress { get; set; }
        /// <summary>
        /// 開始住所1
        /// </summary>
        public string StartAddress1 { get; set; }
        /// <summary>
        /// 開始住所2
        /// </summary>
        public string StartAddress2 { get; set; }
        /// <summary>
        /// 開始住所3
        /// </summary>
        public string StartAddress3 { get; set; }
        /// <summary>
        /// 開始住所4
        /// </summary>
        public string StartAddress4 { get; set; }
        /// <summary>
        /// 終了地点区分
        /// </summary>
        public int EndPointKubun { get; set; }
        /// <summary>
        /// 終了建物名
        /// </summary>
        public string EndBuildingName { get; set; }
        /// <summary>
        /// 終了建物ZID
        /// </summary>
        public string EndBuildingZid { get; set; }
        /// <summary>
        /// 終了建物ZID属性
        /// </summary>
        public string EndBuildingZidAttr { get; set; }
        /// <summary>
        /// 終了建物名読み
        /// </summary>
        public string EndBuildingNameRead { get; set; }
        /// <summary>
        /// 終了地点項目タイトル
        /// </summary>
        public string EndPointKoumokuTitle { get; set; }
        /// <summary>
        /// 終了地点タイプ
        /// </summary>
        public string EndPointType { get; set; }
        /// <summary>
        /// 終了地点名
        /// </summary>
        public string EndPointName { get; set; }
        /// <summary>
        /// 終了経度
        /// </summary>
        public string EndLng { get; set; }
        /// <summary>
        /// 終了緯度
        /// </summary>
        public string EndLat { get; set; }
        /// <summary>
        /// 終了郵便番号
        /// </summary>
        public string EndPostCode { get; set; }
        /// <summary>
        /// 終了住所
        /// </summary>
        public string EndAddress { get; set; }
        /// <summary>
        /// 終了住所1
        /// </summary>
        public string EndAddress1 { get; set; }
        /// <summary>
        /// 終了住所2
        /// </summary>
        public string EndAddress2 { get; set; }
        /// <summary>
        /// 終了住所3
        /// </summary>
        public string EndAddress3 { get; set; }
        /// <summary>
        /// 終了住所4
        /// </summary>
        public string EndAddress4 { get; set; }
        /// <summary>
        /// 表示1
        /// </summary>
        public string Display1 { get; set; }
        /// <summary>
        /// 表示2
        /// </summary>
        public string Display2 { get; set; }
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
