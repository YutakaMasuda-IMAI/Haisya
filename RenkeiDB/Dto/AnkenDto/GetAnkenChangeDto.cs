using System.Collections.Generic;

namespace RenkeiDB.Dto.AnkenDto
{
    /// <summary>
    /// 案件変更情報を取得するDTO
    /// </summary>
    public class GetAnkenChangeDto
    {
        public string renkeiAnkenNo { get; set; }
        public List<HistoryChangeAnkenDto> changes { get; set; }
    }

    /// <summary>
    /// 案件変更履歴情報を表すDTO
    /// </summary>
    public class HistoryChangeAnkenDto
    {
        public string name { get; set; }
        public List<ChangeDto> values { get; set; }
    }

    /// <summary>
    /// 変更情報を表すDTO
    /// </summary>
    public class ChangeDto
    {
        public string val { get; set; }
        public bool change { get; set; }
    }
}
