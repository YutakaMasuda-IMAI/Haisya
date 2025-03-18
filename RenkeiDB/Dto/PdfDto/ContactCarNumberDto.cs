using System;

namespace RenkeiDB.Dto.PdfDto
{
    /// <summary>
    /// 車番連絡情報を表すDTO
    /// </summary>
    public class ContactCarNumberDto
    {
        public int id { get; set; }
        public string ankenNo { get; set; }
        public string shareLuggageNo { get; set; }
        public string syasyuDisplay { get; set; }
        public DateTime? tumiDatetime { get; set; }
        public string tumiAddress { get; set; }
        public string oroshiAddress { get; set; }
        public string kokyakuName { get; set; }
        public string vehicleRentalDestination { get; set; }
        public string syaban { get; set; }
        public string driverName { get; set; }
        public string status { get; set; }
        public string haisyaTanto { get; set; }
        public string haisyaTantoPhone { get; set; }
        public DateTime? oroshiDatetime { get; set; }
        public string drivePhone { get; set; }
        public string remarks { get; set; }
    }
}
