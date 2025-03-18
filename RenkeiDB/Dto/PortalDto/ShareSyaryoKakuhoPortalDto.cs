namespace RenkeiDB.Dto.PortalDto
{
    /// <summary>
    /// 共有車両確保ポータル情報を表すDTO
    /// </summary>
    public class ShareSyaryoKakuhoPortalDto
    {
        public int id { get; set; }
        public string shareSyaryoNo { get; set; }
        public string syasyuDisplay { get; set; }
        public string syaban { get; set; }
        public string emptyCarDay { get; set; }
        public string emptyAddress {  get; set; }
        public string companyId { get; set; }
        public string driverName { get; set; }
        public string cellPhone { get; set; }
        public int remarksCnt { get; set; }

    }
}
