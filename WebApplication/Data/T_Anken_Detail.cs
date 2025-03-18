using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Anken_Detail")]
    public partial class T_Anken_Detail
    {
        [Key]
        public int Anken_ID { get; set; }
        [Key]
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
        public int HaisyaDriverID { get; set; }
        public int HaisyaDriverSyaryoID { get; set; }
        [StringLength(50)]
        public string HaisyaDriverDisplay { get; set; }
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
        public double Luggage_Weight { get; set; }
    }
}
