namespace RenkeiDB.Dto.EquipmentDto
{
    /// <summary>
    /// マスター装備品情報を表すDTO
    /// </summary>
    public class MasterEquipmentDto
    {
        public int id {  get; set; }
        public int sortOrder { get; set; }
        public string equipmentName {  get; set; }
        public string unitName {  get; set; }
        public string remarks { get; set; }
    }
}
