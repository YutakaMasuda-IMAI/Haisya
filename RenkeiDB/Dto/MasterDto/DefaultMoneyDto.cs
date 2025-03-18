namespace RenkeiDB.Dto.MasterDto
{
    /// <summary>
    /// デフォルト料金情報を表すDTO
    /// </summary>
    public class DefaultMoneyDto
    {
        public string area { get; set; }
        public string syasyuSize { get; set; }
        public int fromDistance { get; set; }
        public int toDistance { get; set; }
        public int Interval { get; set; }
        public decimal? amount { get; set; }
        public decimal? additionAmount { get; set; }
    }
}
