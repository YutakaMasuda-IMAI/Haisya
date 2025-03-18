using System.Collections.Generic;
using static RenkeiDB.Common.SystemEnums;

namespace RenkeiDB.Dto.SettingDto
{
    /// <summary>
    /// 設定エントリー情報を表すDTO
    /// </summary>
    public class SettingEntryDto
    {
        public NotifyType notifyType { get; set; } = NotifyType.NOTIFY_WHEN_EACH_CONFIRMED;
        /// <summary>
        /// 荷物共有通知設定
        /// </summary>
        public IEnumerable<LuggageNotifySettingDto> shareLuggageNotifySettings { set; get; }

        /// <summary>
        /// 車両共有通知設定
        /// </summary>
        public IEnumerable<SyaryoNotifySettignDto> shareSyaryoNotifySettigns { set; get; }

    }
}
