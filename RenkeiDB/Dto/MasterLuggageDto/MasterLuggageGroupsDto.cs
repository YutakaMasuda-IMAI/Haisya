namespace RenkeiDB.Dto.MasterLuggageDto
{
    /// <summary>
    /// マスター荷物グループDTO
    /// </summary>
    public class MasterLuggageGroupsDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// ソート順
        /// </summary>
        public int sortOrder { get; set; }

        /// <summary>
        /// 荷物グループ名
        /// </summary>
        public string luggageGroupName { get; set; }
    }
}
