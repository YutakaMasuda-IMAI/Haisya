namespace RenkeiDB.Dto.AnkenDto
{
    /// <summary>
    /// 荷物グループ情報を表すDTO
    /// </summary>
    public class LuggageGroupDto
    {
        public int id { get; set; }
        public int sortOrder { get; set; }
        public string luggageGroupName { get; set; }
    }
}
