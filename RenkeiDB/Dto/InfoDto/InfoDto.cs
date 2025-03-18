namespace RenkeiDB.Dto.InfoDto
{
    /// <summary>
    /// お知らせDTO
    /// </summary>
    public class InfoDto
    {
        public string title { get; set; }
        public string detail { get; set; }
        public string category { get; set; }
        public string action { get; set; }
        public string controller { get; set; }
        public string insertDatetime { get; set; }
        public CompanyDto company { get; set; }
        public int criticalKubun { get; set; }
        public int renkeiAnkenId { get; set; }
        public int id { get; set; }
    }

    /// <summary>
    /// 表示フラグDTO
    /// </summary>
    public class DisplayFlgDto
    {
#pragma warning disable IDE1006 // Naming Styles
        public bool displayFlg { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
