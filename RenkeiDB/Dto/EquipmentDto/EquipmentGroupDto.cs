namespace RenkeiDB.Dto.EquipmentDto
{
    /// <summary>
    /// 装備品グループ情報を表すDTO
    /// </summary>
    public class EquipmentGroupDto
    {
        public int id { get; set; }
        public int sortOrder { get; set; }
        public string equipmentGroupName { get; set; }
    }
}
