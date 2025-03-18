using RenkeiDB.Common;
using System.ComponentModel.DataAnnotations;

namespace RenkeiDB.Dto.AnkenDto
{
    /// <summary>
    /// 案件ポイント更新情報を表すDTO
    /// </summary>
    public class UpdateAnkenPointDto
    {
#pragma warning disable IDE1006 // Naming Styles
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "区分")]
        public int? kubun { get; set; }
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "住所")]
        public string address { get; set; }
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "日程")]
        public string pointDate { get; set; }
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
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "緯度")]
        public string lng { get; set; }
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "経度")]
        public string lat { get; set; }
        [Display(Name = "郵便番号")]
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        public string postCode { get; set; }

        [Display(Name = "住所2")]
        public string address2 { get; set; }

        [Display(Name = "住所3")]
        public string address3 { get; set; }

        [Display(Name = "住所4")]
        public string address4 { get; set; }

        [Display(Name = "建物名")]
        public string buildingName { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}