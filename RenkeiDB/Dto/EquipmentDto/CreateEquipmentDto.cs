using RenkeiDB.Common;
using System.ComponentModel.DataAnnotations;

namespace RenkeiDB.Dto.EquipmentDto
{
    /// <summary>
    /// 装備品作成情報を表すDTO
    /// </summary>
    public class CreateEquipmentDto
    {
#pragma warning disable IDE1006 // Naming Styles
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        public int? equipmentGroupId { get; set; }
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [MaxLength(50, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "名前")]
        public string equipmentName { get; set; }
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [MaxLength(50, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "単位")]
        public string unitName { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
