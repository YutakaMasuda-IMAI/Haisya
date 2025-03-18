namespace RenkeiDB.Dto.MasterLuggageDto
{
    /// <summary>
    /// マスター荷物情報を表すDTO
    /// </summary>
    public class MasterLuggageDto
    {
        public int id {  get; set; }
        public int sortOrder { get; set; }
        public string luggageName { get; set; }
        public string unitName { get; set; }
        public string remarks { get; set; }
    }
}
