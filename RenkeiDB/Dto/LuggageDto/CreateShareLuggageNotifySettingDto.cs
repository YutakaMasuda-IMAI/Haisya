using RenkeiDB.Common;
using System.ComponentModel.DataAnnotations;
using System;
using RenkeiDB.Dto.ValidateRules;

namespace RenkeiDB.Dto.LuggageDto
{
    /// <summary>
    /// 荷物共有通知設定作成情報を表すDTO
    /// </summary>
    public class CreateShareLuggageNotifySettingDto
    {
#pragma warning disable IDE1006 // Naming Styles
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "通知")]
        public bool? notifyFlg { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "メール通知")]
        public bool? meilFlg { get; set; }

        [RequiredIf("meilFlg", true)]
        [MultiEmailAddress()]
        [StringLength(50, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "メールアドレス")]
        public string meilAddress { get; set; }

        [CustomDateFormat("yyyy/MM/dd")]
        [Display(Name = "開始日")]
        public string fromDate { get; set; }

        [CustomDateFormat("yyyy/MM/dd")]
        [FromDateGreaterThanToDate("fromDate")]
        [Display(Name = "終了日")]
        public string toDate { get; set; }

        [CustomDateFormat("yyyy/MM/dd")]
        [Display(Name = "積み開始日")]
        public string tumiFromDate { get; set; }

        [CustomDateFormat("yyyy/MM/dd")]
        [FromDateGreaterThanToDate("tumiFromDate")]
        [Display(Name = "積み終了日")]
        public string tumiToDate { get; set; }

        [MaxLength(3, ErrorMessage = SystemConstants.Message.InValidParam)]
        [MinLength(3, ErrorMessage = SystemConstants.Message.InValidParam)]
        [RequiredIf("isTumi", true)]
        [StringLengthArray(20)]
        [Display(Name = "積み")]
        public string[] tumis { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "積み地")]
        public bool? isTumi { get; set; }

        [MaxLength(3, ErrorMessage = SystemConstants.Message.InValidParam)]
        [MinLength(3, ErrorMessage = SystemConstants.Message.InValidParam)]
        [RequiredIf("isOroshi", true)]
        [StringLengthArray(20)]
        [Display(Name = "卸")]
        public string[] oroshis { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "卸し地")]
        public bool? isOroshi { get; set; }

        [RequiredIf("isSyasyu", true)]
        [MaxLength(20, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "車種")]
        public string syasyu { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "車種")]
        public bool? isSyasyu { get; set; }

        [CustomDateFormat("yyyy/MM/dd HH:mm:ss")]
        [Display(Name = "更新日時")]
        public string updateDatetime { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "グループ")]
        public int? tantouGroupId { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
