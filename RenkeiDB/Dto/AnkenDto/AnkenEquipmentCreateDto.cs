using RenkeiDB.Common;
using System.ComponentModel.DataAnnotations;

namespace RenkeiDB.Dto.AnkenDto
{
    /// <summary>
    /// 案件装備品作成情報を表すDTO
    /// </summary>
    public class AnkenEquipmentCreateDto
    {
#pragma warning disable IDE1006 // Naming Styles
        [Display(Name = "装備品ID")]
        public int equipmentId { get; set; }

        [Display(Name = "数")]
        public int equipmentCount { get; set; }

        [MaxLength(50, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "備考")]
        public string remarks { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
