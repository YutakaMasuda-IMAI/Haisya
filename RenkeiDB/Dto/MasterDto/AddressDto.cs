namespace RenkeiDB.Dto.MasterDto
{
    /// <summary>
    /// 住所情報を表すDTO
    /// </summary>
    public class AddressDto
    {
        public int id { get; set; }
        public string ken { get; set; }
        public string shikucho { get; set; }
        public string choiki { get; set; }
    }
}
