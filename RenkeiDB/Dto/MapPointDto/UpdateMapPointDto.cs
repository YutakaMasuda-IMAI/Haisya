using RenkeiDB.Common;
using System.ComponentModel.DataAnnotations;

namespace RenkeiDB.Dto.MapPointDto
{
    /// <summary>
    /// 地図ポイント更新情報を表すDTO
    /// </summary>
    public class UpdateMapPointDto
    {
#pragma warning disable IDE1006 // Naming Styles
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        public int userId { get; set; }
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        public int groupId { get; set; }
        [MaxLength(100, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "建物ZIP(地図)")]
        public string buildingZid { get; set; }
        [MaxLength(100, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        public string buildingZidAttr { get; set; }
        [MaxLength(255, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "ポイント名")]
        public string buildingName { get; set; }
        [MaxLength(50, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        public string buildingNameRead { get; set; }
        [MaxLength(255, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        public string address { get; set; }
        [MaxLength(255, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        public string addressCode { get; set; }
        [MaxLength(10, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        public string addressLevel { get; set; }
        [MaxLength(100, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "緯度")]
        public string lng { get; set; }
        [MaxLength(100, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "経度")]
        public string lat { get; set; }
        [MaxLength(10, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "郵便番号")]
        public string postCode { get; set; }
        [MaxLength(255, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "県")]
        public string address2 { get; set; }
        [MaxLength(255, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "市区町村")]
        public string address3 { get; set; }
        [MaxLength(255, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "町域")]
        public string address4 { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
