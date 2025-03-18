using System.Collections.Generic;
using RenkeiDB.Data;

namespace RenkeiDB.Dto.SettingDto
{
    /// <summary>
    /// 車両通知設定情報を表すDTO
    /// </summary>
    public class SyaryoNotifySettignDto
    {
        public int id { get; set; }
        public bool notifyFlg { get; set; }
        public bool meilFlg { get; set; }
        public string meilAddress { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string emptyFromDate { get; set; }
        public string emptyToDate { get; set; }
        public List<string> empties { get; set; }
        public bool isEmpty { get; set; }
        public List<string> dests { get; set; }
        public bool isDest { get; set; }
        public string syasyu { get; set; }
        public bool isSyasyu { get; set; } = false;
        public string syasyuDisplay { get; set; }
        public string updateDatetime { get; set; }
        public int tantouGroupId { get; set; }
        public string tantouGroupName { get; set; }
    }

    public class JoinSyaryoNotifySettingDto
    {
        public T_Share_Syaryo_Notify_Setting S { get; set; }
        public M_CompanyUser_Group UG { get; set; }
    }
}
