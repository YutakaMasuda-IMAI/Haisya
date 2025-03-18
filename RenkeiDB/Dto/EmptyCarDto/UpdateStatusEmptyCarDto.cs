using RenkeiDB.Common;
using System.ComponentModel.DataAnnotations;

namespace RenkeiDB.Dto.EmptyCarDto
{
    /// <summary>
    /// 空車ステータス更新情報を表すDTO
    /// </summary>
    public class UpdateStatusEmptyCarDto
    {
#pragma warning disable IDE1006 // Naming Styles
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Range(1, 4, ErrorMessage = SystemConstants.Message.AllowValue)]
        [Display(Name = "ステータス")]
        public int? status { get; set; }
        public int? tantouGroupId { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
