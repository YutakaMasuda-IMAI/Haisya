using System.ComponentModel.DataAnnotations;

namespace RenkeiDB.Dto.AnkenDto
{
    /// <summary>
    /// 案件荷物更新情報を表すDTO
    /// </summary>
    public class UpdateAnkenLuggageDto
    {
        [Display(Name = "荷物ID")]
        public int? luggageId { get; set; }

        [Display(Name = "数")]
        public int? luggageCount { get; set; }

        [Display(Name = "備考")]
        public string remarks { get; set; }
    }
}
