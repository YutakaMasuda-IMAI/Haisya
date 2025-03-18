using RenkeiDB.Common;
using RenkeiDB.Dto.ValidateRules;
using System.ComponentModel.DataAnnotations;

namespace RenkeiDB.Dto.EmptyCarDto
{
    /// <summary>
    /// 空車情報更新を表すDTO
    /// </summary>
    public class UpdateEmptyCarDto
    {
#pragma warning disable IDE1006 // Naming Styles
        [CustomDateFormat("yyyy/MM/dd")]
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "空車日")] 
        public string emptyCarDay { get; set; }

        [MaxLength(10, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "場所")]
        public string emptyPostCode { get; set; }

        [MaxLength(3, ErrorMessage = SystemConstants.Message.InValidParam)]
        [MinLength(3, ErrorMessage = SystemConstants.Message.InValidParam)]
        [StringLengthArray(255)]
        [Display(Name = "場所")]
        public string[] emptyAddresses { get; set; }

        [MaxLength(10, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "行先")]
        public string destPostCode { get; set; }

        [MaxLength(3, ErrorMessage = SystemConstants.Message.InValidParam)]
        [MinLength(3, ErrorMessage = SystemConstants.Message.InValidParam)]
        [StringLengthArray(255)]
        [Display(Name = "行先")]
        public string[] destAddresses { get; set; }

        [MaxLength(255, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "特記事項")]
        public string remarks { get; set; }

        [Range(0, 9999, ErrorMessage = SystemConstants.Message.AllowValue)]
        [Display(Name = "車番")]
        public int? syaban { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "車種")]
        public int? syasyu { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "重量")]
        public double? syaryoWeight { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "総重量")]
        public double? syaryoTotalWeight { get; set; }

        [MaxLength(100, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "主な必要装備")]
        public string enquipmentDisplay { get; set; }

        [MaxLength(50, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "乗務員名")]
        public string driverName { get; set; }

        [MaxLength(30, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "携帯番号")]
        public string cellPhone { get; set; }

        [CustomDateFormat("yyyy/MM/dd HH:mm:ss")]
        [Display(Name = "更新日時")]
        public string updateDatetime { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
