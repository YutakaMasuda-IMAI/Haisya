namespace RenkeiDB.Dto.MapPointDto
{
    /// <summary>
    /// 地図ポイント情報を表すDTO
    /// </summary>
    public class MapPointDto
    {
        public int id { get; set; }
        public string postCode { get; set; }
        public string address { get; set; }
        public string buildingName { get; set; }
        public string lng { get; set; }
        public string lat { get; set; }
        public string address2 { get;set; }
    }
}
