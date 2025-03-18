namespace RenkeiDB.Dto.AnkenDto
{
    /// <summary>
    /// 案件荷物情報を表すDTO
    /// </summary>
    public class AnkenLuggageDto
    {
        public int id { get; set; }
        public int renkeiAnkenOrder { get; set; }
        public LuggageDto luggage { get; set; }
        public double? luggageCount { get; set; }
        public string remarks { get; set; }
    }
}
