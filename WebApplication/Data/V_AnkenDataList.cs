using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    /// <summary>
    /// 案件データリストを表します。
    /// </summary>
    [Keyless]
    [Table("V_AnkenDataList")]
    public partial class V_AnkenDataList
    {
        /// <summary>
        /// 選択行
        /// </summary>
        public int SelectRow { get; set; }
        /// <summary>
        /// 案件番号
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Anken_No { get; set; }
        /// <summary>
        /// 案件ステータス
        /// </summary>
        public int Anken_Status { get; set; }
        /// <summary>
        /// 最新の案件注文
        /// </summary>
        public int Anken_Latest_Order { get; set; }
        /// <summary>
        /// 案件ID
        /// </summary>
        public int Anken_ID { get; set; }
        /// <summary>
        /// 案件注文
        /// </summary>
        public int Anken_Order { get; set; }
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
        /// 顧客名略称
        /// </summary>
        [StringLength(40)]
        public string Customer_Name_Abbr { get; set; }
        /// <summary>
        /// 備考
        /// </summary>
        [StringLength(255)]
        public string Remarks { get; set; }
        /// <summary>
        /// 荷物
        /// </summary>
        [StringLength(255)]
        public string Luggage { get; set; }
        /// <summary>
        /// 装備
        /// </summary>
        [StringLength(255)]
        public string Equipment { get; set; }
        /// <summary>
        /// 挿入日時
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? Insert_Datetime { get; set; }
        /// <summary>
        /// 挿入ユーザー
        /// </summary>
        public int? Insert_User { get; set; }
        /// <summary>
        /// 更新日時
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? Update_Datetime { get; set; }
        /// <summary>
        /// 更新ユーザー
        /// </summary>
        public int? Update_User { get; set; }
        /// <summary>
        /// 担当ID
        /// </summary>
        public int? TantouID { get; set; }
        /// <summary>
        /// 営業ID
        /// </summary>
        public int? EigyoID { get; set; }
        /// <summary>
        /// 顧客ID
        /// </summary>
        public int? KokyakuId { get; set; }
        /// <summary>
        /// 顧客名
        /// </summary>
        [StringLength(50)]
        public string KokyakuName { get; set; }
        /// <summary>
        /// 顧客担当ID
        /// </summary>
        public int? KokyakuTantouId { get; set; }
        /// <summary>
        /// 顧客担当名
        /// </summary>
        [StringLength(50)]
        public string KokyakuTantouName { get; set; }
        /// <summary>
        /// 顧客担当電話番号
        /// </summary>
        [StringLength(50)]
        public string KokyakuTantouPhone { get; set; }
        /// <summary>
        /// 作業名
        /// </summary>
        [StringLength(100)]
        public string Work_Name { get; set; }
        /// <summary>
        /// 車両ID
        /// </summary>
        public int Syaryo_ID { get; set; }
        /// <summary>
        /// 車種
        /// </summary>
        [StringLength(20)]
        public string Syasyu { get; set; }
        /// <summary>
        /// 車種サイズ
        /// </summary>
        [StringLength(20)]
        public string SyasyuSize { get; set; }
        /// <summary>
        /// 車種表示
        /// </summary>
        [StringLength(50)]
        public string SyasyuDisplay { get; set; }
        /// <summary>
        /// 車種表示2
        /// </summary>
        [StringLength(50)]
        public string SyasyuDisplay2 { get; set; }
        /// <summary>
        /// 車種台数表示
        /// </summary>
        [StringLength(100)]
        public string SyasyuDaisuDisplay { get; set; }
        /// <summary>
        /// 型
        /// </summary>
        [StringLength(20)]
        public string Kata { get; set; }
        /// <summary>
        /// 台数
        /// </summary>
        public int? Daisuu { get; set; }
        /// <summary>
        /// ルートフェリー
        /// </summary>
        [StringLength(30)]
        public string Root_Ferry { get; set; }
        /// <summary>
        /// ルート規制
        /// </summary>
        [StringLength(30)]
        public string Root_Regulation { get; set; }
        /// <summary>
        /// ルート二往復
        /// </summary>
        [StringLength(30)]
        public string Root_Twouturn { get; set; }
        /// <summary>
        /// ルート営業所戻り
        /// </summary>
        [StringLength(30)]
        public bool Root_EigyoshoModori { get; set; }
        /// <summary>
        /// 請求区分
        /// </summary>
        public int? SeikyuKubun { get; set; }
        /// <summary>
        /// 数量制限区分
        /// </summary>
        public int? NumberCommLimitKubun { get; set; }
        /// <summary>
        /// 数量制限日時
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? NumberCommLimitDateTime { get; set; }
        /// <summary>
        /// ルートID
        /// </summary>
        [StringLength(255)]
        public string RouteID { get; set; }
        /// <summary>
        /// ルートタイプ
        /// </summary>
        public int? RouteType { get; set; }
        /// <summary>
        /// ルートタイプ表示
        /// </summary>
        [StringLength(10)]
        public string RouteTypeDisplay { get; set; }
        /// <summary>
        /// ルート総時間
        /// </summary>
        [StringLength(10)]
        public string Route_TotalTime { get; set; }
        /// <summary>
        /// ルート休憩時間
        /// </summary>
        public int? Route_BreakTime { get; set; }
        /// <summary>
        /// ルート休息時間
        /// </summary>
        public int? Route_RestTime { get; set; }
        /// <summary>
        /// ルート総距離
        /// </summary>
        public double? Route_TotalDistance { get; set; }
        /// <summary>
        /// ルート燃料消費
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Route_FuelConsume { get; set; }
        /// <summary>
        /// ルート総通行料
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Route_Totaltoll { get; set; }
        /// <summary>
        /// ルート休息時間表示
        /// </summary>
        [StringLength(20)]
        public string Route_RestTimeDisplay { get; set; }
        /// <summary>
        /// ルート標準運賃
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Route_StdFreight { get; set; }
        /// <summary>
        /// ルート標準追加料金
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Route_StdExcharge { get; set; }
        /// <summary>
        /// ルート標準全運賃
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Route_StdALLFreight { get; set; }
        /// <summary>
        /// ルート標準総運賃
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Route_StdTotalFreight { get; set; }
        /// <summary>
        /// ルート労働コスト総額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Route_GrossAmountForLaborCost { get; set; }
        /// <summary>
        /// ルート燃料コスト総額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Route_GrossAmountForFuelCost { get; set; }
        /// <summary>
        /// ルート車両コスト総額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Route_GrossAmountForSyaryoCost { get; set; }
        /// <summary>
        /// ルート荷物コスト総額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Route_GrossAmountForLuggage { get; set; }
        /// <summary>
        /// ルート追加料金総額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Route_GrossAmountForExcharge { get; set; }
        /// <summary>
        /// ルート総額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Route_GrossAmount { get; set; }
        /// <summary>
        /// ルート総額合計
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Route_GrossAmountTotal { get; set; }
        /// <summary>
        /// ルート総日数
        /// </summary>
        public int? Route_TotalDays { get; set; }
        /// <summary>
        /// 基本料金
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? BaseFee { get; set; }
        /// <summary>
        /// 追加料金
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? ExtraCharge { get; set; }
        /// <summary>
        /// 通行料
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Toll { get; set; }
        /// <summary>
        /// 割引
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Discount { get; set; }
        /// <summary>
        /// 総額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? GrossAmount { get; set; }
        /// <summary>
        /// 高さ
        /// </summary>
        public double? Height { get; set; }
        /// <summary>
        /// 幅
        /// </summary>
        public double? Width { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public double? Weight { get; set; }
        /// <summary>
        /// 燃費
        /// </summary>
        public double? Nenpi { get; set; }
        /// <summary>
        /// 担当者名
        /// </summary>
        [StringLength(50)]
        public string Tantou_Name { get; set; }
        /// <summary>
        /// 営業者名
        /// </summary>
        [StringLength(50)]
        public string Eigyo_Name { get; set; }
        /// <summary>
        /// 案件ステータス表示
        /// </summary>
        [StringLength(50)]
        public string AnkenStatusDisplay { get; set; }
        /// <summary>
        /// 案件ステップ
        /// </summary>
        [StringLength(50)]
        public string AnkenStep { get; set; }
        /// <summary>
        /// ドライバー総額計算
        /// </summary>
        public int DriverGrossCalc { get; set; }
        /// <summary>
        /// エリア
        /// </summary>
        public int Area { get; set; }
        /// <summary>
        /// 積みタスク時間
        /// </summary>
        [StringLength(10)]
        public string TsumiTaskTime { get; set; }
        /// <summary>
        /// 卸しタスク時間
        /// </summary>
        [StringLength(10)]
        public string OroshiTaskTime { get; set; }
        /// <summary>
        /// 暫定金額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? ZanteiAmount { get; set; }
        /// <summary>
        /// 確定金額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? KakuteiAmount { get; set; }
        /// <summary>
        /// 開始住所
        /// </summary>
        [StringLength(255)]
        public string START_Address { get; set; }
        /// <summary>
        /// 開始住所コード
        /// </summary>
        [StringLength(255)]
        public string START_Address_Code { get; set; }
        /// <summary>
        /// 開始住所レベル
        /// </summary>
        [StringLength(10)]
        public string START_Address_Level { get; set; }
        /// <summary>
        /// 開始経度
        /// </summary>
        [StringLength(100)]
        public string START_Lng { get; set; }
        /// <summary>
        /// 開始緯度
        /// </summary>
        [StringLength(100)]
        public string START_Lat { get; set; }
        /// <summary>
        /// 開始建物名
        /// </summary>
        [StringLength(255)]
        public string START_BuildingName { get; set; }
        /// <summary>
        /// 開始建物ZID
        /// </summary>
        [StringLength(100)]
        public string START_BuildingZid { get; set; }
        /// <summary>
        /// 開始建物名読み
        /// </summary>
        [StringLength(50)]
        public string START_BuildingNameRead { get; set; }
        /// <summary>
        /// 開始ポイント項目タイトル
        /// </summary>
        [StringLength(50)]
        public string START_Point_KoumokuTitle { get; set; }
        /// <summary>
        /// 開始ポイントタイプ
        /// </summary>
        [StringLength(10)]
        public string START_Point_Type { get; set; }
        /// <summary>
        /// 開始ポイント名
        /// </summary>
        [StringLength(50)]
        public string START_PointName { get; set; }
        /// <summary>
        /// 開始ポイント日付
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime? START_PointDate { get; set; }
        /// <summary>
        /// 開始ポイント時間
        /// </summary>
        [StringLength(5)]
        public string START_PointTime { get; set; }
        /// <summary>
        /// 開始ポイント時間区分
        /// </summary>
        [StringLength(20)]
        public int? START_PointTimeKubun { get; set; }
        /// <summary>
        /// 開始ポイントステータス区分
        /// </summary>
        public int? START_PointStatusKubun { get; set; }
        /// <summary>
        /// 現地確認フラグ
        /// </summary>
        public bool? START_FlgGenchiKakunin { get; set; }
        /// <summary>
        /// 開始通行料表示
        /// </summary>
        [StringLength(20)]
        public string START_TollDisplay { get; set; }
        /// <summary>
        /// 開始通行料表示高さ
        /// </summary>
        public double? START_TollDisplayHeight { get; set; }
        /// <summary>
        /// 開始郵便番号
        /// </summary>
        [StringLength(10)]
        public string START_Post_code { get; set; }
        /// <summary>
        /// 開始住所2
        /// </summary>
        [StringLength(50)]
        public string START_Address2 { get; set; }
        /// <summary>
        /// 開始住所3
        /// </summary>
        [StringLength(50)]
        public string START_Address3 { get; set; }
        /// <summary>
        /// 開始住所4
        /// </summary>
        [StringLength(50)]
        public string START_Address4 { get; set; }
        /// <summary>
        /// 開始住所表示
        /// </summary>
        [StringLength(255)]
        public string StartAddressDisplay { get; set; }
        /// <summary>
        /// 開始住所表示2
        /// </summary>
        [StringLength(255)]
        public string StartAddressDisplay2 { get; set; }
        /// <summary>
        /// 開始住所表示3
        /// </summary>
        [StringLength(255)]
        public string StartAddressDisplay3 { get; set; }
        /// <summary>
        /// 開始日時
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime StartDatetime { get; set; }
        /// <summary>
        /// 開始日時表示
        /// </summary>
        [StringLength(50)]
        public string StartDatetimeDisplay { get; set; }
        /// <summary>
        /// 開始ポイント数
        /// </summary>
        public int StartPointCount { get; set; }
        /// <summary>
        /// 終了住所
        /// </summary>
        [StringLength(255)]
        public string END_Address { get; set; }
        /// <summary>
        /// 終了住所コード
        /// </summary>
        [StringLength(255)]
        public string END_Address_Code { get; set; }
        /// <summary>
        /// 終了住所レベル
        /// </summary>
        [StringLength(10)]
        public string END_Address_Level { get; set; }
        /// <summary>
        /// 終了経度
        /// </summary>
        [StringLength(100)]
        public string END_Lng { get; set; }
        /// <summary>
        /// 終了緯度
        /// </summary>
        [StringLength(100)]
        public string END_Lat { get; set; }
        /// <summary>
        /// 終了建物名
        /// </summary>
        [StringLength(255)]
        public string END_BuildingName { get; set; }
        /// <summary>
        /// 終了建物ZID
        /// </summary>
        [StringLength(100)]
        public string END_BuildingZid { get; set; }
        /// <summary>
        /// 終了建物名読み
        /// </summary>
        [StringLength(50)]
        public string END_BuildingNameRead { get; set; }
        /// <summary>
        /// 終了ポイント項目タイトル
        /// </summary>
        [StringLength(50)]
        public string END_Point_KoumokuTitle { get; set; }
        /// <summary>
        /// 終了ポイントタイプ
        /// </summary>
        [StringLength(10)]
        public string END_Point_Type { get; set; }
        /// <summary>
        /// 終了ポイント名
        /// </summary>
        [StringLength(50)]
        public string END_PointName { get; set; }
        /// <summary>
        /// 終了ポイント日付
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime? END_PointDate { get; set; }
        /// <summary>
        /// 終了ポイント時間
        /// </summary>
        [StringLength(5)]
        public string END_PointTime { get; set; }
        /// <summary>
        /// 終了ポイント時間区分
        /// </summary>
        [StringLength(20)]
        public int? END_PointTimeKubun { get; set; }
        /// <summary>
        /// 終了ポイントステータス区分
        /// </summary>
        public int? END_PointStatusKubun { get; set; }
        /// <summary>
        /// 現地確認フラグ
        /// </summary>
        public bool? END_FlgGenchiKakunin { get; set; }
        /// <summary>
        /// 終了通行料表示
        /// </summary>
        [StringLength(20)]
        public string END_TollDisplay { get; set; }
        /// <summary>
        /// 終了通行料表示高さ
        /// </summary>
        public double? END_TollDisplayHeight { get; set; }
        /// <summary>
        /// 終了郵便番号
        /// </summary>
        [StringLength(10)]
        public string END_Post_code { get; set; }
        /// <summary>
        /// 終了住所2
        /// </summary>
        [StringLength(50)]
        public string END_Address2 { get; set; }
        /// <summary>
        /// 終了住所3
        /// </summary>
        [StringLength(50)]
        public string END_Address3 { get; set; }
        /// <summary>
        /// 終了住所4
        /// </summary>
        [StringLength(50)]
        public string END_Address4 { get; set; }
        /// <summary>
        /// 終了住所表示
        /// </summary>
        [StringLength(255)]
        public string EndAddressDisplay { get; set; }
        /// <summary>
        /// 終了住所表示2
        /// </summary>
        [StringLength(255)]
        public string EndAddressDisplay2 { get; set; }
        /// <summary>
        /// 終了住所表示3
        /// </summary>
        [StringLength(255)]
        public string EndAddressDisplay3 { get; set; }
        /// <summary>
        /// 終了日時
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime EndDatetime { get; set; }
        /// <summary>
        /// 終了日時表示
        /// </summary>
        [StringLength(50)]
        public string EndDatetimeDisplay { get; set; }
        /// <summary>
        /// 終了ポイント数
        /// </summary>
        public int EndPointCount { get; set; }
        /// <summary>
        /// 配車計画区分
        /// </summary>
        public int HaisyaPlanKubun { get; set; }
        /// <summary>
        /// 公開グループID
        /// </summary>
        public int? PublishGroup_ID { get; set; }
        /// <summary>
        /// 公開フラグ
        /// </summary>
        public bool? Publish_Flg { get; set; }
        /// <summary>
        /// 公開開始日時
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? Publish_FromDatetime { get; set; }
        /// <summary>
        /// 公開終了日時
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? Publish_ToDatetime { get; set; }
        /// <summary>
        /// 通知
        /// </summary>
        [StringLength(100)]
        public string Notice { get; set; }
        /// <summary>
        /// 通行料区分
        /// </summary>
        public int Toll_Kubun { get; set; }
        /// <summary>
        /// 通行料金額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal Toll_Money { get; set; }
        /// <summary>
        /// 通行料備考
        /// </summary>
        [StringLength(255)]
        public string Toll_Remarks { get; set; }
    }
}
