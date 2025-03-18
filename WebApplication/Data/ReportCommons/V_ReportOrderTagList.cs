using System;
using System.Text.Json.Serialization;

namespace WebApplication.Data.ReportCommons
{
    /// <summary>
    /// V_ReportOrderTagListクラスは帳票order tag　pdfのモデル
    /// </summary>
    public partial class V_ReportOrderTagList : T_Anken_Detail
    {
        /// <summary>
        /// 選択行
        /// </summary>
        public int SelectRow { get; set; }
        
        /// <summary>
        /// 案件番号
        /// </summary>
        public string Anken_No { get; set; }
        
        /// <summary>
        /// 案件ステータス
        /// </summary>
        public int Anken_Status { get; set; }
        
        /// <summary>
        /// 案件最新注文
        /// </summary>
        public int Anken_Latest_Order { get; set; }
        
        /// <summary>
        /// 案件区分
        /// </summary>
        public int Anken_Kubun { get; set; }
        
        /// <summary>
        /// 専属ID
        /// </summary>
        public int SenzokuID { get; set; }
        
        /// <summary>
        /// 専属ドライバーID
        /// </summary>
        public int Senzoku_Driver_ID { get; set; }
        
        /// <summary>
        /// 発行グループID
        /// </summary>
        public int? PublishGroup_ID { get; set; }
        
        /// <summary>
        /// 発行フラグ
        /// </summary>
        public bool? Publish_Flg { get; set; }
        
        /// <summary>
        /// 発行開始日時
        /// </summary>
        public DateTime? Publish_FromDatetime { get; set; }
        
        /// <summary>
        /// 発行終了日時
        /// </summary>
        public DateTime? Publish_ToDatetime { get; set; }
        
        /// <summary>
        /// 車種表示2
        /// </summary>
        public string SyasyuDisplay2 { get; set; }
        
        /// <summary>
        /// 車種台数表示
        /// </summary>
        public string SyasyuDaisuDisplay { get; set; }
        
        /// <summary>
        /// 担当者名
        /// </summary>
        public string Tantou_Name { get; set; }
        
        /// <summary>
        /// 営業者名
        /// </summary>
        public string Eigyo_Name { get; set; }
        
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks { get; set; }
        
        /// <summary>
        /// 荷物
        /// </summary>
        public string Luggage { get; set; }
        
        /// <summary>
        /// 装備
        /// </summary>
        public string Equipment { get; set; }
        
        /// <summary>
        /// 顧客名略称
        /// </summary>
        [JsonPropertyName("Customer_Name_Abbr")]
        public string Customer_Name_Abbr { get; set; }
        
        /// <summary>
        /// 案件ステータス表示
        /// </summary>
        public string AnkenStatusDisplay { get; set; }
        
        /// <summary>
        /// 案件ステップ
        /// </summary>
        public string AnkenStep { get; set; }
        
        /// <summary>
        /// 暫定金額
        /// </summary>
        public decimal? ZanteiAmount { get; set; }
        
        /// <summary>
        /// 確定金額
        /// </summary>
        public decimal? KakuteiAmount { get; set; }
        
        /// <summary>
        /// 案件表示ID
        /// </summary>
        public int AnkenDisplay_ID { get; set; }
        
        /// <summary>
        /// 台数ソート
        /// </summary>
        public int Daisuu_Sort { get; set; }
        
        /// <summary>
        /// 案件キー
        /// </summary>
        public int Anken_Key { get; set; }
        
        /// <summary>
        /// 日付
        /// </summary>
        public DateTime Day { get; set; }
        
        /// <summary>
        /// 表示区分
        /// </summary>
        public int Display_Kubun { get; set; }
        
        /// <summary>
        /// フル車番
        /// </summary>
        public string FULL_SYABAN { get; set; }
        
        /// <summary>
        /// 車番
        /// </summary>
        [JsonPropertyName("SYABAN")]
        public string SYABAN { get; set; }
        
        /// <summary>
        /// 車種表示
        /// </summary>
        public string Syasyu_Disp { get; set; }
        
        /// <summary>
        /// 従業員番号
        /// </summary>
        public string Employee_Number { get; set; }
        
        /// <summary>
        /// ドライバーID
        /// </summary>
        public int? Driver_ID { get; set; }
        
        /// <summary>
        /// ドライバー名
        /// </summary>
        [JsonPropertyName("Driver_Name")]
        public string Driver_Name { get; set; }
        
        /// <summary>
        /// ドライバー車両ID
        /// </summary>
        public int? DriverSyaryo_ID { get; set; }
        
        /// <summary>
        /// 車両管理ID
        /// </summary>
        public int? SyaryoManagement_ID { get; set; }
        
        /// <summary>
        /// 車両管理ID1
        /// </summary>
        public int? SyaryoManagement_ID1 { get; set; }
        
        /// <summary>
        /// ドライバー支店名
        /// </summary>
        public string Driver_Branch_Name { get; set; }
        
        /// <summary>
        /// 傭車表示1
        /// </summary>
        public string Yosya_Display1 { get; set; }
        
        /// <summary>
        /// 傭車表示2
        /// </summary>
        public string Yosya_Display2 { get; set; }
        
        /// <summary>
        /// 傭車表示3
        /// </summary>
        public string Yosya_Display3 { get; set; }
        
        /// <summary>
        /// 配車ID
        /// </summary>
        public int? Haisya_ID { get; set; }
        
        /// <summary>
        /// 配車ステータス
        /// </summary>
        public int? Haisya_Status { get; set; }
        
        /// <summary>
        /// 配車区分
        /// </summary>
        public int? Haisya_Kubun { get; set; }
        
        /// <summary>
        /// 配車区分表示
        /// </summary>
        public string HaisyaKubunDisplay { get; set; }
        
        /// <summary>
        /// 開始日時
        /// </summary>
        [JsonPropertyName("StartDatetime")]
        public DateTime StartDatetime { get; set; }
        
        /// <summary>
        /// 終了日時
        /// </summary>
        public DateTime EndDatetime { get; set; }
        
        /// <summary>
        /// 開始地点区分
        /// </summary>
        public int Start_Point_Kubun { get; set; }
        
        /// <summary>
        /// 開始建物名
        /// </summary>
        public string Start_BuildingName { get; set; }
        
        /// <summary>
        /// 開始建物ZID
        /// </summary>
        public string Start_BuildingZid { get; set; }
        
        /// <summary>
        /// 開始建物ZID属性
        /// </summary>
        public string Start_BuildingZid_Attr { get; set; }
        
        /// <summary>
        /// 開始建物名読み
        /// </summary>
        public string Start_BuildingNameRead { get; set; }
        
        /// <summary>
        /// 開始地点項目タイトル
        /// </summary>
        public string Start_Point_KoumokuTitle { get; set; }
        
        /// <summary>
        /// 開始地点タイプ
        /// </summary>
        public string Start_Point_Type { get; set; }
        
        /// <summary>
        /// 開始地点名
        /// </summary>
        public string Start_PointName { get; set; }
        
        /// <summary>
        /// 開始経度
        /// </summary>
        public string Start_Lng { get; set; }
        
        /// <summary>
        /// 開始緯度
        /// </summary>
        public string Start_Lat { get; set; }
        
        /// <summary>
        /// 開始郵便番号
        /// </summary>
        public string Start_Post_code { get; set; }
        
        /// <summary>
        /// 開始住所
        /// </summary>
        public string Start_Address { get; set; }
        
        /// <summary>
        /// 開始住所2
        /// </summary>
        public string Start_Address2 { get; set; }
        
        /// <summary>
        /// 開始住所3
        /// </summary>
        public string Start_Address3 { get; set; }
        
        /// <summary>
        /// 開始住所4
        /// </summary>
        public string Start_Address4 { get; set; }
        
        /// <summary>
        /// 終了地点区分
        /// </summary>
        public int End_Point_Kubun { get; set; }
        
        /// <summary>
        /// 終了建物名
        /// </summary>
        public string End_BuildingName { get; set; }
        
        /// <summary>
        /// 終了建物ZID
        /// </summary>
        public string End_BuildingZid { get; set; }
        
        /// <summary>
        /// 終了建物ZID属性
        /// </summary>
        public string End_BuildingZid_Attr { get; set; }
        
        /// <summary>
        /// 終了建物名読み
        /// </summary>
        public string End_BuildingNameRead { get; set; }
        
        /// <summary>
        /// 終了地点項目タイトル
        /// </summary>
        public string End_Point_KoumokuTitle { get; set; }
        
        /// <summary>
        /// 終了地点タイプ
        /// </summary>
        public string End_Point_Type { get; set; }
        
        /// <summary>
        /// 終了地点名
        /// </summary>
        public string End_PointName { get; set; }
        
        /// <summary>
        /// 終了経度
        /// </summary>
        public string End_Lng { get; set; }
        
        /// <summary>
        /// 終了緯度
        /// </summary>
        public string End_Lat { get; set; }
        
        /// <summary>
        /// 終了郵便番号
        /// </summary>
        public string End_Post_code { get; set; }
        
        /// <summary>
        /// 終了住所
        /// </summary>
        public string End_Address { get; set; }
        
        /// <summary>
        /// 終了住所2
        /// </summary>
        public string End_Address2 { get; set; }
        
        /// <summary>
        /// 終了住所3
        /// </summary>
        public string End_Address3 { get; set; }
        
        /// <summary>
        /// 終了住所4
        /// </summary>
        public string End_Address4 { get; set; }
        
        /// <summary>
        /// 表示1
        /// </summary>
        public string Display1 { get; set; }
        
        /// <summary>
        /// 表示2
        /// </summary>
        public string Display2 { get; set; }
        
        /// <summary>
        /// 開始住所表示
        /// </summary>
        public string StartAddressDisplay { get; set; }
        
        /// <summary>
        /// 開始住所表示2
        /// </summary>
        public string StartAddressDisplay2 { get; set; }
        
        /// <summary>
        /// 開始住所表示3
        /// </summary>
        [JsonPropertyName("StartAddressDisplay3")]
        public string StartAddressDisplay3 { get; set; }
        
        /// <summary>
        /// エリア表示
        /// </summary>
        public string Area_Disp { get; set; }
        
        /// <summary>
        /// エリアソート順
        /// </summary>
        public int? Area_Sort_Order { get; set; }
        
        /// <summary>
        /// 終了住所表示
        /// </summary>
        public string EndAddressDisplay { get; set; }
        
        /// <summary>
        /// 終了住所表示2
        /// </summary>
        public string EndAddressDisplay2 { get; set; }
        
        /// <summary>
        /// 終了住所表示3
        /// </summary>
        [JsonPropertyName("EndAddressDisplay3")]
        public string EndAddressDisplay3 { get; set; }
        
        /// <summary>
        /// 日報ステータス
        /// </summary>
        public int NippouStatus { get; set; }
        
        /// <summary>
        /// 連絡区分
        /// </summary>
        public int Renraku_Kubun { get; set; }
        
        /// <summary>
        /// 連携ステータス
        /// </summary>
        public int RenkeiStatus { get; set; }
    }
}