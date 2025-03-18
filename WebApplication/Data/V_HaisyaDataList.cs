using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WebApplication.Data
{
    /// <summary>
    /// 配車データリストを表します。
    /// </summary>
    [Keyless]
    [Table("V_HaisyaDataList")]
    public partial class V_HaisyaDataList
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
        public int Anken_Status { get; set; }
        public int Anken_Latest_Order { get; set; }
        public int Anken_Kubun { get; set; }
        public int SenzokuID { get; set; }
        public int Senzoku_Driver_ID { get; set; }
        public int? PublishGroup_ID { get; set; }
        public bool? Publish_Flg { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Publish_FromDatetime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Publish_ToDatetime { get; set; }
        public int Anken_ID { get; set; }
        public int Anken_Order { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        public int Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Update_Datetime { get; set; }
        public int Update_User { get; set; }
        public int Reg_Kubun { get; set; }
        public int TantouID { get; set; }
        public int EigyoID { get; set; }
        public int KokyakuId { get; set; }
        [StringLength(50)]
        public string KokyakuCode { get; set; }
        [StringLength(100)]
        public string KokyakuName { get; set; }
        public int KokyakuTantouId { get; set; }
        [StringLength(50)]
        public string KokyakuTantouName { get; set; }
        [StringLength(50)]
        public string KokyakuTantouPhone { get; set; }
        [StringLength(100)]
        public string Work_Name { get; set; }
        public int Syaryo_ID { get; set; }
        [StringLength(20)]
        public string Syasyu { get; set; }
        [StringLength(20)]
        public string SyasyuSize { get; set; }
        [StringLength(50)]
        public string SyasyuDisplay { get; set; }
        [StringLength(20)]
        public string Kata { get; set; }
        public int? Daisuu { get; set; }
        [StringLength(30)]
        public string Root_Ferry { get; set; }
        [StringLength(30)]
        public string Root_Regulation { get; set; }
        [StringLength(30)]
        public string Root_Twouturn { get; set; }
        public bool Root_EigyoshoModori { get; set; }
        public int? SeikyuKubun { get; set; }
        public int? NumberCommLimitKubun { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? NumberCommLimitDateTime { get; set; }
        [StringLength(255)]
        public string RouteID { get; set; }
        public int? RouteType { get; set; }
        [StringLength(20)]
        public string RouteTypeDisplay { get; set; }
        [StringLength(10)]
        public string Route_TotalTime { get; set; }
        public int? Route_BreakTime { get; set; }
        public int? Route_RestTime { get; set; }
        public double? Route_TotalDistance { get; set; }
        [Column(TypeName = "money")]
        public decimal? Route_FuelConsume { get; set; }
        [Column(TypeName = "money")]
        public decimal? Route_Totaltoll { get; set; }
        [StringLength(20)]
        public string Route_RestTimeDisplay { get; set; }
        [Column(TypeName = "money")]
        public decimal? Route_StdFreight { get; set; }
        [Column(TypeName = "money")]
        public decimal? Route_StdExcharge { get; set; }
        [Column(TypeName = "money")]
        public decimal? Route_StdALLFreight { get; set; }
        [Column(TypeName = "money")]
        public decimal? Route_StdTotalFreight { get; set; }
        [Column(TypeName = "money")]
        public decimal? Route_GrossAmountForLaborCost { get; set; }
        [Column(TypeName = "money")]
        public decimal? Route_GrossAmountForFuelCost { get; set; }
        [Column(TypeName = "money")]
        public decimal? Route_GrossAmountForSyaryoCost { get; set; }
        [Column(TypeName = "money")]
        public decimal? Route_GrossAmountForLuggage { get; set; }
        [Column(TypeName = "money")]
        public decimal? Route_GrossAmountForExcharge { get; set; }
        [Column(TypeName = "money")]
        public decimal? Route_GrossAmount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Route_GrossAmountTotal { get; set; }
        public int? Route_TotalDays { get; set; }
        [Column(TypeName = "money")]
        public decimal? BaseFee { get; set; }
        [Column(TypeName = "money")]
        public decimal? ExtraCharge { get; set; }
        [Column(TypeName = "money")]
        public decimal? Toll { get; set; }
        [Column(TypeName = "money")]
        public decimal? Discount { get; set; }
        [Column(TypeName = "money")]
        public decimal? GrossAmount { get; set; }
        public double? Height { get; set; }
        public double? Width { get; set; }
        public double? Weight { get; set; }
        public double? Nenpi { get; set; }
        [StringLength(10)]
        public string TsumiTaskTime { get; set; }
        [StringLength(10)]
        public string OroshiTaskTime { get; set; }
        public bool CheckOroshiSpace { get; set; }
        public bool EdnGoBackEigyosyo { get; set; }
        public int HaisyaPlanKubun { get; set; }
        [StringLength(100)]
        public string LuggageDisplay { get; set; }
        [StringLength(100)]
        public string EquipmentDisplay { get; set; }
        [StringLength(50)]
        public string SyabanRenraku_Remarks { get; set; }
        public int Area { get; set; }
        public int DriverGrossCalc { get; set; }
        public int Toll_Kubun { get; set; }
        [Column(TypeName = "money")]
        public decimal Toll_Money { get; set; }
        [StringLength(255)]
        public string Toll_Remarks { get; set; }
        [StringLength(100)]
        public string Notice { get; set; }
        [StringLength(40)]
        public string SyasyuDisplay2 { get; set; }
        [StringLength(56)]
        public string SyasyuDaisuDisplay { get; set; }
        [StringLength(50)]
        public string Tantou_Name { get; set; }
        [StringLength(50)]
        public string Eigyo_Name { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
        [StringLength(100)]
        public string Luggage { get; set; }
        [StringLength(100)]
        public string Equipment { get; set; }
        [StringLength(50)]
        public string Customer_Name_Abbr { get; set; }
        [StringLength(14)]
        public string AnkenStatusDisplay { get; set; }
        [Required]
        [StringLength(4)]
        public string AnkenStep { get; set; }
        [Column(TypeName = "money")]
        public decimal? ZanteiAmount { get; set; }
        [Column(TypeName = "money")]
        public decimal? KakuteiAmount { get; set; }
        public int AnkenDisplay_ID { get; set; }
        public int Daisuu_Sort { get; set; }
        public int Anken_Key { get; set; }
        [Column(TypeName = "date")]
        public DateTime Day { get; set; }
        public int Display_Kubun { get; set; }
        [Required]
        [StringLength(40)]
        public string FULL_SYABAN { get; set; }
        [Required]
        [StringLength(40)]
        public string SYABAN { get; set; }
        [StringLength(40)]
        public string Syasyu_Disp { get; set; }

        public int? Employee_Number { get; set; }
        public int? Driver_ID { get; set; }
        [StringLength(50)]
        public string Driver_Name { get; set; }
        public int? DriverSyaryo_ID { get; set; }
        public int? SyaryoManagement_ID { get; set; }
        public int? SyaryoManagement_ID1 { get; set; }
        public int? Driver_Branch_ID { get; set; }
        [StringLength(50)]
        public string Driver_Branch_Name { get; set; }
        [StringLength(30)]
        public string Driver_Phone { get; set; }

        [StringLength(50)]
        public string Driver_Yosya_Tantou_Name { get; set; }

        public int? Yosya_Count { get; set; }
        public string Yosya_Display1 { get; set; }
        public string Yosya_Display2 { get; set; }
        public string Yosya_Display3 { get; set; }

        public int? Haisya_ID { get; set; }
        public int? Haisya_Status { get; set; }
        public int? Haisya_Kubun { get; set; }
        [StringLength(8)]
        public string HaisyaKubunDisplay { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDatetime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EndDatetime { get; set; }
        public int Start_Point_Kubun { get; set; }
        [StringLength(255)]
        public string Start_BuildingName { get; set; }
        [StringLength(100)]
        public string Start_BuildingZid { get; set; }
        [StringLength(100)]
        public string Start_BuildingZid_Attr { get; set; }
        [StringLength(50)]
        public string Start_BuildingNameRead { get; set; }
        [StringLength(50)]
        public string Start_Point_KoumokuTitle { get; set; }
        [StringLength(10)]
        public string Start_Point_Type { get; set; }
        [StringLength(50)]
        public string Start_PointName { get; set; }
        [StringLength(100)]
        public string Start_Lng { get; set; }
        [StringLength(100)]
        public string Start_Lat { get; set; }
        [StringLength(10)]
        public string Start_Post_code { get; set; }
        [StringLength(255)]
        public string Start_Address { get; set; }
        [StringLength(255)]
        public string Start_Address2 { get; set; }
        [StringLength(255)]
        public string Start_Address3 { get; set; }
        [StringLength(255)]
        public string Start_Address4 { get; set; }
        public int End_Point_Kubun { get; set; }
        [StringLength(255)]
        public string End_BuildingName { get; set; }
        [StringLength(100)]
        public string End_BuildingZid { get; set; }
        [StringLength(100)]
        public string End_BuildingZid_Attr { get; set; }
        [StringLength(50)]
        public string End_BuildingNameRead { get; set; }
        [StringLength(50)]
        public string End_Point_KoumokuTitle { get; set; }
        [StringLength(10)]
        public string End_Point_Type { get; set; }
        [StringLength(50)]
        public string End_PointName { get; set; }
        [StringLength(100)]
        public string End_Lng { get; set; }
        [StringLength(100)]
        public string End_Lat { get; set; }
        [StringLength(10)]
        public string End_Post_code { get; set; }
        [StringLength(255)]
        public string End_Address { get; set; }
        [StringLength(255)]
        public string End_Address2 { get; set; }
        [StringLength(255)]
        public string End_Address3 { get; set; }
        [StringLength(255)]
        public string End_Address4 { get; set; }
        [StringLength(50)]
        public string Display1 { get; set; }
        [StringLength(50)]
        public string Display2 { get; set; }
        [StringLength(512)]
        public string StartAddressDisplay { get; set; }
        [Required]
        [StringLength(765)]
        public string StartAddressDisplay2 { get; set; }
        [StringLength(510)]
        public string StartAddressDisplay3 { get; set; }
        [StringLength(10)]
        public string Area_Disp { get; set; }
        public int? Area_Sort_Order { get; set; }
        [StringLength(512)]
        public string EndAddressDisplay { get; set; }
        [Required]
        [StringLength(765)]
        public string EndAddressDisplay2 { get; set; }
        [StringLength(510)]
        public string EndAddressDisplay3 { get; set; }
        public int NippouStatus { get; set; }
        public int Renraku_Kubun { get; set; }
        public int RenkeiStatus { get; set; }

        /// <summary>
        /// 荷物の重量
        /// </summary>
        public double Luggage_Weight { get; set; }
    }
}
