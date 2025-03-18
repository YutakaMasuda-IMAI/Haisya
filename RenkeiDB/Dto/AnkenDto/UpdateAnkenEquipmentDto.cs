using System.ComponentModel.DataAnnotations;

namespace RenkeiDB.Dto.AnkenDto
{
    /// <summary>
    /// 案件装備品更新情報を表すDTO
    /// </summary>
    public class UpdateAnkenEquipmentDto
    {
        [Display(Name = "装備品ID")]
        public int? equipmentId { get; set; }

        [Display(Name = "数")]
        public int? equipmentCount { get; set; }

        [Display(Name = "備考")]
        public string remarks { get; set; }
    }
}
