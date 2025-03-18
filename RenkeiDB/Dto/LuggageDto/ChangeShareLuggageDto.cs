using RenkeiDB.Common;
using RenkeiDB.Dto.ValidateRules;
using System.ComponentModel.DataAnnotations;

namespace RenkeiDB.Dto.LuggageDto
{
    /// <summary>
    /// 荷物共有変更情報を表すDTO
    /// </summary>
    public class ChangeShareLuggageDto
    {
#pragma warning disable IDE1006 // Naming Styles
        [MaxLength(100, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "荷主")]
        public string kokyakuName { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "荷主名公開設定")]
        public bool? kokyakuPublicFlg { get; set; }

        [MaxLength(100, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "元請け")]
        public string primeContractor { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "金額")]
        public decimal? unchin { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "通行区分")]
        public int? tollKubun { get; set; }

        [Display(Name = "指定額")]
        [RequiredIf("tollKubun", 3)]
        [SetNullIfNot("tollKubun", 3)]
        public decimal? tollMoney { get; set; }

        [CustomDateFormat("yyyy/MM/dd HH:mm:ss")]
        [Display(Name = "積み日")]
        public string tumiDatetime { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "時間区分")]
        public int? tumiTimeKubun { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "ステータス")]
        public int? tumiStatusKubun { get; set; }

        public string tumiBuildingName { get; set; }

        [MaxLength(10, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "積み郵便番号")]
        public string tumiPostCode { get; set; }

        [MaxLength(255, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "積み地")]
        public string tumiAddress { get; set; }

        [CustomDateFormat("yyyy/MM/dd HH:mm:ss")]
        [Display(Name = "日時")]
        public string oroshiDatetime { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "時間区分")]
        public int? oroshiTimeKubun { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "ステータス")]
        public int? oroshiStatusKubun { get; set; }

        public string oroshiBuildingName { get; set; }

        [MaxLength(10, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "卸地郵便番号")]
        public string oroshiPostCode { get; set; }

        [MaxLength(255, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "卸地住所")]
        public string oroshiAddress { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "重量")]
        public double? luggageWeight { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "車種")]
        public int? syasyu { get; set; }

        [MaxLength(255, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "備考")]
        public string remarks { get; set; }

        [MaxLength(100, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "装備品名")]
        public string enquipmentDisplay { get; set; }

        [MaxLength(100, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "荷物表示名")]
        public string luggageDisplay { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
