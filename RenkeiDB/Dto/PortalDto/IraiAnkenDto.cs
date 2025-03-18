namespace RenkeiDB.Dto.PortalDto
{
    /// <summary>
    /// 依頼案件情報を表すDTO
    /// </summary>
    public class IraiAnkenDto
    {
#pragma warning disable IDE1006 // Naming Styles
        public int id { get; set; }
        public string ankenNo { get; set; }
        public string shareLuggageNo { get; set; }
        public string syasyuDisplay { get; set; }
        public string tumiDatetime { get; set; }
        public string tumiAddress { get; set; }
        public string oroshiAddress { get; set; }
        public string kokyakuName { get; set; }
        public string vehicleRentalDestination { get;set; }
        public string syaban {  get; set; }
        public string driverName { get; set; }
        public string status { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
