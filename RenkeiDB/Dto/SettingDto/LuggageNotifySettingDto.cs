using System.Collections.Generic;
using RenkeiDB.Data;

namespace RenkeiDB.Dto.SettingDto
{
    /// <summary>
    /// 荷物通知設定情報を表すDTO
    /// </summary>
    public class LuggageNotifySettingDto
    {
        public int id { get; set; }
        public bool notifyFlg { get; set; }
        public bool meilFlg { get; set; }
        public string meilAddress { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string tumiFromDate { get; set; }
        public string tumiToDate { get; set; }
        public List<string> tumis { get; set; }
        public bool isTumi { get; set; }
        public List<string> oroshis { get; set; }
        public bool isOroshi { get; set; }
        public string syasyu { get; set; }
        public string syasyuDisplay { get; set; }
        public bool isSyasyu { get; set; } = false;
        public string updateDatetime { get; set; }
        public int tantouGroupId { get; set; }
        public string tantouGroupName { get; set; }
    }

    public class JoinLuggageNotifySettingDto
    {
        public T_Share_Luggage_Notify_Setting S { get; set; }
        public M_CompanyUser_Group UG { get; set; }
    }
}
