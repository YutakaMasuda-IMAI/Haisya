using System;
using System.Text.Json.Serialization;

namespace SeikyuWeb.Models.ReportCommons
{
    /// <summary>
    /// クラスV_AnkenDataListは案件データリスト用のモデル
    /// </summary>
    public class V_AnkenDataList
    {
        [JsonPropertyName("Anken_No")]
        public string Anken_No { get; set; }
        [JsonPropertyName("Anken_Status")]
        public string Anken_Status { get; set; }
        [JsonPropertyName("Anken_Latest_Order")]
        public string Anken_Latest_Order { get; set; }
        [JsonPropertyName("Anken_Kubun")]
        public string Anken_Kubun { get; set; }
        [JsonPropertyName("SenzokuID")]
        public string SenzokuID { get; set; }
        [JsonPropertyName("Senzoku_Driver_ID")]
        public string Senzoku_Driver_ID { get; set; }
        [JsonPropertyName("PublishGroup_ID")]
        public string PublishGroup_ID { get; set; }
        [JsonPropertyName("Publish_Flg")]
        public string Publish_Flg { get; set; }
        [JsonPropertyName("Publish_FromDatetime")]
        public string Publish_FromDatetime { get; set; }
        [JsonPropertyName("Publish_ToDatetime")]
        public string Publish_ToDatetime { get; set; }
        [JsonPropertyName("Anken_ID")]
        public int Anken_ID { get; set; }
        [JsonPropertyName("Anken_Order")]
        public int Anken_Order { get; set; }
        [JsonPropertyName("Insert_Datetime")]
        public DateTime Insert_Datetime { get; set; }
        [JsonPropertyName("Insert_User")]
        public int Insert_User { get; set; }
        [JsonPropertyName("Update_Datetime")]
        public DateTime Update_Datetime { get; set; }
        [JsonPropertyName("Update_User")]
        public int Update_User { get; set; }
        [JsonPropertyName("Reg_Kubun")]
        public int Reg_Kubun { get; set; }
        [JsonPropertyName("TantouID")]
        public int TantouID { get; set; }
        [JsonPropertyName("EigyoID")]
        public int EigyoID { get; set; }
        [JsonPropertyName("KokyakuId")]
        public int KokyakuId { get; set; }
        [JsonPropertyName("KokyakuCode")]
        public string KokyakuCode { get; set; }
        [JsonPropertyName("KokyakuName")]
        public string KokyakuName { get; set; }
        [JsonPropertyName("KokyakuTantouId")]
        public int KokyakuTantouId { get; set; }
        [JsonPropertyName("KokyakuTantouName")]
        public string KokyakuTantouName { get; set; }
        [JsonPropertyName("KokyakuTantouPhone")]
        public string KokyakuTantouPhone { get; set; }
        [JsonPropertyName("Work_Name")]
        public string Work_Name { get; set; }
        [JsonPropertyName("Syaryo_ID")]
        public int Syaryo_ID { get; set; }
        [JsonPropertyName("Syasyu")]
        public string Syasyu { get; set; }
        [JsonPropertyName("SyasyuSize")]
        public string SyasyuSize { get; set; }
        [JsonPropertyName("SyasyuDisplay")]
        public string SyasyuDisplay { get; set; }
        [JsonPropertyName("Kata")]
        public string Kata { get; set; }
        [JsonPropertyName("Daisuu")]
        public int Daisuu { get; set; }
        [JsonPropertyName("Root_Ferry")]
        public string Root_Ferry { get; set; }
        [JsonPropertyName("Root_Regulation")]
        public string Root_Regulation { get; set; }
        [JsonPropertyName("Root_Twouturn")]
        public string Root_Twouturn { get; set; }
        [JsonPropertyName("Root_EigyoshoModori")]
        public bool Root_EigyoshoModori { get; set; }
        [JsonPropertyName("SeikyuKubun")]
        public int SeikyuKubun { get; set; }
        [JsonPropertyName("NumberCommLimitKubun")]
        public int NumberCommLimitKubun { get; set; }
        [JsonPropertyName("NumberCommLimitDateTime")]
        public DateTime NumberCommLimitDateTime { get; set; }
        [JsonPropertyName("RouteID")]
        public string RouteID { get; set; }
        [JsonPropertyName("RouteType")]
        public int RouteType { get; set; }
        [JsonPropertyName("RouteTypeDisplay")]
        public string RouteTypeDisplay { get; set; }
        [JsonPropertyName("Route_TotalTime")]
        public string Route_TotalTime { get; set; }
        [JsonPropertyName("Route_BreakTime")]
        public int Route_BreakTime { get; set; }
        [JsonPropertyName("Route_RestTime")]
        public int Route_RestTime { get; set; }
        [JsonPropertyName("Route_TotalDistance")]
        public double Route_TotalDistance { get; set; }
        [JsonPropertyName("Route_FuelConsume")]
        public decimal Route_FuelConsume { get; set; }
        [JsonPropertyName("Route_Totaltoll")]
        public decimal Route_Totaltoll { get; set; }
        [JsonPropertyName("Route_RestTimeDisplay")]
        public string Route_RestTimeDisplay { get; set; }
        [JsonPropertyName("Route_StdFreight")]
        public decimal Route_StdFreight { get; set; }
        [JsonPropertyName("Route_StdExcharge")]
        public decimal Route_StdExcharge { get; set; }
        [JsonPropertyName("Route_StdALLFreight")]
        public decimal Route_StdALLFreight { get; set; }
        [JsonPropertyName("Route_StdTotalFreight")]
        public decimal Route_StdTotalFreight { get; set; }
        [JsonPropertyName("Route_GrossAmountForLaborCost")]
        public decimal Route_GrossAmountForLaborCost { get; set; }
        [JsonPropertyName("Route_GrossAmountForFuelCost")]
        public decimal Route_GrossAmountForFuelCost { get; set; }
        [JsonPropertyName("Route_GrossAmountForSyaryoCost")]
        public decimal Route_GrossAmountForSyaryoCost { get; set; }
        [JsonPropertyName("Route_GrossAmountForLuggage")]
        public decimal Route_GrossAmountForLuggage { get; set; }
        [JsonPropertyName("Route_GrossAmountForExcharge")]
        public decimal Route_GrossAmountForExcharge { get; set; }
        [JsonPropertyName("Route_GrossAmount")]
        public decimal Route_GrossAmount { get; set; }
        [JsonPropertyName("Route_GrossAmountTotal")]
        public decimal Route_GrossAmountTotal { get; set; }
        [JsonPropertyName("Route_TotalDays")]
        public int Route_TotalDays { get; set; }
        [JsonPropertyName("BaseFee")]
        public decimal BaseFee { get; set; }
        [JsonPropertyName("ExtraCharge")]
        public decimal ExtraCharge { get; set; }
        [JsonPropertyName("Toll")]
        public decimal Toll { get; set; }
        [JsonPropertyName("Discount")]
        public decimal Discount { get; set; }
        [JsonPropertyName("GrossAmount")]
        public decimal GrossAmount { get; set; }
        [JsonPropertyName("Height")]
        public double Height { get; set; }
        [JsonPropertyName("Width")]
        public double Width { get; set; }
        [JsonPropertyName("Weight")]
        public double Weight { get; set; }
        [JsonPropertyName("Nenpi")]
        public double Nenpi { get; set; }
        [JsonPropertyName("TsumiTaskTime")]
        public string TsumiTaskTime { get; set; }
        [JsonPropertyName("OroshiTaskTime")]
        public string OroshiTaskTime { get; set; }
        [JsonPropertyName("CheckOroshiSpace")]
        public bool CheckOroshiSpace { get; set; }
        [JsonPropertyName("EdnGoBackEigyosyo")]
        public bool EdnGoBackEigyosyo { get; set; }
        [JsonPropertyName("HaisyaPlanKubun")]
        public int HaisyaPlanKubun { get; set; }
        [JsonPropertyName("HaisyaDriverID")]
        public int HaisyaDriverID { get; set; }
        [JsonPropertyName("HaisyaDriverSyaryoID")]
        public int HaisyaDriverSyaryoID { get; set; }
        [JsonPropertyName("HaisyaDriverDisplay")]
        public string HaisyaDriverDisplay { get; set; }
        [JsonPropertyName("LuggageDisplay")]
        public string LuggageDisplay { get; set; }
        [JsonPropertyName("EquipmentDisplay")]
        public string EquipmentDisplay { get; set; }
        [JsonPropertyName("SyabanRenraku_Remarks")]
        public string SyabanRenraku_Remarks { get; set; }
        [JsonPropertyName("Area")]
        public int? Area { get; set; }
        [JsonPropertyName("DriverGrossCalc")]
        public int? DriverGrossCalc { get; set; }
        [JsonPropertyName("Toll_Kubun")]
        public int? Toll_Kubun { get; set; }
        [JsonPropertyName("Toll_Money")]
        public decimal? Toll_Money { get; set; }
        [JsonPropertyName("Toll_Remarks")]
        public string Toll_Remarks { get; set; }
        [JsonPropertyName("Notice")]
        public string Notice { get; set; }
        [JsonPropertyName("Luggage_Weight")]
        public double Luggage_Weight { get; set; }     
        [JsonPropertyName("SyasyuDisplay2")]
        public string SyasyuDisplay2 { get; set; }
        [JsonPropertyName("SyasyuDaisuDisplay")]
        public string SyasyuDaisuDisplay { get; set; }
        [JsonPropertyName("Tantou_Name")]
        public string Tantou_Name { get; set; }
        [JsonPropertyName("Eigyo_Name")]
        public string Eigyo_Name { get; set; }
        [JsonPropertyName("Remarks")]
        public string Remarks { get; set; }
        [JsonPropertyName("Luggage")]
        public string Luggage { get; set; }
        [JsonPropertyName("Equipment")]
        public string Equipment { get; set; }
        [JsonPropertyName("Customer_Name_Abbr")]
        public string Customer_Name_Abbr { get; set; }
        [JsonPropertyName("AnkenStatusDisplay")]
        public string AnkenStatusDisplay { get; set; }
        [JsonPropertyName("AnkenStep")]
        public string AnkenStep { get; set; }
        [JsonPropertyName("ZanteiAmount")]
        public string ZanteiAmount { get; set; }
        [JsonPropertyName("KakuteiAmount")]
        public string KakuteiAmount { get; set; }
        [JsonPropertyName("START_Address")]
        public string START_Address { get; set; }
        [JsonPropertyName("START_Address_Code")]
        public string START_Address_Code { get; set; }
        [JsonPropertyName("START_Address_Level")]
        public string START_Address_Level { get; set; }
        [JsonPropertyName("START_Lng")]
        public string START_Lng { get; set; }
        [JsonPropertyName("START_BuildingName")]
        public string START_BuildingName { get; set; }
        [JsonPropertyName("START_BuildingZid")]
        public string START_BuildingZid { get; set; }
        [JsonPropertyName("START_BuildingNameRead")]
        public string START_BuildingNameRead { get; set; }
        [JsonPropertyName("START_Point_KoumokuTitle")]
        public string START_Point_KoumokuTitle { get; set; }
        [JsonPropertyName("START_Point_Type")]
        public string START_Point_Type { get; set; }
        [JsonPropertyName("START_PointName")]
        public string START_PointName { get; set; }
        [JsonPropertyName("START_PointDate")]
        public string START_PointDate { get; set; }
        [JsonPropertyName("START_PointTime")]
        public string START_PointTime { get; set; }
        [JsonPropertyName("START_PointTimeKubun")]
        public string START_PointTimeKubun { get; set; }
        [JsonPropertyName("START_PointStatusKubun")]
        public string START_PointStatusKubun { get; set; }
        [JsonPropertyName("START_FlgGenchiKakunin")]
        public string START_FlgGenchiKakunin { get; set; }
        [JsonPropertyName("START_TollDisplay")]
        public string START_TollDisplay { get; set; }
        [JsonPropertyName("START_TollDisplayHeight")]
        public string START_TollDisplayHeight { get; set; }
        [JsonPropertyName("START_Post_code")]
        public string START_Post_code { get; set; }
        [JsonPropertyName("START_Address2")]
        public string START_Address2 { get; set; }
        [JsonPropertyName("START_Address3")]
        public string START_Address3 { get; set; }
        [JsonPropertyName("START_Address4")]
        public string START_Address4 { get; set; }
        [JsonPropertyName("StartAddressDisplay")]
        public string StartAddressDisplay { get; set; }
        [JsonPropertyName("StartAddressDisplay2")]
        public string StartAddressDisplay2 { get; set; }
        [JsonPropertyName("StartAddressDisplay3")]
        public string StartAddressDisplay3 { get; set; }
        [JsonPropertyName("StartDatetime")]
        public string StartDatetime { get; set; }
        [JsonPropertyName("StartDatetimeDisplay")]
        public string StartDatetimeDisplay { get; set; }
        [JsonPropertyName("StartPointCount")]
        public string StartPointCount { get; set; }
        [JsonPropertyName("END_Address")]
        public string END_Address { get; set; }
        [JsonPropertyName("END_Address_Code")]
        public string END_Address_Code { get; set; }
        [JsonPropertyName("END_Address_Level")]
        public string END_Address_Level { get; set; }
        [JsonPropertyName("END_Lng")]
        public string END_Lng { get; set; }
        [JsonPropertyName("END_Lat")]
        public string END_Lat { get; set; }
        [JsonPropertyName("END_BuildingName")]
        public string END_BuildingName { get; set; }
        [JsonPropertyName("END_BuildingZid")]
        public string END_BuildingZid { get; set; }
        [JsonPropertyName("END_BuildingNameRead")]
        public string END_BuildingNameRead { get; set; }
        [JsonPropertyName("END_Point_KoumokuTitle")]
        public string END_Point_KoumokuTitle { get; set; }
        [JsonPropertyName("END_Point_Type")]
        public string END_Point_Type { get; set; }
        [JsonPropertyName("END_PointName")]
        public string END_PointName { get; set; }
        [JsonPropertyName("END_PointDate")]
        public string END_PointDate { get; set; }
        [JsonPropertyName("END_PointTime")]
        public string END_PointTime { get; set; }
        [JsonPropertyName("END_PointTimeKubun")]
        public string END_PointTimeKubun { get; set; }
        [JsonPropertyName("END_PointStatusKubun")]
        public string END_PointStatusKubun { get; set; }
        [JsonPropertyName("END_FlgGenchiKakunin")]
        public string END_FlgGenchiKakunin { get; set; }
        [JsonPropertyName("END_TollDisplay")]
        public string END_TollDisplay { get; set; }
        [JsonPropertyName("END_TollDisplayHeight")]
        public string END_TollDisplayHeight { get; set; }
        [JsonPropertyName("END_Post_code")]
        public string END_Post_code { get; set; }
        [JsonPropertyName("END_Address2")]
        public string END_Address2 { get; set; }
        [JsonPropertyName("END_Address3")]
        public string END_Address3 { get; set; }
        [JsonPropertyName("END_Address4")]
        public string END_Address4 { get; set; }
        [JsonPropertyName("EndAddressDisplay")]
        public string EndAddressDisplay { get; set; }
        [JsonPropertyName("EndAddressDisplay2")]
        public string EndAddressDisplay2 { get; set; }
        [JsonPropertyName("EndAddressDisplay3")]
        public string EndAddressDisplay3 { get; set; }
        [JsonPropertyName("EndDatetime")]
        public string EndDatetime { get; set; }
        [JsonPropertyName("EndDatetimeDisplay")]
        public string EndDatetimeDisplay { get; set; }
        [JsonPropertyName("EndPointCount")]
        public string EndPointCount { get; set; }

    }
}
