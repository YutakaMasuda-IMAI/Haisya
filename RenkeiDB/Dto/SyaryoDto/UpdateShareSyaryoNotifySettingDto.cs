using RenkeiDB.Common;
using RenkeiDB.Dto.ValidateRules;
using System.ComponentModel.DataAnnotations;

namespace RenkeiDB.Dto.SyaryoDto
{
    /// <summary>
    /// 共有車両通知設定更新情報を表すDTO
    /// </summary>
    public class UpdateShareSyaryoNotifySettingDto : CreateShareSyaryoNotifySettingDto
    {
#pragma warning disable IDE1006 // Naming Styles
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [CustomDateFormat("yyyy/MM/dd HH:mm:ss")]
        [Display(Name = "更新日時")]
        public string updateDatetime { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
