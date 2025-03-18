namespace SeikyuWeb.Dto.Contractor
{
    /// <summary>
    /// 会社DTO
    /// </summary>
    public class CompanyDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 会社コード
        /// </summary>
        public string companyCode { get; set; }

        /// <summary>
        /// 会社名
        /// </summary>
        public string companyName { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}