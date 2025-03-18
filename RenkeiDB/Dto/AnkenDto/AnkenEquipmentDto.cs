namespace RenkeiDB.Dto.AnkenDto
{
    /// <summary>
    /// 案件装備品情報を表すDTO
    /// </summary>
    public class AnkenEquipmentDto
    {
        public int id { get; set; }
        public int renkeiAnkenOrder { get; set; }
        public EquipmentDto equipment { get; set; }
        public double? equipmentCount { get; set; }
        public string remarks { get; set; }
    }
}
