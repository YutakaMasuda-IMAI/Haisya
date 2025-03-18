using RenkeiDB.Common;
using System.ComponentModel.DataAnnotations;

namespace RenkeiDB.Dto.AnkenDto
{
    /// <summary>
    /// 案件更新情報を表すDTO
    /// </summary>
    public class UpdateAnkenDto
    {
#pragma warning disable IDE1006 // Naming Styles
        [Display(Name = "案件名")]
        public string workName { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "車種")]
        public int? syasyu { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "台数")]
        public int? daisuu { get; set; }

        [Display(Name = "積み時間")]
        public string tsumiTaskTime { get; set; }

        [Display(Name = "卸し時間")]
        public string oroshiTaskTime { get; set; }

        [Display(Name = "ルート種別")]
        public string routeTypeDisplay { get; set; }

        [Display(Name = "所要時間")]
        public string routeTotalTime { get; set; }

        [Display(Name = "距離(km)")]
        public double? routeTotalDistance { get; set; }

        [Display(Name = "有料道路（料金）")]
        public int? routeTotalToll { get; set; }

        [Display(Name = "見積原価")]
        public int? routeGrossAmount { get; set; }

        [Display(Name = "標準運賃")]
        public int? routeStgFreight { get; set; }

        [Display(Name = "運賃区分")]
        public int? seikyuKubun { get; set; }

        [Display(Name = "基本運賃")]
        public int? baseFee { get; set; }

        [Display(Name = "追加費用")]
        public int? extraCharge { get; set; }

        [Display(Name = "有料道路")]
        public int? toll { get; set; }

        [Display(Name = "支払運賃")]
        public int? grossAmount { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "高速代")]
        public int? tollKubun { get; set; }

        [Display(Name = "金額入力")]
        public int? tollMoney { get; set; }

        [Display(Name = "荷物情報")]
        public string luggageDisplay { get; set; }

        [Display(Name = "装備品情報")]
        public string equipmentDisplay { get; set; }

        [Display(Name = "特記事項")]
        public string syabanrenrakuRemarks { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "宵済み案件")]
        public bool? rootEigyoshoModori { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "卸し場所の詳細は積み地で確認")]
        public bool? checkOroshiSpace { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "業務御に営業所戻り")]
        public bool? ednGoBackEigyosyo { get; set; }

        [Display(Name = "高速備考")]
        public string tollRemarks { get; set; }

        [Display(Name = "荷物重量")]
        public double? luggageWeight { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        public UpdateAnkenPointDto[] ankenPoints { get; set; }
        public UpdateAnkenLuggageDto[] ankenLuggages { get; set; }
        public UpdateAnkenEquipmentDto[] ankenEquipments { get; set; }
        public decimal discount { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
