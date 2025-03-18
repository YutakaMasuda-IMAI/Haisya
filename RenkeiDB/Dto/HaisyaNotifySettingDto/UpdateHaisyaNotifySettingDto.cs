using static RenkeiDB.Common.SystemEnums;
using System.ComponentModel.DataAnnotations;
using RenkeiDB.Common;

namespace RenkeiDB.Dto.HaisyaNotifySettingDto
{
    /// <summary>
    /// 配車通知設定更新情報を表すDTO
    /// </summary>
    public class UpdateHaisyaNotifySettingDto
    {
#pragma warning disable IDE1006 // Naming Styles
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [EnumDataType(typeof(NotifyType), ErrorMessage = SystemConstants.Message.AllowValue)]
        [Display(Name = "配車通知")]
        public int? notifyType { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
