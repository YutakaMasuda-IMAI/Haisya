namespace RenkeiDB.Dto.MasterDto
{
    public class SyaryoDto
    {
        public int id { get; set; }
        public string syasyu { get; set; }
        public string kata { get; set; }
        public int sortOrder { get; set; }
        public string syasyuDisplay { get; set; }
        public string kataDisplay { get; set; }
        public double Long { get; set; }
        public double width { get; set; }
        public double height { get; set; }
        public double maxLoadCapa { get; set; }
        public double carWeight { get; set; }
        public double carGrossWeight { get; set; }
        public double avgFuelCosts { get; set; }
        public string size { get; set; }
        public string kataId { get; set; }
        public int? syasyuKubunId { get; set; }
        public string tollType { get; set; }
        public string regulationType { get; set; }
        public string carDetailInfo { get; set; }
    }
}
