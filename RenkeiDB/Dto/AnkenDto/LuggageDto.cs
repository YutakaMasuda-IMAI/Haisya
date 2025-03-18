namespace RenkeiDB.Dto.AnkenDto
{
    public class LuggageDto
    {
        public int id { get; set; }
        public int sortOrder { get; set; }
        public string luggageName { get; set; }
        public string unitName { get; set; }
        public string remarks { get; set; }
        public LuggageGroupDto luggageGroup { get; set; }
    }
}
