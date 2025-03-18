using RenkeiDB.Common;
using RenkeiDB.Dto.ValidateRules;
using System.ComponentModel.DataAnnotations;

namespace RenkeiDB.Dto.AnkenDto
{
    /// <summary>
    /// 案件ポイント作成情報を表すDTO
    /// </summary>
    public class AnkenPointCreateDto
    {
#pragma warning disable IDE1006 // Naming Styles
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "区分")]
        public int? kubun { get; set; }

        [MaxLength(255, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "住所")]
        public string address { get; set; }

        [CustomDateFormat("yyyy/MM/dd")]
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "日程")]
        public string pointDate { get; set; }

        [MaxLength(5, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [CustomDateFormat("HH:mm")]
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "時間")]
        public string pointTime { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "時間区分")]
        public int? pointTimeKubun { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "ステータス区分")]
        public int? pointStatusKubun { get; set; }

        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "道路種別")]
        public int? roadType { get; set; }

        [MaxLength(100, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "緯度")]
        public string lng { get; set; }

        [MaxLength(100, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "経度")]
        public string lat { get; set; }

        [MaxLength(10, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "郵便番号")]
        public string postCode { get; set; }

        [MaxLength(255, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "住所2")]
        public string address2 { get; set; }

        [MaxLength(255, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "住所3")]
        public string address3 { get; set; }

        [MaxLength(255, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "住所4")]
        public string address4 { get; set; }

        [MaxLength(255, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "建物名")]
        public string buildingName { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
