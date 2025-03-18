namespace RenkeiDB.Dto
{
    /// <summary>
    /// マスターデータ情報を表すDTO
    /// </summary>
    public class MasterDataDto
    {
    }

    /// <summary>
    /// 共通の結果値を表すDTO
    /// </summary>
    public class CommonResultValDto
    {
        public int code { get; set; }
        public string message { get; set; }
    }

    /// <summary>
    /// M_Code_DataのDTO
    /// </summary>
    public class MasterCodeDataDto
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    /// <summary>
    /// M_LuggageのDTO
    /// </summary>
    public sealed class LuggageDataDto
    {
#pragma warning disable IDE1006 // Naming Styles
        public int luggageGroupId { get; set; }
        public string luggageName { get; set; }
        public string unitName { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }

    /// <summary>
    /// M_LuggageのDTO
    /// </summary>
    public class MLuggageDto
    {
        public int id { get; set; }
        public int sortOrder { get; set; }
        public string luggageName { get; set; }
        public string unitName { get; set; }
        public string remarks { get; set; }
    }

    /// <summary>
    /// M_EquipmentのDTO
    /// </summary>
    public class MEquipmentDto
    {
        public int id { get; set; }
        public int sortOrder { get; set; }
        public string equipmentName { get; set; }
        public string unitName { get; set; }
        public string remarks { get; set; }
    }
}
