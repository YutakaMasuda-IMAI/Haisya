namespace SeikyuWeb.Dto.Shitabarai
{
    public class CheckShitabaraiChangeDto
    {
#pragma warning disable IDE1006 // Naming Styles
        public int checkShitabaraiId { get; set; }
        public int uriageShiharaiId { get; set; }
        public double? qty { get; set; }
        public int? unit { get; set; }
        public decimal? calcPrice { get; set; }
        public decimal? shiharaiUnchin { get; set; }
        public decimal? tatekaekin { get; set; }
        public decimal? warimashi1 { get; set; }
        public decimal? warimashi2 { get; set; }
        public decimal? warimashi3 { get; set; }
        public decimal? warimashi4 { get; set; }
        public decimal? warimashi5 { get; set; }
        public decimal? shiharaiTotal { get; set; }
        public decimal unitPrice { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
