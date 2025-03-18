namespace RenkeiDB.Dto.EmptyCarDto
{
    /// <summary>
    /// 空車保持情報を表すDTO
    /// </summary>
    public class KeepEmptyCarDto
    {
#pragma warning disable IDE1006 // Naming Styles
        public int id { get; set; }
        public string shareSyaryoNo { get; set; }
        public string syasyuDisplay { get; set; }
        public string syaban { get; set; }
        public string emptyCarDay { get; set; }
        public string emptyAddress { get; set; }
        public string companyName { get; set; }
        public string driverName { get; set; }
        public string cellPhone { get; set; }
        public string remark { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
