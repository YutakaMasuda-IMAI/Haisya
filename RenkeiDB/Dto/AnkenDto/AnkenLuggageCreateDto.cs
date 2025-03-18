using RenkeiDB.Common;
using System.ComponentModel.DataAnnotations;

namespace RenkeiDB.Dto.AnkenDto
{
    /// <summary>
    /// 案件荷物作成情報を表すDTO
    /// </summary>
    public class AnkenLuggageCreateDto
    {
#pragma warning disable IDE1006 // Naming Styles
        [Display(Name = "荷物ID")]
        public int luggageId { get; set; }

        [Display(Name = "数")]
        public int luggageCount { get; set; }

        [MaxLength(50, ErrorMessage = SystemConstants.Message.InvalidNumberLength)]
        [Display(Name = "備考")]
        public string remarks { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
