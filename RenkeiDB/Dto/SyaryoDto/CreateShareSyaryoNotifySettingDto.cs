using RenkeiDB.Common;
using RenkeiDB.Dto.ValidateRules;
using System.ComponentModel.DataAnnotations;

namespace RenkeiDB.Dto.SyaryoDto
{
    /// <summary>
    /// 共有車両通知設定作成情報を表すDTO
    /// </summary>
    public class CreateShareSyaryoNotifySettingDto
    {
#pragma warning disable IDE1006 // Naming Styles
        [CustomDateFormat("yyyy/MM/dd")]
        [Display(Name = "開始日")]
        public string fromDate { get; set; }

        [FromDateGreaterThanToDate("fromDate")]
        [RequiredIf("fromDate", null)]
        [CustomDateFormat("yyyy/MM/dd")]
        [Display(Name = "終了日")]
        public string toDate { get; set; }

        [CustomDateFormat("yyyy/MM/dd")]
        [Display(Name = "空車開始日")]
        public string emptyFromDate { get; set; }

        [FromDateGreaterThanToDate("emptyFromDate")]
        [CustomDateFormat("yyyy/MM/dd")]
        [Display(Name = "空車終了日")]
        public string emptyToDate { get; set; }

        [MaxLength(3, ErrorMessage = SystemConstants.Message.InValidParam)]
        [MinLength(3, ErrorMessage = SystemConstants.Message.InValidParam)]
        [RequiredIf("isEmpty", true)]
        [StringLengthArray(20)]
        [Display(Name = "空車")]
        public string[] empties { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "空車")]
        public bool? isEmpty { get; set; }

        [MaxLength(3, ErrorMessage = SystemConstants.Message.InValidParam)]
        [MinLength(3, ErrorMessage = SystemConstants.Message.InValidParam)]
        [RequiredIf("isDest", true)]
        [StringLengthArray(20)]
        [Display(Name = "積地")]
        public string[] dests { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "行先")]
        public bool? isDest { get; set; }

        [RequiredIf("isSyasyu", true)]
        [MaxLength(20, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "車種")]
        public string syasyu { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "車種")]
        public bool? isSyasyu { get; set; }

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

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "グループ")]
        public int? tantouGroupId { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
