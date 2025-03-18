namespace RenkeiDB.Dto.AnkenDto
{
    /// <summary>
    /// 案件ポイント情報を表すDTO
    /// </summary>
    public class AnkenPointDto
    {
#pragma warning disable IDE1006 // Naming Styles
        public int id { get; set; }
        public int renkeiAnkenOrder { get; set; }
        public int kubun { get; set; }
        public int pointOrder { get; set; }
        public string seKubun { get; set; }
        public string address { get; set; }
        public string addressCode { get; set; }
        public string addressLevel { get; set; }
        public string lng { get; set; }
        public string lat { get; set; }
        public string buildingName { get; set; }
        public string buildingZid { get; set; }
        public string buildingZidAttr { get; set; }
        public string buildingNameRead { get; set; }
        public string pointKoumokuTitle { get; set; }
        public string pointType { get; set; }
        public string pointName { get; set; }
        public string pointDate { get; set; }
        public string pointTime { get; set; }
        public int? pointTimeKubun { get; set; }
        public int? pointStatusKubun { get; set; }
        public int flgGenchiKakunin { get; set; }
        public string tollDisplay { get; set; }
        public double? tollDisplayHeight { get; set; }
        public string postCode { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string address4 { get; set; }
        public string roadType { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
