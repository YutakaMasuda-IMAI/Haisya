using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 配車データリスト（旧）を表すクラス
    /// </summary>
    public partial class VHaisyaDataListOld
    {
        /// <summary>
        /// 選択行
        /// </summary>
        public int SelectRow { get; set; }
        /// <summary>
        /// 案件番号
        /// </summary>
        public string AnkenNo { get; set; }
        /// <summary>
        /// 案件ステータス
        /// </summary>
        public int AnkenStatus { get; set; }
        /// <summary>
        /// 案件最新順序
        /// </summary>
        public int AnkenLatestOrder { get; set; }
        /// <summary>
        /// 案件区分
        /// </summary>
        public int AnkenKubun { get; set; }
        /// <summary>
        /// 専属ID
        /// </summary>
        public int SenzokuId { get; set; }
        /// <summary>
        /// 専属ドライバーID
        /// </summary>
        public int SenzokuDriverId { get; set; }
        /// <summary>
        /// 公開グループID
        /// </summary>
        public int? PublishGroupId { get; set; }
        /// <summary>
        /// 公開フラグ
        /// </summary>
        public bool? PublishFlg { get; set; }
        /// <summary>
        /// 公開開始日時
        /// </summary>
        public DateTime? PublishFromDatetime { get; set; }
        /// <summary>
        /// 公開終了日時
        /// </summary>
        public DateTime? PublishToDatetime { get; set; }
        /// <summary>
        /// 案件ID
        /// </summary>
        public int AnkenId { get; set; }
        /// <summary>
        /// 案件順序
        /// </summary>
        public int AnkenOrder { get; set; }
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
        /// 登録区分
        /// </summary>
        public int RegKubun { get; set; }
        /// <summary>
        /// 担当者ID
        /// </summary>
        public int TantouId { get; set; }
        /// <summary>
        /// 営業所ID
        /// </summary>
        public int EigyoId { get; set; }
        /// <summary>
        /// 顧客ID
        /// </summary>
        public int KokyakuId { get; set; }
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
        public int KokyakuTantouId { get; set; }
        /// <summary>
        /// 顧客担当者名
        /// </summary>
        public string KokyakuTantouName { get; set; }
        /// <summary>
        /// 顧客担当者電話番号
        /// </summary>
        public string KokyakuTantouPhone { get; set; }
        /// <summary>
        /// 作業名
        /// </summary>
        public string WorkName { get; set; }
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
        /// ルート二回転
        /// </summary>
        public string RootTwouturn { get; set; }
        /// <summary>
        /// ルート営業所戻り
        /// </summary>
        public bool RootEigyoshoModori { get; set; }
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
        public DateTime? NumberCommLimitDateTime { get; set; }
        /// <summary>
        /// ルートID
        /// </summary>
        public string RouteId { get; set; }
        /// <summary>
        /// ルートタイプ
        /// </summary>
        public int? RouteType { get; set; }
        /// <summary>
        /// ルートタイプ表示
        /// </summary>
        public string RouteTypeDisplay { get; set; }
        /// <summary>
        /// ルート総時間
        /// </summary>
        public string RouteTotalTime { get; set; }
        /// <summary>
        /// ルート休憩時間
        /// </summary>
        public int? RouteBreakTime { get; set; }
        /// <summary>
        /// ルート休息時間
        /// </summary>
        public int? RouteRestTime { get; set; }
        /// <summary>
        /// ルート総距離
        /// </summary>
        public double? RouteTotalDistance { get; set; }
        /// <summary>
        /// ルート燃料消費
        /// </summary>
        public decimal? RouteFuelConsume { get; set; }
        /// <summary>
        /// ルート総通行料
        /// </summary>
        public decimal? RouteTotaltoll { get; set; }
        /// <summary>
        /// ルート休息時間表示
        /// </summary>
        public string RouteRestTimeDisplay { get; set; }
        /// <summary>
        /// ルート標準運賃
        /// </summary>
        public decimal? RouteStdFreight { get; set; }
        /// <summary>
        /// ルート標準追加料金
        /// </summary>
        public decimal? RouteStdExcharge { get; set; }
        /// <summary>
        /// ルート標準全運賃
        /// </summary>
        public decimal? RouteStdAllfreight { get; set; }
        /// <summary>
        /// ルート標準総運賃
        /// </summary>
        public decimal? RouteStdTotalFreight { get; set; }
        /// <summary>
        /// ルート労働コスト総額
        /// </summary>
        public decimal? RouteGrossAmountForLaborCost { get; set; }
        /// <summary>
        /// ルート燃料コスト総額
        /// </summary>
        public decimal? RouteGrossAmountForFuelCost { get; set; }
        /// <summary>
        /// ルート車両コスト総額
        /// </summary>
        public decimal? RouteGrossAmountForSyaryoCost { get; set; }
        /// <summary>
        /// ルート荷物コスト総額
        /// </summary>
        public decimal? RouteGrossAmountForLuggage { get; set; }
        /// <summary>
        /// ルート追加料金総額
        /// </summary>
        public decimal? RouteGrossAmountForExcharge { get; set; }
        /// <summary>
        /// ルート総額
        /// </summary>
        public decimal? RouteGrossAmount { get; set; }
        /// <summary>
        /// ルート総額合計
        /// </summary>
        public decimal? RouteGrossAmountTotal { get; set; }
        /// <summary>
        /// ルート総日数
        /// </summary>
        public int? RouteTotalDays { get; set; }
        /// <summary>
        /// 基本料金
        /// </summary>
        public decimal? BaseFee { get; set; }
        /// <summary>
        /// 追加料金
        /// </summary>
        public decimal? ExtraCharge { get; set; }
        /// <summary>
        /// 通行料
        /// </summary>
        public decimal? Toll { get; set; }
        /// <summary>
        /// 割引
        /// </summary>
        public decimal? Discount { get; set; }
        /// <summary>
        /// 総額
        /// </summary>
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
        /// 積み作業時間
        /// </summary>
        public string TsumiTaskTime { get; set; }
        /// <summary>
        /// 降ろし作業時間
        /// </summary>
        public string OroshiTaskTime { get; set; }
        /// <summary>
        /// 降ろしスペース確認
        /// </summary>
        public bool CheckOroshiSpace { get; set; }
        /// <summary>
        /// 終了後営業所に戻る
        /// </summary>
        public bool EdnGoBackEigyosyo { get; set; }
        /// <summary>
        /// 配車計画区分
        /// </summary>
        public int HaisyaPlanKubun { get; set; }
        /// <summary>
        /// 配車ドライバーID
        /// </summary>
        public int HaisyaDriverId { get; set; }
        /// <summary>
        /// 配車ドライバー車両ID
        /// </summary>
        public int HaisyaDriverSyaryoId { get; set; }
        /// <summary>
        /// 配車ドライバー表示
        /// </summary>
        public string HaisyaDriverDisplay { get; set; }
        /// <summary>
        /// 荷物表示
        /// </summary>
        public string LuggageDisplay { get; set; }
        /// <summary>
        /// 装備表示
        /// </summary>
        public string EquipmentDisplay { get; set; }
        /// <summary>
        /// 車番連絡備考
        /// </summary>
        public string SyabanRenrakuRemarks { get; set; }
        /// <summary>
        /// エリア
        /// </summary>
        public int Area { get; set; }
        /// <summary>
        /// ドライバー総額計算
        /// </summary>
        public int DriverGrossCalc { get; set; }
        /// <summary>
        /// 通行料区分
        /// </summary>
        public int TollKubun { get; set; }
        /// <summary>
        /// 通行料金額
        /// </summary>
        public decimal TollMoney { get; set; }
        /// <summary>
        /// 通行料備考
        /// </summary>
        public string TollRemarks { get; set; }
        /// <summary>
        /// 注意事項
        /// </summary>
        public string Notice { get; set; }
        /// <summary>
        /// 荷物重量
        /// </summary>
        public double LuggageWeight { get; set; }
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
        public string TantouName { get; set; }
        /// <summary>
        /// 営業所名
        /// </summary>
        public string EigyoName { get; set; }
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
        public string CustomerNameAbbr { get; set; }
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
        public int AnkenDisplayId { get; set; }
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
        /// 車番
        /// </summary>
        public string FillSyaban { get; set; }
        /// <summary>
        /// 車番
        /// </summary>
        public string Syaban { get; set; }
        /// <summary>
        /// ドライバー名
        /// </summary>
        public string DriverName { get; set; }
        /// <summary>
        /// 傭車表示1
        /// </summary>
        public int? YosyaDisplay1 { get; set; }
        /// <summary>
        /// 傭車表示2
        /// </summary>
        public int? YosyaDisplay2 { get; set; }
        /// <summary>
        /// 傭車表示3
        /// </summary>
        public int? YosyaDisplay3 { get; set; }
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
        public string StartAddressDisplay3 { get; set; }
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
        public string EndAddressDisplay3 { get; set; }
    }
}
