using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 案件詳細を表すクラス
    /// </summary>
    public partial class VAnkenDetail
    {
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
        /// 配車日
        /// </summary>
        public DateTime? HaisyaDay { get; set; }
    }
}
